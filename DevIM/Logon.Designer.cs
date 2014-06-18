namespace DevIM
{
    partial class Logon
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logon));
            this.btnUserLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbServerIP = new System.Windows.Forms.TextBox();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.tbUserPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUserLogin
            // 
            this.btnUserLogin.Location = new System.Drawing.Point(291, 34);
            this.btnUserLogin.Name = "btnUserLogin";
            this.btnUserLogin.Size = new System.Drawing.Size(65, 75);
            this.btnUserLogin.TabIndex = 0;
            this.btnUserLogin.Text = "登录";
            this.btnUserLogin.UseVisualStyleBackColor = true;
            this.btnUserLogin.Click += new System.EventHandler(this.btnUserLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户姓名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务器地址：";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(111, 61);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(174, 21);
            this.tbUserName.TabIndex = 3;
            // 
            // tbServerIP
            // 
            this.tbServerIP.Location = new System.Drawing.Point(111, 34);
            this.tbServerIP.Name = "tbServerIP";
            this.tbServerIP.Size = new System.Drawing.Size(113, 21);
            this.tbServerIP.TabIndex = 4;
            this.tbServerIP.Text = "192.168.159.100";
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(230, 34);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(55, 21);
            this.tbServerPort.TabIndex = 7;
            this.tbServerPort.Text = "1005";
            // 
            // tbUserPwd
            // 
            this.tbUserPwd.Location = new System.Drawing.Point(111, 88);
            this.tbUserPwd.Name = "tbUserPwd";
            this.tbUserPwd.PasswordChar = '*';
            this.tbUserPwd.Size = new System.Drawing.Size(174, 21);
            this.tbUserPwd.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "密    码：";
            // 
            // Logon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 143);
            this.Controls.Add(this.tbUserPwd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbServerPort);
            this.Controls.Add(this.tbServerIP);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUserLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Logon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DevIM-开发者局域网内即时通讯工具";
            this.Load += new System.EventHandler(this.Logon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUserLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbServerIP;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.TextBox tbUserPwd;
        private System.Windows.Forms.Label label3;
    }
}

