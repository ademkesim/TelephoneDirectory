using ReportService.Api.Core.Application.Repository;

namespace ReportService.Api.Extensions
{
    public static class MongoRegistration
    {
        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
           IConfiguration configuration)
        {
            return services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
                options.Database = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
            });
        }
    }
}
