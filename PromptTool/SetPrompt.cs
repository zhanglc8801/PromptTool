using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PromptTool
{
    public partial class SetPrompt : Form
    {
        public event Action BtnClicked;
        public string PromptText
        {
            get { return txtPrompt.Text; }
            set { txtPrompt.Text = value; }
        }
        public string NoteText
        {
            get { return txtNote.Text; }
            set { txtNote.Text = value; }
        }

        public bool DelSource
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        public SetPrompt()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Enabled = false;
            this.btnOK.Text= "处理中...";
            BtnClicked?.Invoke();
            this.Close();
        }
    }
}
