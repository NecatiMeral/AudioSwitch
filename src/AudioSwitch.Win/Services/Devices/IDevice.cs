namespace AudioSwitch.Win.Services.Devices
{
    interface IDevice
    {
        string Id { get; }
        DeviceType Type { get; }
    }
}