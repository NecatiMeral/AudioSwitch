using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AudioSwitch.Win.ViewModels.Devices;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Base;
using AudioSwitch.Win.ViewModels.Switcher;
using DevExpress.Utils.Animation;

namespace AudioSwitch.Win.Views
{
    partial class DevicesView : XtraUserControl
    {
        readonly IWaitingIndicatorProperties waitingIndicatorProperties;

        DevicesViewModel viewModel;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DevicesViewModel ViewModel
        {
            get => viewModel;
            set => SetupViewModel(value);
        }

        public DevicesView()
        {
            InitializeComponent();
            waitingIndicatorProperties = new BarWaitingIndicatorProperties() { ContentAlignment = ContentAlignment.MiddleCenter, Caption = "", Description = "" };
        }

        void SetupViewModel(DevicesViewModel value)
        {
            viewModel = value;
            mvvmContext?.Dispose();
            mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(components)
            {
                ContainerControl = this
            };
            mvvmContext.SetViewModel(typeof(DevicesViewModel), value);
            var fluent = mvvmContext.OfType<DevicesViewModel>();

            fluent.SetBinding(grid, grid => grid.DataSource, x => x.Devices);
            fluent.WithEvent<TileView, FocusedRowObjectChangedEventArgs>(view, nameof(TileView.FocusedRowObjectChanged))
                .SetBinding(x => x.DefaultDevice
                    , args => args.Row as DeviceViewModel
                    , (view, entity) => view.FocusedRowHandle = view.FindRow(entity));

            fluent.SetTrigger(vm => vm.UpdateDevices.IsExecuting, (isExecuting) =>
            {
                if (isExecuting)
                {
                    transitionManager.StartWaitingIndicator(this, waitingIndicatorProperties);
                }
                else
                {
                    transitionManager.StopWaitingIndicator();
                }
            });
        }

        protected override void OnFirstLoad()
        {
            base.OnFirstLoad();
            viewModel?.UpdateDevices?.Execute(null);
        }
    }
}
