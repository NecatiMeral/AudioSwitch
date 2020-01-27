using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitch.Win.Properties;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.Design;
using DevExpress.XtraEditors;
using Microsoft.Extensions.Options;

namespace AudioSwitch.Win.Services
{
    class AppOptionsStartupTask : IStartupTask
    {
        readonly Settings settings;
        readonly DevExpressSkinService skinService;

        public AppOptionsStartupTask(Settings settings
            , DevExpressSkinService skinService)
        {
            this.settings = settings;
            this.skinService = skinService;
        }

        public Task RunAsync()
        {
            skinService.ActiveSkin = settings.Skin;
            WindowsFormsSettings.CompactUIMode = settings.CompactUi;
            return Task.CompletedTask;
        }
    }
}
