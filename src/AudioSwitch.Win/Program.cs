using System;
using System.Windows.Forms;
using AudioSwitch.CoreAudioApi;
using AudioSwitch.Win.AutoMapper;
using AudioSwitch.Win.Properties;
using AudioSwitch.Win.Services;
using AudioSwitch.Win.Services.Device;
using AudioSwitch.Win.ViewModels.AppSettings;
using AudioSwitch.Win.ViewModels.Devices;
using AudioSwitch.Win.ViewModels.Switcher;
using AudioSwitch.Win.Views;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using DevExpress.Mvvm;
using DevExpress.Skins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AudioSwitch.Win
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder(args).Build();

            using (var form = host.Services.GetRequiredService<SwitcherView>())
            {
                var startupTasks = host.Services.GetServices<IStartupTask>();

                foreach (var task in startupTasks)
                {
                    task.RunAsync()
                        .GetAwaiter()
                        .GetResult();
                }
                Application.Run();
            }

            return 0;
        }

        static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
                {
                    // TODO: Find a more "modular" way to use AutoFac profiles
                    services.AddLogging();

                    // AutoMapper
                    services.AddSingleton((serviceProvider) =>
                    {
                        return new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>())
                            .CreateMapper();
                    });

                    // Settings
                    services.AddSingleton(Settings.Default);

                    // Core Audio Api
                    services.AddSingleton<MMDeviceEnumerator>();
                    services.AddSingleton<MMDeviceNotifyClient>();
                    services.AddSingleton<PolicyConfigClient>();

                    // Infrastructure
                    services.AddSingleton<IMessenger, Messenger>();

                    // Domain
                    services.AddTransient<DevicesRepository>();

                    // Application
                    services.AddTransient<DevicesService>();
                    services.AddTransient<DeviceIconProvider>();
                    services.AddSingleton<DevExpressSkinService>();
                    services.AddSingleton((sp) => SkinManager.Default);

                    // Services
                    services.AddTransient<IStartupTask, AppOptionsStartupTask>();

                    // UI
                    services.AddTransient<DevicesView>();
                    services.AddTransient<SwitcherView>();
                    services.AddTransient<SwitcherViewModel>();

                    //services.AddTransient<DevicesViewModel>();
                    services.AddTransient<PlaybackDevicesViewModel>();
                    services.AddTransient<RecordingDevicesViewModel>();
                    services.AddTransient<AppSettingsView>();
                    services.AddTransient<AppSettingsViewModel>();
                });
    }
}
