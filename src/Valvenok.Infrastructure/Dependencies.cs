using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Valvenok.Infrastructure.Data;
using MongoDB.Driver;
using Valvenok.Core.Interfaces.Repositories;
using Valvenok.Infrastructure.Repository;
using Valvenok.Infrastructure.Data.Config.Settings;
namespace Valvenok.Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is missing. Set 'ConnectionStrings:DefaultConnection' " + "in user-secrets or the 'ConnectionStrings__DefaultConnection' environment variable.");
            }
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));
            var mongoConn = configuration.GetSection("MongoDB")["ConnectionString"];
            if (string.IsNullOrEmpty(mongoConn))
            {
                throw new InvalidOperationException(
                    "MongoDB connection string is missing. Set 'MongoDB:ConnectionString' in user-secrets " +
                    "or the 'MongoDB__ConnectionString' environment variable.");
            }
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return sp.GetRequiredService<IMongoClient>().GetDatabase(settings.DatabaseName);
            });
            services.AddScoped<ICategoryContentRepository, CategoryContentRepository>();
        }
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                    .AddMongoDb(clientFactory: sp => sp.GetRequiredService<IMongoClient>())
                    .AddDbContextCheck<ApplicationDbContext>(); ;
            return services;
        }
    }
}
