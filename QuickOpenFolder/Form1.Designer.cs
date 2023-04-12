namespace QuickOpenFolder
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.txtFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.UpdateFoldersNameButton = new System.Windows.Forms.Button();
            this.CompareClipboardContentButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ShowDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.ShowUpdateStateTextBox = new System.Windows.Forms.TextBox();
            this.ShowClipboardContentTextBox = new System.Windows.Forms.TextBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.RenameButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.Location = new System.Drawing.Point(30, 248);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(136, 26);
            this.OpenFolderButton.TabIndex = 0;
            this.OpenFolderButton.Text = "打开文件夹";
            this.OpenFolderButton.UseVisualStyleBackColor = true;
            this.OpenFolderButton.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(30, 19);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(136, 25);
            this.SelectFolderButton.TabIndex = 1;
            this.SelectFolderButton.Text = "选择主目录";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtFolderPathTextBox
            // 
            this.txtFolderPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderPathTextBox.Location = new System.Drawing.Point(172, 19);
            this.txtFolderPathTextBox.Name = "txtFolderPathTextBox";
            this.txtFolderPathTextBox.ReadOnly = true;
            this.txtFolderPathTextBox.Size = new System.Drawing.Size(125, 25);
            this.txtFolderPathTextBox.TabIndex = 2;
            // 
            // UpdateFoldersNameButton
            // 
            this.UpdateFoldersNameButton.Location = new System.Drawing.Point(30, 50);
            this.UpdateFoldersNameButton.Name = "UpdateFoldersNameButton";
            this.UpdateFoldersNameButton.Size = new System.Drawing.Size(136, 27);
            this.UpdateFoldersNameButton.TabIndex = 5;
            this.UpdateFoldersNameButton.Text = "更新文件夹名称";
            this.UpdateFoldersNameButton.UseVisualStyleBackColor = true;
            this.UpdateFoldersNameButton.Click += new System.EventHandler(this.UpdateFoldersName_Click);
            // 
            // CompareClipboardContentButton
            // 
            this.CompareClipboardContentButton.ForeColor = System.Drawing.Color.Red;
            this.CompareClipboardContentButton.Location = new System.Drawing.Point(30, 83);
            this.CompareClipboardContentButton.Name = "CompareClipboardContentButton";
            this.CompareClipboardContentButton.Size = new System.Drawing.Size(267, 61);
            this.CompareClipboardContentButton.TabIndex = 6;
            this.CompareClipboardContentButton.Text = "自动匹配剪切板内容(已开启)";
            this.CompareClipboardContentButton.UseVisualStyleBackColor = true;
            this.CompareClipboardContentButton.Click += new System.EventHandler(this.CompareFolderNameButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ShowDirectoryTextBox
            // 
            this.ShowDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowDirectoryTextBox.Location = new System.Drawing.Point(172, 248);
            this.ShowDirectoryTextBox.Name = "ShowDirectoryTextBox";
            this.ShowDirectoryTextBox.ReadOnly = true;
            this.ShowDirectoryTextBox.Size = new System.Drawing.Size(125, 25);
            this.ShowDirectoryTextBox.TabIndex = 9;
            // 
            // ShowUpdateStateTextBox
            // 
            this.ShowUpdateStateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowUpdateStateTextBox.Location = new System.Drawing.Point(172, 52);
            this.ShowUpdateStateTextBox.Name = "ShowUpdateStateTextBox";
            this.ShowUpdateStateTextBox.ReadOnly = true;
            this.ShowUpdateStateTextBox.Size = new System.Drawing.Size(125, 25);
            this.ShowUpdateStateTextBox.TabIndex = 10;
            // 
            // ShowClipboardContentTextBox
            // 
            this.ShowClipboardContentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowClipboardContentTextBox.Location = new System.Drawing.Point(30, 218);
            this.ShowClipboardContentTextBox.Name = "ShowClipboardContentTextBox";
            this.ShowClipboardContentTextBox.ReadOnly = true;
            this.ShowClipboardContentTextBox.Size = new System.Drawing.Size(267, 25);
            this.ShowClipboardContentTextBox.TabIndex = 11;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(95, 280);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(140, 42);
            this.ResetButton.TabIndex = 12;
            this.ResetButton.Text = "清空剪切板";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // RenameButton
            // 
            this.RenameButton.ForeColor = System.Drawing.Color.Red;
            this.RenameButton.Location = new System.Drawing.Point(30, 150);
            this.RenameButton.Name = "RenameButton";
            this.RenameButton.Size = new System.Drawing.Size(267, 62);
            this.RenameButton.TabIndex = 13;
            this.RenameButton.Text = "重命名提醒(已开启)";
            this.RenameButton.UseVisualStyleBackColor = true;
            this.RenameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 340);
            this.Controls.Add(this.RenameButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.ShowClipboardContentTextBox);
            this.Controls.Add(this.ShowUpdateStateTextBox);
            this.Controls.Add(this.ShowDirectoryTextBox);
            this.Controls.Add(this.CompareClipboardContentButton);
            this.Controls.Add(this.UpdateFoldersNameButton);
            this.Controls.Add(this.txtFolderPathTextBox);
            this.Controls.Add(this.SelectFolderButton);
            this.Controls.Add(this.OpenFolderButton);
            this.Name = "Form1";
            this.Text = "QuickOpenFolder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFolderButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.TextBox txtFolderPathTextBox;
        private System.Windows.Forms.Button UpdateFoldersNameButton;
        private System.Windows.Forms.Button CompareClipboardContentButton;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox ShowDirectoryTextBox;
        private System.Windows.Forms.TextBox ShowUpdateStateTextBox;
        private System.Windows.Forms.TextBox ShowClipboardContentTextBox;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button RenameButton;
    }
}

