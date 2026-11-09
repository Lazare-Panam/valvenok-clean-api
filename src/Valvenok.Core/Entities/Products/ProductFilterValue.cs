namespace Valvenok.Core.Entities.Products
{
    public class ProductFilterValue
    {
        public int ProductFilterValueId { get; set; }
        public int ProductId { get; set; }  
        public int FilterId { get; set; }  
        public int CategoryId { get; set; } 
        public required string Value { get; set; }
    }
}
