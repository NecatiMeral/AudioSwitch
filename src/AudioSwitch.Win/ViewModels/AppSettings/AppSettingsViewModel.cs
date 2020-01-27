using System;
using System.ComponentModel;
using AudioSwitch.Win.Properties;
using AudioSwitch.Win.Services;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Microsoft.Extensions.Options;

namespace AudioSwitch.Win.ViewModels.AppSettings
{
    class AppSettingsViewModel : ViewModelBase
    {
        readonly Settings settings;
        private readonly DevExpressSkinService skinService;

        public string Skin
        {
            get => GetProperty(() => Skin);
            set => SetProperty(() => Skin, value, OnSkinChanged);
        }

        public BindingList<string> AvailableSkins
        {
            get => GetProperty(() => AvailableSkins);
        }

        public DefaultBoolean CompactUi
        {
            get => GetProperty(() => CompactUi);
            set => SetProperty(() => CompactUi, value, onCompactUiChanged);
        }

        public IDelegateCommand SaveSettingsAndClose { get; }

        public AppSettingsViewModel(Settings settings, DevExpressSkinService skinService)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.skinService = skinService ?? throw new ArgumentNullException(nameof(skinService));

            SaveSettingsAndClose = new DelegateCommand(SaveSettingsAndCloseImpl);

            SetProperty(() => AvailableSkins, new BindingList<string>(skinService.AvailableSkins));
            LoadSettings();
        }

        void LoadSettings()
        {
            Skin = settings.Skin;
            CompactUi = settings.CompactUi;
        }

        void SaveSettingsAndCloseImpl()
        {
            settings.Skin = Skin;
            settings.CompactUi = CompactUi;
            settings.Save();
        }

        void OnSkinChanged(string previousValue)
        {
            skinService.ActiveSkin = Skin;
        }

        void onCompactUiChanged(DefaultBoolean previousValue)
        {
            WindowsFormsSettings.CompactUIMode = CompactUi;
        }
    }
}