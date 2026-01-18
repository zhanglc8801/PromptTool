using PromptTool.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace PromptTool
{
    public partial class MainForm : Form
    {
        ImageList imageList;
        SqlSugarClient db = Db.GetDb();
        string imagePath = "";
        public MainForm()
        {
            InitializeComponent();
            txtNote.Text = "提示：双击图片可用默认程序打开查看，拖拽图片或视频文件到列表中添加提示词";
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            // 自动建表（如果不存在）
            db.CodeFirst.InitTables<Prompt>();
            db.CodeFirst.InitTables<Cfg>();
            InitListView();

            CheckImagePath();
            await InitData();
        }

        private void CheckImagePath()
        {
            Cfg cfg = db.Queryable<Cfg>().Where(x => x.Key == "ImagePath").First();
            if (cfg != null)
            {
                imagePath = cfg.Value;
            }
            else
            {
                if (DialogResult.OK == MessageBox.Show("请先设置图片存储路径", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        cfg = new Cfg
                        {
                            Key = "ImagePath",
                            Value = fbd.SelectedPath
                        };
                        db.Insertable(cfg).ExecuteCommand();
                        return;
                    }
                }
            }
        }

        private async Task InitData()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            imageList.Images.Clear();
            listView1.EndUpdate();
            // 1. 后台线程取数据
            var list = await Task.Run(() =>
                db.Queryable<Prompt>().ToList()
            );
            // 2. 并发生成缩略图
            await LoadItemsAsync(list);
        }

        #region listView1事件
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Directory.CreateDirectory(imagePath);

            bool isImage = IsImage(files[0]);
            bool isVideo = IsVideo(files[0]);

            if (!isImage && !isVideo)
                return;

            SetPrompt sp = new SetPrompt();
            sp.BtnClicked += () =>
            {
                string newName = Guid.NewGuid().ToString("N") + Path.GetExtension(files[0]);
                string destPath = Path.Combine(imagePath, newName);

                File.Copy(files[0], destPath, true);

                ImageOrientation orientation = ImageOrientation.Landscape;

                string imageKey = destPath;

                if (isImage)
                {
                    using var image = SixLabors.ImageSharp.Image.Load(files[0]);
                    orientation = image.Width >= image.Height
                        ? ImageOrientation.Landscape
                        : ImageOrientation.Portrait;
                    imageList.Images.Add(imageKey, GenThumbnail(files[0]));
                }
                else
                {
                    imageList.Images.Add(imageKey, Resource.Video);
                }

                listView1.BeginUpdate();
                Prompt prompt = new Prompt
                {
                    ImageName = newName,
                    imageOrientation = orientation,
                    PromptStr = sp.PromptText,
                    Note = sp.NoteText
                };
                var lvItem = new ListViewItem(newName);
                lvItem.ImageKey = imageKey;
                lvItem.Tag = prompt;
                listView1.Items.Add(lvItem);
                listView1.EndUpdate();
                db.Insertable(prompt).ExecuteCommand();
            };
            sp.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            var lvItem = listView1.SelectedItems[0];
            var prompt = lvItem.Tag as Prompt;
            if (prompt == null)
                return;
            // 示例：显示到 TextBox / RichTextBox / Label
            txtPrompt.Text = prompt.PromptStr;
            txtNote.Text = prompt.Note;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            try
            {
                var item = listView1.SelectedItems[0];
                string filePath = Path.Combine(@"D:\aaa", (item.Tag as Prompt).ImageName);
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("文件不存在");
                    return;
                }
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // 关键：使用系统默认程序
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 保存修改提示词
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择一张图片");
                return;
            }
            var lvItem = listView1.SelectedItems[0];
            var prompt = lvItem.Tag as Prompt;

            if (prompt == null)
                return;
            prompt.PromptStr = txtPrompt.Text;
            prompt.Note = txtNote.Text;
            db.Updateable(prompt).ExecuteCommand();
            MessageBox.Show("保存成功");
        }
        #endregion

        private void 修改图片存储目录DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                db.Updateable<Cfg>()
                    .SetColumns(x => x.Value == fbd.SelectedPath)
                    .Where(x => x.Key == "ImagePath")
                    .ExecuteCommand();
                return;
            }
        }

        void InitListView()
        {
            imageList = new ImageList
            {
                ImageSize = new System.Drawing.Size(128, 128),
                ColorDepth = ColorDepth.Depth32Bit
            };
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = imageList;
        }

        #region 批量生成缩略图
        private async Task LoadItemsAsync(List<Prompt> list)
        {
            // 控制并发量，防止 CPU / 内存爆
            using var semaphore = new SemaphoreSlim(4);
            var tasks = list.Select(async item =>
            {
                await semaphore.WaitAsync();
                try
                {
                    string fullPath = Path.Combine(imagePath, item.ImageName);
                    if (!File.Exists(fullPath))
                        return;
                    Bitmap bmp = null;
                    bool isValid = false;
                    if (IsImage(fullPath))
                    {
                        bmp = GenThumbnail(fullPath);
                        isValid = true;
                    }
                    else if (IsVideo(fullPath))
                    {
                        bmp = Resource.Video;
                        isValid = true;
                    }
                    if (!isValid)
                        return;
                    // 回 UI 线程
                    BeginInvoke(() =>
                    {
                        string imageKey = fullPath;
                        imageList.Images.Add(imageKey, bmp);
                        var lvItem = new ListViewItem(item.Note!=""?item.Note: item.ImageName)
                        {
                            ImageKey = imageKey,
                            Tag = item
                        };
                        listView1.Items.Add(lvItem);
                    });
                }
                finally
                {
                    semaphore.Release();
                }
            });
            await Task.WhenAll(tasks);
        }

        #endregion

        #region WebP 图片处理
        // 生成 WebP 缩略图
        Bitmap GenThumbnail(string fullPath)
        {
            using var image = SixLabors.ImageSharp.Image.Load(fullPath);
            image.Mutate(x => x.Resize(128, 128));

            using var ms = new MemoryStream();
            image.SaveAsBmp(ms);

            ms.Position = 0;
            return new Bitmap(ms);
        }

        // WebP 转 JPG
        void ConvertWebpToJpg(string webpPath, string jpgPath)
        {
            using var image = SixLabors.ImageSharp.Image.Load(webpPath);
            image.Save(jpgPath, new JpegEncoder
            {
                Quality = 90
            });
        }
        #endregion

        #region 图片格式判断
        bool IsImage(string file)
        {
            string ext = Path.GetExtension(file).ToLower();
            return ext == ".jpg" || ext == ".png" || ext == ".bmp" || ext == ".jpeg" || ext == ".webp";
        }
        bool IsVideo(string file)
        {
            string ext = Path.GetExtension(file).ToLower();
            return ext == ".mp4" || ext == ".avi" || ext == ".mkv";
        }
        #endregion
    }
}
