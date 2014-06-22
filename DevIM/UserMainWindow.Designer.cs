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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserMainWindow));
            this.imgListICO = new System.Windows.Forms.ImageList(this.components);
            this.tFriend = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // imgListICO
            // 
            this.imgListICO.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListICO.ImageStream")));
            this.imgListICO.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListICO.Images.SetKeyName(0, "male.png");
            this.imgListICO.Images.SetKeyName(1, "famale.png");
            this.imgListICO.Images.SetKeyName(2, "category.png");
            // 
            // tFriend
            // 
            this.tFriend.BackColor = System.Drawing.SystemColors.Menu;
            this.tFriend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tFriend.ImageIndex = 0;
            this.tFriend.ImageList = this.imgListICO;
            this.tFriend.Location = new System.Drawing.Point(0, 0);
            this.tFriend.Name = "tFriend";
            this.tFriend.SelectedImageIndex = 0;
            this.tFriend.Size = new System.Drawing.Size(225, 371);
            this.tFriend.TabIndex = 0;
            this.tFriend.DoubleClick += new System.EventHandler(this.tFriend_DoubleClick);
            // 
            // UserMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 371);
            this.Controls.Add(this.tFriend);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserMainWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DevIM";
            this.Load += new System.EventHandler(this.UserMainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgListICO;
        private System.Windows.Forms.TreeView tFriend;

    }
}