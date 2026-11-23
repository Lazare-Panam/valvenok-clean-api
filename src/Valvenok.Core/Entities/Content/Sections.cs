using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Valvenok.Core.Entities.Content
{
    // Two serializers, two discriminators:
    //   [BsonKnownTypes] -> MongoDB read/write (uses the "_t" field in the document)
    //   [JsonPolymorphic] -> HTTP response (emits a "type" field the frontend maps to a component)
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(HeroSection), "hero")]
    [JsonDerivedType(typeof(IntroSection), "intro")]
    [JsonDerivedType(typeof(StatsSection), "stats")]
    [JsonDerivedType(typeof(CirclesSection), "circles")]
    [JsonDerivedType(typeof(ChartSection), "chart")]
    [JsonDerivedType(typeof(CardsSection), "cards")]
    [JsonDerivedType(typeof(BenefitsSection), "benefits")]
    [JsonDerivedType(typeof(SelectorSection), "selector")]
    [JsonDerivedType(typeof(ChecklistSection), "checklist")]
    [JsonDerivedType(typeof(StandardsSection), "standards")]
    [JsonDerivedType(typeof(BrandsSection), "brands")]
    [JsonDerivedType(typeof(FaqSection), "faq")]
    [JsonDerivedType(typeof(CtaSection), "cta")]
    [BsonKnownTypes(typeof(HeroSection), typeof(IntroSection), typeof(StatsSection),
        typeof(CirclesSection), typeof(ChartSection), typeof(CardsSection),
        typeof(BenefitsSection), typeof(SelectorSection), typeof(ChecklistSection),
        typeof(StandardsSection), typeof(BrandsSection), typeof(FaqSection),
        typeof(CtaSection))]
    public abstract class Section
    {
    }

    public class SectionHeader
    {
        public string? Kicker { get; set; }
        public string? Title { get; set; }
        public string? Lead { get; set; }
    }

    public class HeroSection : Section
    {
        public string? Overline { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleAccent { get; set; }
        public string? Subtitle { get; set; }
        public string? Image { get; set; }
        public List<string> Tags { get; set; } = [];
        public BrandRef? Brand { get; set; }
        public List<BrandRef> ExternalBrands { get; set; } = [];
        public CtaLink? PrimaryCta { get; set; }
        public CtaLink? SecondaryCta { get; set; }
    }

    public class BrandRef
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Href { get; set; }
    }

    public class IntroSection : Section
    {
        public string? Heading { get; set; }
        public List<string> Paragraphs { get; set; } = [];
        public CtaLink? Link { get; set; }
    }

    public class StatsSection : Section
    {
        public List<Stat> Items { get; set; } = [];
    }

    public class CirclesSection : Section
    {
        public List<CirclePair> Items { get; set; } = [];
    }

    public class CirclePair
    {
        public string Top { get; set; } = string.Empty;
        public string Bottom { get; set; } = string.Empty;
    }

    public class ChartSection : Section
    {
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Unit { get; set; }
        public int Max { get; set; }
        public List<ChartBar> Bars { get; set; } = [];
    }

    public class ChartBar
    {
        public string Name { get; set; } = string.Empty;
        public string? Note { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string? Color { get; set; }
    }

    public class CardsSection : Section
    {
        public SectionHeader? Header { get; set; }
        public List<CategoryCard> Cards { get; set; } = [];
    }

    public class CategoryCard
    {
        public string Title { get; set; } = string.Empty;
        public string? Text { get; set; }
        public string? Metric { get; set; }
        public string? Series { get; set; }
        public string? Tint { get; set; }
        public string? Href { get; set; }
        public string? Image { get; set; }
    }

    public class BenefitsSection : Section
    {
        public SectionHeader? Header { get; set; }
        public List<Benefit> Items { get; set; } = [];
    }

    public class SelectorSection : Section
    {
        public SectionHeader? Header { get; set; }
        public string? ResultLabel { get; set; }
        public List<SelectorItem> Items { get; set; } = [];
    }

    public class SelectorItem
    {
        public string App { get; set; } = string.Empty;
        public string Pick { get; set; } = string.Empty;
        public string? Href { get; set; }
    }

    public class ChecklistSection : Section
    {
        public SectionHeader? Header { get; set; }
        public List<ChecklistItem> Items { get; set; } = [];
        public CtaLink? Link { get; set; }
    }

    public class ChecklistItem
    {
        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }

    public class StandardsSection : Section
    {
        public SectionHeader? Header { get; set; }
        public List<StandardRow> Rows { get; set; } = [];
    }

    public class StandardRow
    {
        public string Code { get; set; } = string.Empty;
        public string What { get; set; } = string.Empty;
        public string? Where { get; set; }
    }

    public class BrandsSection : Section
    {
        public SectionHeader? Header { get; set; }
        public List<BrandCard> Items { get; set; } = [];
    }

    public class BrandCard
    {
        public string Name { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string? Description { get; set; }
        public List<CtaLink> Links { get; set; } = [];
        public bool External { get; set; }
    }

    public class FaqSection : Section
    {
        public SectionHeader? Header { get; set; }
        public List<FaqItem> Items { get; set; } = [];
    }

    public class CtaSection : Section
    {
        public string? Heading { get; set; }
        public string? Text { get; set; }
        public CtaLink? PrimaryCta { get; set; }
        public CtaLink? SecondaryCta { get; set; }
    }
}
