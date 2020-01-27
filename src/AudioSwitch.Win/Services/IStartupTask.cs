using System.Threading.Tasks;

namespace AudioSwitch.Win.Services
{
    interface IStartupTask
    {
        Task RunAsync();
    }
}