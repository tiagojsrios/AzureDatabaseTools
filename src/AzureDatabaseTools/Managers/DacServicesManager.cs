using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Dac;

namespace AzureDatabaseTools.Managers;

public class DacServicesManager
{
    private readonly ILogger<DacServicesManager> _logger;

    public DacServicesManager(ILogger<DacServicesManager> logger)
    {
        _logger = logger;
    }

    public void ExportDatabase(string connectionString, string databaseName)
    {
        DacServices dac = new(connectionString);

        dac.ProgressChanged += (o, e) =>
        {
            _logger.LogDebug(message: "{OperationId} | {Status} | {Message}",
                e.OperationId, e.Status.ToString(), e.Message);
        };

        string bacpacFileLocation = $"{Directory.GetCurrentDirectory()}\\{databaseName}.bacpac";
        _logger.LogInformation("Extracting database to the following file {FileLocation}", bacpacFileLocation);

        dac.ExportBacpac(packageFileName: bacpacFileLocation, databaseName);
    }

    public void ImportDatabase(string connectionString, string databaseName, string developmentDatabaseName)
    {
        DacServices dac = new(connectionString);

        dac.ProgressChanged += (o, e) =>
        {
            _logger.LogDebug(message: "{OperationId} | {Status} | {Message}",
                e.OperationId, e.Status.ToString(), e.Message);
        };

        string bacpacFileLocation = $"{Directory.GetCurrentDirectory()}\\{databaseName}.bacpac";
        _logger.LogInformation("Loading database from the following file {FileLocation}", bacpacFileLocation);

        using BacPackage bacPackage = BacPackage.Load(bacpacFileLocation);
        dac.ImportBacpac(bacPackage, developmentDatabaseName);
    }
}
