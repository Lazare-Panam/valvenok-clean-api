using Microsoft.EntityFrameworkCore;
using Valvenok.Core.Entities.Products;
using Valvenok.Core.Entities.User;

namespace Valvenok.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<CatalogProduct> CatalogProducts => Set<CatalogProduct>();
        public DbSet<Filter> Filters => Set<Filter>();
        public DbSet<CategoryFilter> CategoryFilters => Set<CategoryFilter>();
        public DbSet<ProductFilterValue> ProductFilterValues => Set<ProductFilterValue>();
        public DbSet<Enquiry> Enquiries => Set<Enquiry>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
