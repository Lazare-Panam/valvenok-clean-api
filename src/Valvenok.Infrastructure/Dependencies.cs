using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Valvenok.Infrastructure.Data;
using Valvenok.Infrastructure.Settings;
using MongoDB.Driver;
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
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });
        }
    }
}
