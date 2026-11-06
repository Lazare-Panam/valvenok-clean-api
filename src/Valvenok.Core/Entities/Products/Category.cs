using System;
using System.Collections.Generic;
using System.Text;

namespace Valvenok.Core.Entities.Products
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }         
        public required string Name { get; set; }
        public required string Slug { get; set; }    
        public required string Path { get; set; }    
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
