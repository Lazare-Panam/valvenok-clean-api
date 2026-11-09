using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Valvenok.Core.Entities.Products;
using Valvenok.Infrastructure.Data.Config.EntityConfigurations;

namespace Valvenok.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Brands> Brands => Set<Brands>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<CatalogProduct> CatalogProducts => Set<CatalogProduct>();
        public DbSet<Filter> Filters => Set<Filter>();
        public DbSet<CategoryFilter> CategoryFilters => Set<CategoryFilter>();
        public DbSet<ProductFilterValue> ProductFilterValues => Set<ProductFilterValue>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
