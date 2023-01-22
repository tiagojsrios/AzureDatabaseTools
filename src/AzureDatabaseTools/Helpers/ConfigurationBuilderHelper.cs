using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureDatabaseTools.Helpers;

internal static class ConfigurationBuilderHelper
{
    internal static IConfigurationBuilder AddJsonFileByEnvironment(this IConfigurationBuilder configurationBuilder,
        string? environment, ILogger logger)
    {
        if (environment is not null)
        {
            environment = $".{environment}";
        }

        string filePath = $"{Directory.GetCurrentDirectory()}\\appsettings{environment}.json";

        configurationBuilder = configurationBuilder.AddJsonFile(filePath, optional: false);
        logger.LogDebug(message: "Added {FilePath} file to the configuration builder", filePath);

        return configurationBuilder;
    }

    internal static SqlConnectionStringBuilder GetDatabaseConnectionString(this IConfigurationRoot configurationRoot,
        string sectionName, ILogger logger)
    {
        string? connectionStringValue = configurationRoot.GetValue<string>(sectionName);

        if (string.IsNullOrEmpty(connectionStringValue))
        {
            throw new InvalidOperationException("Connection string cannot be null, neither empty");
        }

        SqlConnectionStringBuilder databaseConnectionBuilder = new()
        {
            ConnectionString = connectionStringValue
        };

        return databaseConnectionBuilder;
    }
}