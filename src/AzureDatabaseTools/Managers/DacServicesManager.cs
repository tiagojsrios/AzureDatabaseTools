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
            _logger.LogDebug(e.Message);
        };
        
        dac.ExportBacpac(packageFileName: $"./{databaseName}.bacpac", databaseName);
    }
}
