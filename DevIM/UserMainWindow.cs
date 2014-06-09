﻿using DevIM.custom;
using DevIM.Test;
using Fundation.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevIM
{
    public partial class UserMainWindow : Form
    {
        public UserMainWindow()
        {
            InitializeComponent();
        }

        private void btnTestSend_Click(object sender, EventArgs e)
        {
            (new TestSendFile()).ShowDialog();
        }

        private void UserMainWindow_Load(object sender, EventArgs e)
        {
            CustomConfig.GetSystemParameters();
            LogInterface.Listen(CustomConfig.LogDirectoryName.ToString());

        }

    }
}
