using System.Data.Common;
using AzureDatabaseTools.Helpers;
using AzureDatabaseTools.Managers;
using AzureDatabaseTools.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureDatabaseTools.Services;

public class SqlServerOperationService : IDatabaseOperationService
{
    private readonly DacServicesManager _dacServicesManager;
    private readonly ILogger<SqlServerOperationService> _logger;

    public SqlServerOperationService(DacServicesManager dacServicesManager, ILogger<SqlServerOperationService> logger)
    {
        _logger = logger;
        _dacServicesManager = dacServicesManager;
    }

    /// <summary>
    ///     Reads the connection string from the appsettings file and exports a Sql Server database into a .bacpac file.
    /// </summary>
    /// <param name="configurationRoot">
    ///     Environment name that should be used to get the database connection string from the appsettings file.
    /// </param>
    /// <param name="configurationSection">
    ///     Section name where the connection string is located.
    /// </param>
    /// <exception cref="InvalidOperationException">
    ///     Exception thrown when the connection string does not have the property Database defined.
    /// </exception>
    public void Export(IConfigurationRoot configurationRoot, string configurationSection)
    {
        DbConnectionStringBuilder databaseConnectionString = configurationRoot.GetDatabaseConnectionString(configurationSection, _logger);

        if (databaseConnectionString["Database"] is not string databaseName)
        {
            throw new InvalidOperationException(message: "Could not find database property in the connection string");
        }

        string connectionString = databaseConnectionString.ToString();
        _logger.LogDebug(message: "Found the following connection string {ConnectionString}", connectionString);

        _dacServicesManager.ExportDatabase(connectionString, databaseName);
    }

    public void Clone(IConfigurationRoot configurationRoot, IConfigurationRoot developmentConfigurationRoot, string configurationSection)
    {
        Export(configurationRoot, configurationSection);

        DbConnectionStringBuilder developmentDatabaseConnectionString = developmentConfigurationRoot.GetDatabaseConnectionString(configurationSection, _logger);

        if (developmentDatabaseConnectionString["Database"] is not string developmentDatabaseName)
        {
            throw new InvalidOperationException("Could not find database property in the connection string");
        }

        string developmentConnectionString = developmentDatabaseConnectionString.ToString();

        DbConnectionStringBuilder databaseConnectionString = configurationRoot.GetDatabaseConnectionString(configurationSection, _logger);

        _dacServicesManager.ImportDatabase(developmentConnectionString, databaseName: databaseConnectionString["Database"].ToString()!, developmentDatabaseName);
    }
}
