using AzureDatabaseTools.Helpers;
using AzureDatabaseTools.Managers;
using AzureDatabaseTools.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace AzureDatabaseTools.Services;

public class SqlServerOperationService : IDatabaseOperationService
{
    /// <summary>
    ///     Reads the connection string from the appsettings file and exports a Sql Server database into a .bacpac file.
    /// </summary>
    /// <param name="sourceEnvironmentName">
    ///     Environment name that should be used to get the database connection string from the appsettings file.
    /// </param>
    /// <param name="sourceSection">
    ///     Section name where the connection string is located.
    /// </param>
    /// <exception cref="InvalidOperationException">
    ///     Exception thrown when the connection string does not have the property Database defined.
    /// </exception>
    public void Export(string sourceEnvironmentName, string sourceSection)
    {
        try
        {
            DbConnectionStringBuilder sourceDatabaseConnectionString = new ConfigurationBuilder()
                .AddJsonFileByEnvironment(sourceEnvironmentName)
                .Build()
                .GetDatabaseConnectionString(sourceSection);

            if (sourceDatabaseConnectionString["Database"] is not string sourceDatabaseName)
            {
                throw new InvalidOperationException("Could not find database property in the database connection string");
            }

            DacServicesManager.ExportDatabase(sourceDatabaseConnectionString, sourceDatabaseName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}