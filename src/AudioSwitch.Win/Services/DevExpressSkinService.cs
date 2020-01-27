using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.LookAndFeel;
using DevExpress.Mvvm;
using DevExpress.Skins;

namespace AudioSwitch.Win.Services
{
    class DevExpressSkinService
    {
        readonly IMessenger messenger;

        public List<string> AvailableSkins { get; set; }

        public string ActiveSkin
        {
            get => UserLookAndFeel.Default.ActiveSkinName;
            set => UserLookAndFeel.Default.SkinName = value;
        }

        public DevExpressSkinService(SkinManager skinManager, IMessenger messenger)
        {
            this.messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            var skins = new List<string>();
            foreach (SkinContainer container in skinManager.Skins)
            {
                skins.Add(container.SkinName);
            }

            AvailableSkins = skins;
        }
    }
}
