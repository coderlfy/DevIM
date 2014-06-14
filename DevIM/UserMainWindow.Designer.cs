namespace DevIM
{
    partial class UserMainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserMainWindow));
            this.btnTestSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestSend
            // 
            this.btnTestSend.Location = new System.Drawing.Point(99, 69);
            this.btnTestSend.Name = "btnTestSend";
            this.btnTestSend.Size = new System.Drawing.Size(102, 23);
            this.btnTestSend.TabIndex = 0;
            this.btnTestSend.Text = "发送文件给**";
            this.btnTestSend.UseVisualStyleBackColor = true;
            this.btnTestSend.Click += new System.EventHandler(this.btnTestSend_Click);
            // 
            // UserMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 371);
            this.Controls.Add(this.btnTestSend);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserMainWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserMainWindow";
            this.Load += new System.EventHandler(this.UserMainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestSend;
    }
}