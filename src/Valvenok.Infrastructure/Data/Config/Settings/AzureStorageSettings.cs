namespace Valvenok.Infrastructure.Data.Config.Settings
{
    public class AzureStorageSettings
    {
        public const string SectionName = "AzureStorage";
        public string ConnectionString { get; set; } = string.Empty;
        public string ContainerName { get; set; } = "enquiries-valvenok";
    }
}
