using Hollow_Launcher.pages.TitleWindow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;

namespace Hollow_Launcher.windows
{
    /// <summary>
    /// Interaction logic for TitleWindow.xaml
    /// </summary>
    public partial class TitleWindow : Window
    {
        private NotifyIcon _notifyIcon;
        private bool _isExit;

        HorrorGameLauncher HorrorGameLauncher { get; set; }
        public TitleWindow()
        {
            InitializeComponent();

            _notifyIcon = new NotifyIcon();

            string rootPath = Directory.GetCurrentDirectory();
            string iconPath = Path.Combine(rootPath, "Icon.ico");
            _notifyIcon.Icon = new Icon(iconPath);

            _notifyIcon.Visible = true;
            _notifyIcon.Text = "Hollow Launcher";

            _notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit", null, OnExit);

            _notifyIcon.DoubleClick += (s, e) => ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
        }

        private void OnExit(object sender, EventArgs e)
        {
            _isExit = true;
            _notifyIcon.Dispose();
            Close();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (WindowState == WindowState.Minimized)
            {
                this.Hide(); // Hide from taskbar
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                base.OnClosing(e);
            }
        }

        private void HorrorButton_Click(object sender, RoutedEventArgs e)
        {
            HorrorGameLauncher = new HorrorGameLauncher();
            horrorGameFrame.Content = HorrorGameLauncher;
        }
    }
}
