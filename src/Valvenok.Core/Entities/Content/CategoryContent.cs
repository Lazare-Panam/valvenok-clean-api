namespace Valvenok.Core.Entities.Content
{
    public class CategoryContent
    {
        public string? Id { get; set; }
        public int CategoryId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public Seo? Seo { get; set; }
        public Hero? Hero { get; set; }
        public Intro? Intro { get; set; }
        public List<Stat> Stats { get; set; } = [];
        public PressureChart? Pressure { get; set; }
        public List<Group> Groups { get; set; } = [];
        public List<Benefit> Benefits { get; set; } = [];
        public List<WhichItem> Which { get; set; } = [];
        public List<FaqItem> Faq { get; set; } = [];
        public Cta? Cta { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class Seo
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Canonical { get; set; }
        public string? OgImage { get; set; }
    }

    // A labelled link, reused by hero and cta buttons.
    public class CtaLink
    {
        public string? Label { get; set; }
        public string? Href { get; set; }
    }

    public class Hero
    {
        public string? Overline { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleAccent { get; set; }
        public string? Subtitle { get; set; }
        public string? Image { get; set; }
        public CtaLink? PrimaryCta { get; set; }
        public CtaLink? SecondaryCta { get; set; }
    }

    public class Intro
    {
        public string? Heading { get; set; }
        public List<string> Paragraphs { get; set; } = [];
    }

    // One stat tile, e.g. value "150,000", unit "psi", label "Highest rated fittings".
    public class Stat
    {
        public string Value { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public string Label { get; set; } = string.Empty;
    }

    // The animated pressure-range chart: a scale (MaxPsi) and the bars drawn on it.
    public class PressureChart
    {
        public int MaxPsi { get; set; }
        public List<PressureBar> Bars { get; set; } = [];
    }

    public class PressureBar
    {
        public string Name { get; set; } = string.Empty;
        public string? Note { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string? Color { get; set; }
    }

    // A product-family group (e.g. "Valves") holding its range cards.
    public class Group
    {
        public string Label { get; set; } = string.Empty;
        public string? Kicker { get; set; }
        public string? Title { get; set; }
        public string? Intro { get; set; }
        public string? Tint { get; set; }
        public List<Range> Ranges { get; set; } = [];
    }

    // One range card linking to a subcategory page.
    public class Range
    {
        public string Title { get; set; } = string.Empty;
        public string? Brands { get; set; }
        public string? Text { get; set; }
        public string? Pressure { get; set; }
        public string? Href { get; set; }
        public string? Image { get; set; }
    }

    public class Benefit
    {
        // Icon name the frontend maps to a component, e.g. "verified".
        public string? Icon { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }

    // "Which product for which application" guidance row.
    public class WhichItem
    {
        public string App { get; set; } = string.Empty;
        public string Pick { get; set; } = string.Empty;
        public string? Href { get; set; }
        public string? Image { get; set; }
    }

    public class FaqItem
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class Cta
    {
        public string? Heading { get; set; }
        public string? Text { get; set; }
        public CtaLink? EmailCta { get; set; }
        public CtaLink? QuoteCta { get; set; }
    }
}
