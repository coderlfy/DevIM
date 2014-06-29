/****************************************
***创建人：bhlfy
***创建时间：2013-08-28 08:00:29
***修改人：bhlfy
***修改时间：2014-01-22 22:14:20
***文件描述：软件状态栏图标组件封装。
*****************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
//using System.Windows;
using System.Windows.Forms;

namespace Fundation.Core
{
    public enum IconStatusMode
    { 
        Normal,
        Flash
    }
    public class WinIconStatus
    {
        /// <summary>
        /// 图标控件对象
        /// </summary>
        public NotifyIcon _systemNotifyIcon = null;
        /// <summary>
        /// 图表资源名（嵌入类资源名）
        /// </summary>
        private string _icoResourceName = "";
        /// <summary>
        /// ico上显示的文字内容
        /// </summary>
        private string _icoText = "";
        /// <summary>
        /// 主窗口对象
        /// </summary>
        private Form _mainFrm = null;
        /// <summary>
        /// 主窗口初始状态
        /// </summary>
        private FormWindowState _LastFrmState = FormWindowState.Maximized;

        private IconStatusMode _iconStatusMode = IconStatusMode.Normal;

        public EventHandler OnFlashEventHandler = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resourceName">ico（嵌入类）资源名</param>
        public WinIconStatus(string resourceName)
        {
            #region
            this._systemNotifyIcon = new NotifyIcon();

            this._icoResourceName = resourceName;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        public void SetIconStatusMode(IconStatusMode mode)
        {
            this._iconStatusMode = mode;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resourceName">ico（嵌入类）资源名</param>
        /// <param name="icoText">图标上的文字</param>
        public WinIconStatus(string resourceName, string icoText)
            :this(resourceName)
        {
            #region
            this._icoText = icoText;
            #endregion
        }
        /// <summary>
        /// 托盘弹出菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nIconMenuItem_Click(object sender
            , System.EventArgs e)
        {
            #region
            this._systemNotifyIcon.Visible = false;

            System.Environment.Exit(0);
            #endregion
        }

        /// <summary>
        /// 双击托盘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nIconSystem_DoubleClick(object sender
            , System.EventArgs e)
        {
            #region
            if (_iconStatusMode == IconStatusMode.Normal)
            {
                if (this._mainFrm.Visible)
                {
                    _LastFrmState = this._mainFrm.WindowState;
                    this._mainFrm.Hide();
                }
                else
                {
                    this._mainFrm.Show();

                    this._mainFrm.WindowState = _LastFrmState;
                    this._mainFrm.Activate();
                }
            }
            else
            {
                if (OnFlashEventHandler != null)
                    OnFlashEventHandler(null, null);
            }
            #endregion
        }

        /// <summary>
        /// 窗口大小变更时记录窗口状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender
            , EventArgs e)
        {
            #region
            if (_mainFrm.WindowState == FormWindowState.Minimized)
                _mainFrm.Hide();
            else
                _LastFrmState = _mainFrm.WindowState;
            #endregion
        }

        /// <summary>
        /// 主窗体在加入状态栏图标功能后绑定的窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender
            , CancelEventArgs e)
        {
            #region
            e.Cancel = true;
            _LastFrmState = _mainFrm.WindowState;

            _mainFrm.WindowState = FormWindowState.Minimized;
            #endregion
        }

        /// <summary>
        /// 窗口状态改变时的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_StateChange(object sender
            , EventArgs e)
        {
            #region
            if (_mainFrm.WindowState == FormWindowState.Minimized)
                _mainFrm.Hide();
            #endregion
        }

        /// <summary>
        /// 处理资源名
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private string filterResourceName(string assemblyName)
        {
            #region
            int findindex = this._icoResourceName.IndexOf(assemblyName+".");
            string resource = this._icoResourceName;

            if (findindex == -1)
                resource = string.Format("{0}.{1}", assemblyName, resource);

            ExtConsole.Write(resource);
            return resource;
            #endregion
        }

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <returns></returns>
        private Icon getIcon()
        {
            #region
            Assembly asm = Assembly.GetAssembly(this._mainFrm.GetType());
            string resource = filterResourceName(asm.GetName().Name);

            Stream manifestResourceStream = asm
                .GetManifestResourceStream(resource);

            if (manifestResourceStream == null)
            {
                ExtConsole.Write("设置状态栏ico图标出错，请确认！");
                return new Icon(SystemIcons.Exclamation, 16, 16);
            }

            return new Icon(manifestResourceStream);

            #endregion
        }

        /// <summary>
        /// 配置右下角图标
        /// </summary>
        private void setIconProperty()
        {
            #region
            string icotext = string.IsNullOrEmpty(this._icoText)
                ?this._mainFrm.Text
                :this._icoText;

            this._systemNotifyIcon.Icon = getIcon();
            this._systemNotifyIcon.Visible = true;
            this._systemNotifyIcon.Text = icotext;

            #endregion
        }

        /// <summary>
        /// 将本体绑定到主程序
        /// </summary>
        /// <param name="mainFrm"></param>
        public void BindToWindow(Form mainFrm)
        {
            #region

            object temp = this._mainFrm;
            this._mainFrm = mainFrm;
            this.bindWindowEvent();

            if (temp == null)
            {
                this.setIconProperty();
                this.bindIconEvent();
            }
            #endregion
        }

        /// <summary>
        /// 绑定主窗体受icon组件影响的事件
        /// </summary>
        private void bindWindowEvent()
        {
            #region
            _mainFrm.SizeChanged += this.MainForm_SizeChanged;
            _mainFrm.Closing += this.MainForm_FormClosing;
            //_mainFrm.SystemColorsChanged += new EventHandler(this.MainForm_StateChange);
            #endregion
        }

        /// <summary>
        /// 绑定icon相关事件
        /// </summary>
        private void bindIconEvent()
        {
            #region
            //设置托盘菜单事件
            MenuItem nIconMenuItem = new MenuItem("退出本软件");
            nIconMenuItem.Click += new EventHandler(this.nIconMenuItem_Click);

            this._systemNotifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { nIconMenuItem });
            this._systemNotifyIcon.DoubleClick += new EventHandler(this.nIconSystem_DoubleClick);
            #endregion
        }
    }
}
