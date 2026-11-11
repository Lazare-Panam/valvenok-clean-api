using Azure.Storage.Queues;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Valvenok.Core.Interfaces;
using Valvenok.Core.Interfaces.Repositories;
using Valvenok.Infrastructure.Data;
using Valvenok.Infrastructure.Data.Config.Settings;
using Valvenok.Infrastructure.Messaging;
using Valvenok.Infrastructure.Repository;
using Valvenok.Infrastructure.Storage;
namespace Valvenok.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection  ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
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
            return services;
        }
       
        public static IServiceCollection ConfigureAppHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                    .AddMongoDb(clientFactory: sp => sp.GetRequiredService<IMongoClient>())
                    .AddDbContextCheck<ApplicationDbContext>(); 
            return services;
        }
        public static IServiceCollection ConfigureAzure(this IServiceCollection services, IConfiguration configuration)
        {
            var storageConnection = configuration.GetSection("AzureStorage")["ConnectionString"];
            var queueConnection = configuration.GetSection("Queue")["ConnectionString"];
            var queueName = configuration.GetSection("Queue")["QueueName"];
           
            if (string.IsNullOrEmpty(storageConnection))
                throw new InvalidOperationException(
                    "Azure Storage connection string is missing. Set 'AzureStorage:ConnectionString' in user-secrets " +
                    "or the 'AzureStorage__ConnectionString' environment variable.");
            if (string.IsNullOrEmpty(queueConnection))
                throw new InvalidOperationException(
                    "Queue connection string is missing. Set 'Queue:ConnectionString' in user-secrets " +
                    "or the 'Queue__ConnectionString' environment variable.");
            if (string.IsNullOrEmpty(queueName))
                throw new InvalidOperationException(
                    "Queue name is missing. Set 'Queue:QueueName' in appsettings.json.");
           
            services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorage"));
            services.Configure<QueueSettings>(configuration.GetSection("Queue"));

            services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddBlobServiceClient(storageConnection);
                clientBuilder.AddClient<QueueClient, QueueClientOptions>((options, _, _) =>
                {
                    options.MessageEncoding = QueueMessageEncoding.Base64;
                    return new QueueClient(queueConnection, queueName, options);
                });
            });

          
            services.AddScoped<IFileStorage, AzureBlobStorage>();
            services.AddScoped<INotificationPublisher, EmailQueuePublisher>();

            return services;
        }
    }
}
