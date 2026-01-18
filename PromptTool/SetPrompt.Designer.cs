namespace PromptTool
{
    partial class SetPrompt
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
            txtPrompt = new TextBox();
            btnOK = new Button();
            label1 = new Label();
            label2 = new Label();
            txtNote = new TextBox();
            SuspendLayout();
            // 
            // txtPrompt
            // 
            txtPrompt.Location = new Point(99, 30);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(627, 219);
            txtPrompt.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(303, 313);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(112, 34);
            btnOK.TabIndex = 1;
            btnOK.Text = "确定";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 30);
            label1.Name = "label1";
            label1.Size = new Size(64, 24);
            label1.TabIndex = 2;
            label1.Text = "提示词";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 268);
            label2.Name = "label2";
            label2.Size = new Size(46, 24);
            label2.TabIndex = 3;
            label2.Text = "备注";
            // 
            // txtNote
            // 
            txtNote.Location = new Point(99, 265);
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(627, 30);
            txtNote.TabIndex = 4;
            // 
            // SetPrompt
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(753, 369);
            Controls.Add(txtNote);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnOK);
            Controls.Add(txtPrompt);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SetPrompt";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "输入提示词";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPrompt;
        private Button btnOK;
        private Label label1;
        private Label label2;
        private TextBox txtNote;
    }
}