namespace Valvenok.Core.Entities.Products
{
    public class Brands
    {
        public int BrandId { get; set; }
        public required string Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? WebsiteLink { get; set; }
    }
}
