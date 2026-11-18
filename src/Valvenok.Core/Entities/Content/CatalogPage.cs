namespace Valvenok.Core.Entities.Content
{
    public static class PageTypes
    {
        public const string Category = "category";
        public const string SubCategory = "subcategory";
        public const string Product = "product";
    }

    public class CatalogPage
    {
        public string? Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? ParentPath { get; set; }
        public List<string> Ancestors { get; set; } = [];
        public int Order { get; set; }
        public string Title { get; set; } = string.Empty;
        public Seo? Seo { get; set; }
        public CardSummary? Card { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public LandingContent? Landing { get; set; }
        public ProductContent? Product { get; set; }
    }

    public class CardSummary
    {
        public string? Label { get; set; }
        public string? Image { get; set; }
        public string? Metric { get; set; }
        public string? Blurb { get; set; }
    }

    public class LandingContent
    {
        public Hero? Hero { get; set; }
        public Intro? Intro { get; set; }
        public List<Stat> Stats { get; set; } = [];
        public PressureChart? Pressure { get; set; }
        public List<Group> Groups { get; set; } = [];
        public List<Benefit> Benefits { get; set; } = [];
        public List<WhichItem> Which { get; set; } = [];
        public List<FaqItem> Faq { get; set; } = [];
        public Cta? Cta { get; set; }
    }

    public class ProductContent
    {
        public string? Brand { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public List<string> Features { get; set; } = [];
        public List<ProductImage> Images { get; set; } = [];
        public List<SpecItem> Specs { get; set; } = [];
        public List<VariantColumn> VariantColumns { get; set; } = [];
        public List<ProductVariant> Variants { get; set; } = [];
        public List<ProductDocument> Documents { get; set; } = [];
        public List<FaqItem> Faq { get; set; } = [];
    }

    public class ProductImage
    {
        public string Url { get; set; } = string.Empty;
        public string? Alt { get; set; }
    }

    public class SpecItem
    {
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class VariantColumn
    {
        public string Key { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string? Unit { get; set; }
    }

    public class ProductVariant
    {
        public string Model { get; set; } = string.Empty;
        public Dictionary<string, string> Attributes { get; set; } = [];
    }

    public class ProductDocument
    {
        public string Label { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? Type { get; set; }
    }
}
