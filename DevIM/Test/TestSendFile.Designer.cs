namespace DevIM.Test
{
    partial class TestSendFile
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartSend = new System.Windows.Forms.Button();
            this.btnSelectSendFile = new System.Windows.Forms.Button();
            this.lbSendStatus = new System.Windows.Forms.Label();
            this.btnCancelSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(32, 51);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(284, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "发送状态：";
            // 
            // btnStartSend
            // 
            this.btnStartSend.Location = new System.Drawing.Point(323, 51);
            this.btnStartSend.Name = "btnStartSend";
            this.btnStartSend.Size = new System.Drawing.Size(75, 23);
            this.btnStartSend.TabIndex = 2;
            this.btnStartSend.Text = "开始发送";
            this.btnStartSend.UseVisualStyleBackColor = true;
            this.btnStartSend.Click += new System.EventHandler(this.btnStartSend_Click);
            // 
            // btnSelectSendFile
            // 
            this.btnSelectSendFile.Location = new System.Drawing.Point(404, 21);
            this.btnSelectSendFile.Name = "btnSelectSendFile";
            this.btnSelectSendFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSendFile.TabIndex = 3;
            this.btnSelectSendFile.Text = "选择文件";
            this.btnSelectSendFile.UseVisualStyleBackColor = true;
            this.btnSelectSendFile.Click += new System.EventHandler(this.btnSelectSendFile_Click);
            // 
            // lbSendStatus
            // 
            this.lbSendStatus.AutoSize = true;
            this.lbSendStatus.Location = new System.Drawing.Point(101, 26);
            this.lbSendStatus.Name = "lbSendStatus";
            this.lbSendStatus.Size = new System.Drawing.Size(77, 12);
            this.lbSendStatus.TabIndex = 4;
            this.lbSendStatus.Text = "没有文件发送";
            // 
            // btnCancelSend
            // 
            this.btnCancelSend.Location = new System.Drawing.Point(404, 50);
            this.btnCancelSend.Name = "btnCancelSend";
            this.btnCancelSend.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSend.TabIndex = 5;
            this.btnCancelSend.Text = "取消发送";
            this.btnCancelSend.UseVisualStyleBackColor = true;
            this.btnCancelSend.Click += new System.EventHandler(this.btnCancelSend_Click);
            // 
            // TestSendFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 92);
            this.Controls.Add(this.btnCancelSend);
            this.Controls.Add(this.lbSendStatus);
            this.Controls.Add(this.btnSelectSendFile);
            this.Controls.Add(this.btnStartSend);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestSendFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestSendFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartSend;
        private System.Windows.Forms.Button btnSelectSendFile;
        private System.Windows.Forms.Label lbSendStatus;
        private System.Windows.Forms.Button btnCancelSend;
    }
}