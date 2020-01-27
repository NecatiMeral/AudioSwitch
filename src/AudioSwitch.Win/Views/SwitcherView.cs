using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AudioSwitch.Win.Properties;
using AudioSwitch.Win.Services;
using AudioSwitch.Win.ViewModels.Switcher;
using DevExpress.Utils.Animation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Tile;

namespace AudioSwitch.Win.Views
{
    partial class SwitcherView : XtraForm
    {
        readonly Settings settings;
        readonly Func<AppSettingsView> configFormFactory;
        readonly SwitcherViewModel viewModel;

        public SwitcherView(Settings settings
            , Func<SwitcherViewModel> viewModelFactory
            , Func<AppSettingsView> configFormFactory)
        {
            InitializeComponent();

            this.settings = settings;
            this.configFormFactory = configFormFactory;
            viewModel = viewModelFactory();
            playbackView.ViewModel = viewModel.Playback;
            recordingView.ViewModel = viewModel.Recording;

            notifyIcon.Icon = Icon.FromHandle(Resources._33_66.GetHicon());

            if (this.settings.AlwaysVisible)
            {
                Show();
                Activate();
            }
            else
            {
                barManager.ForceInitialize();
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            if (!settings.AlwaysVisible)
            {
                Hide();
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var point = WindowPosition.GetWindowPosition(notifyIcon, Width, Height);
            Left = point.X;
            Top = point.Y;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = true;
            Hide();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                Activate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                pmTrayMenu.ShowPopup(MousePosition);
            }
        }

        void barManager_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item == bbiSettings)
            {
                showConfigurationForm();
            }
            else if (e.Item == bbiAudioDevices)
            {

            }
            if (e.Item == bbiExit)
            {
                Application.Exit();
            }
        }

        void showConfigurationForm()
        {
            var configForm = configFormFactory();
            configForm.Show(this);
        }
    }
}
