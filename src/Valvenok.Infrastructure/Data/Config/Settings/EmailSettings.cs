namespace Valvenok.Infrastructure.Data.Config.Settings
{
    public class EmailSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string SenderAddress { get; set; } = string.Empty;
        public string InternalAddressEmail { get; set; } = string.Empty;
    }
}
