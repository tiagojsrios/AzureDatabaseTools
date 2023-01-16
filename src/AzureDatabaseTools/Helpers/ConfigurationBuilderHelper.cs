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
        string filePath = $"{Directory.GetCurrentDirectory()}\\appsettings.{environment}.json";
        Console.WriteLine(filePath);

        return configurationBuilder.AddJsonFile(filePath, optional: false);
    }

    internal static SqlConnectionStringBuilder GetDatabaseConnectionString(this IConfigurationRoot configurationRoot,
        string sectionName)
    {
        Console.WriteLine($"SectionName: {sectionName}");
        string connectionStringValue = configurationRoot.GetValue<string>(sectionName);
        Console.WriteLine($"{nameof(connectionStringValue)}: {connectionStringValue}");

        SqlConnectionStringBuilder databaseConnectionBuilder = new()
        {
            ConnectionString = connectionStringValue
        };

        return databaseConnectionBuilder;
    }
}