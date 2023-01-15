using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AzureDatabaseTools.Helpers;

internal static class ConfigurationBuilderHelper
{
    internal static IConfigurationBuilder Create()
    {
        return new ConfigurationBuilder();
    }

    internal static IConfigurationBuilder AddJsonFileByEnvironment(this IConfigurationBuilder configurationBuilder,
        string environment)
    {
        return configurationBuilder.AddJsonFile($"appsettings.{environment}.json", optional: false);
    }

    internal static SqlConnectionStringBuilder GetDatabaseConnectionString(this IConfigurationRoot configurationRoot,
        string sectionName)
    {
        SqlConnectionStringBuilder databaseConnectionBuilder = new()
        {
            ConnectionString = configurationRoot.GetValue<string>(sectionName)
        };

        return databaseConnectionBuilder;
    }
}