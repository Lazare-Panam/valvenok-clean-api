namespace Valvenok.Core.Entities.Products
{
    public class CatalogProduct
    {
        public int CatalogProductId { get; set; }
        public required string PartNumber { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; } = 1;
        public string? ProductStatus { get; set; }
        public string? FamilyDescription { get; set; }
        public string? DetailDescription { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? Price { get; set; }
    }
}
