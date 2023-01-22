using AzureDatabaseTools.Helpers;
using AzureDatabaseTools.Managers;
using AzureDatabaseTools.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.Common;

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
    /// <param name="environmentName">
    ///     Environment name that should be used to get the database connection string from the appsettings file.
    /// </param>
    /// <param name="configurationSection">
    ///     Section name where the connection string is located.
    /// </param>
    /// <exception cref="InvalidOperationException">
    ///     Exception thrown when the connection string does not have the property Database defined.
    /// </exception>
    public void Export(string environmentName, string configurationSection)
    {
        DbConnectionStringBuilder databaseConnectionString = new ConfigurationBuilder()
            .AddJsonFileByEnvironment(environmentName)
            .Build()
            .GetDatabaseConnectionString(configurationSection);

        if (databaseConnectionString["Database"] is not string sourceDatabaseName)
        {
            throw new InvalidOperationException("Could not find database property in the connection string");
        }

        try
        {
            string connectionString = databaseConnectionString.ToString();
            _logger.LogInformation(message: "Found the following connection string {ConnectionString}", connectionString);

            _dacServicesManager.ExportDatabase(connectionString, sourceDatabaseName);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error has occurred while exporting the database");
        }
    }
}
