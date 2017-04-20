namespace UpanCopy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.button_Hide = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(12, 12);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(204, 21);
            this.addressBox.TabIndex = 0;
            this.addressBox.Text = "D:\\UFD";
            // 
            // button_Hide
            // 
            this.button_Hide.Location = new System.Drawing.Point(222, 37);
            this.button_Hide.Name = "button_Hide";
            this.button_Hide.Size = new System.Drawing.Size(58, 23);
            this.button_Hide.TabIndex = 2;
            this.button_Hide.Text = "Hidden";
            this.button_Hide.UseVisualStyleBackColor = true;
            this.button_Hide.Click += new System.EventHandler(this.Button_Hide_Click_1);
            // 
            // button_Search
            // 
            this.button_Search.Location = new System.Drawing.Point(222, 10);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(58, 23);
            this.button_Search.TabIndex = 3;
            this.button_Search.Text = "浏览...";
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(12, 39);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(204, 21);
            this.statusBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 92);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.button_Hide);
            this.Controls.Add(this.addressBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Copy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.Button button_Hide;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.TextBox statusBox;
    }
}

