using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevIM.icon
{
    class IconController
    {
        public Timer _StatusIconTimer = null;
        private bool _currentIconVisible = true;
        private WinIconStatus _iconStatus;

        public WinIconStatus _IconStatus
        {
            get { return _iconStatus; }
            set { _iconStatus = value; }
        }
        
        public void Bind(WinIconStatus iconstatus)
        {
            _IconStatus = iconstatus;

            _StatusIconTimer = new Timer();
            _StatusIconTimer.Interval = 300;

            _StatusIconTimer.Tick += _statusIconTimer_Tick;
            
        }

        private void _statusIconTimer_Tick(object sender, EventArgs e)
        {
            _IconStatus._systemNotifyIcon.Icon = 
                (_currentIconVisible) ? 
                IconCollector.Get(TIcon.NULL) : 
                IconCollector.Get(TIcon.Logining);

            _currentIconVisible = !(_currentIconVisible);
        }

        public void StartFlash()
        {
            //可根据uid来获取要闪动的ico
            _StatusIconTimer.Start();
            
        }



        public void Reset()
        {
            _StatusIconTimer.Stop();
            _IconStatus._systemNotifyIcon.Icon = IconCollector.Get(TIcon.NULL);
        }
    }
}
