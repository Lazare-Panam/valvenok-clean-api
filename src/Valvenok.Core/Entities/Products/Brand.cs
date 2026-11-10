using Valvenok.Core.Interfaces;

namespace Valvenok.Core.Entities.Products
{
    public class Brand : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string? LogoUrl { get; private set; }
        public string? WebsiteLink { get; private set; }
        public Brand(string name, string? logoUrl, string? websiteLink)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            Name = name.Trim();
            LogoUrl = logoUrl;
            WebsiteLink = websiteLink;
        }
    }
}
