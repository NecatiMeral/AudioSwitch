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
using DevExpress.Skins;
using Microsoft.Extensions.Options;
using DevExpress.Utils;
using AudioSwitch.Win.ViewModels.AppSettings;

namespace AudioSwitch.Win.Views
{
    partial class AppSettingsView : XtraForm
    {
        public AppSettingsView(Func<AppSettingsViewModel> viewModelFactory)
        {
            InitializeComponent();
            InitializeEditors();

            var viewModel = viewModelFactory();
            mvvmContext.SetViewModel(typeof(AppSettingsViewModel), viewModel);
            var fluent = mvvmContext.OfType<AppSettingsViewModel>();
            fluent.SetBinding(lookUpEditSkin.Properties, edit => edit.DataSource, vm => vm.AvailableSkins);
            fluent.SetBinding(lookUpEditSkin, edit => edit.EditValue, vm => vm.Skin);
            fluent.SetBinding(icbCompactUi, edit => edit.EditValue, vm => vm.CompactUi);

            btnOk.BindCommand(viewModel.SaveSettingsAndClose);
        }

        void InitializeEditors()
        {
            InitializeCompactUi();
        }

        void InitializeCompactUi()
        {
            icbCompactUi.Properties.AddEnum<DefaultBoolean>();
        }
    }
}
