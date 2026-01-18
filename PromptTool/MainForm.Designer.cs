namespace PromptTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            listView1 = new ListView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            修改图片存储目录DToolStripMenuItem = new ToolStripMenuItem();
            txtPrompt = new TextBox();
            BtnSave = new Button();
            lbl_Note = new Label();
            panel1 = new Panel();
            txtNote = new TextBox();
            contextMenuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.AllowDrop = true;
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.ContextMenuStrip = contextMenuStrip1;
            listView1.Location = new Point(12, 12);
            listView1.Name = "listView1";
            listView1.Size = new Size(1335, 618);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            listView1.DragDrop += listView1_DragDrop;
            listView1.DragEnter += listView1_DragEnter;
            listView1.DoubleClick += listView1_DoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 修改图片存储目录DToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(251, 34);
            // 
            // 修改图片存储目录DToolStripMenuItem
            // 
            修改图片存储目录DToolStripMenuItem.Name = "修改图片存储目录DToolStripMenuItem";
            修改图片存储目录DToolStripMenuItem.Size = new Size(250, 30);
            修改图片存储目录DToolStripMenuItem.Text = "修改图片存储目录(&D)";
            修改图片存储目录DToolStripMenuItem.Click += 修改图片存储目录DToolStripMenuItem_Click;
            // 
            // txtPrompt
            // 
            txtPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPrompt.Location = new Point(12, 3);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(1335, 172);
            txtPrompt.TabIndex = 6;
            // 
            // BtnSave
            // 
            BtnSave.Location = new Point(12, 186);
            BtnSave.Name = "BtnSave";
            BtnSave.Size = new Size(112, 34);
            BtnSave.TabIndex = 7;
            BtnSave.Text = "保存";
            BtnSave.UseVisualStyleBackColor = true;
            BtnSave.Click += BtnSave_Click;
            // 
            // lbl_Note
            // 
            lbl_Note.AutoSize = true;
            lbl_Note.Location = new Point(130, 191);
            lbl_Note.Name = "lbl_Note";
            lbl_Note.Size = new Size(64, 24);
            lbl_Note.TabIndex = 8;
            lbl_Note.Text = "备注：";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtNote);
            panel1.Controls.Add(txtPrompt);
            panel1.Controls.Add(lbl_Note);
            panel1.Controls.Add(BtnSave);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 640);
            panel1.Name = "panel1";
            panel1.Size = new Size(1359, 236);
            panel1.TabIndex = 9;
            // 
            // txtNote
            // 
            txtNote.Location = new Point(198, 189);
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(1149, 30);
            txtNote.TabIndex = 9;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1359, 876);
            Controls.Add(panel1);
            Controls.Add(listView1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PromptTool";
            Load += MainForm_Load;
            contextMenuStrip1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ListView listView1;
        private TextBox txtPrompt;
        private Button BtnSave;
        private Label lbl_Note;
        private Panel panel1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 修改图片存储目录DToolStripMenuItem;
        private TextBox txtNote;
    }
}