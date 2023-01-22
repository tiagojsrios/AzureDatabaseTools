using AzureDatabaseTools.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureDatabaseTools.Commands;

internal class ExportCommand : ConsoleAppBase
{
    private readonly IDatabaseOperationService _databaseOperationService;
    private readonly ILogger<ExportCommand> _logger;

    public ExportCommand(IDatabaseOperationService databaseExportService, ILogger<ExportCommand> logger)
    {
        _databaseOperationService = databaseExportService;
        _logger = logger;
    }

    /// <summary>
    ///     Application entry point to create a database export in bacpac format. This command can be used as the following:
    ///         database export -c {ConfigurationSection} -e {EnvironmentName}
    /// </summary>
    public void Export(
        [Option(shortName: "v", description: "description", DefaultValue = "Test")] string verbosity = "Information",
        [Option(shortName: "e", description: "description", DefaultValue = "Test")] string environmentName = "Test",
        [Option(shortName: "c", description: "", DefaultValue = "ConnectionStrings:Default")] string configurationSection = "ConnectionStrings:Default"
    )
    {
        _databaseOperationService.Export(environmentName, configurationSection);
    }

    /// <summary>
    ///     Application entry point to clone a database into Sql Express. This command can be used as the following:
    ///         database clone -c {ConfigurationSection} -e {EnvironmentName}
    /// </summary>
    public void Clone(
        [Option(shortName: "e", description: "description", DefaultValue = "Test")] string environmentName = "Test",
        [Option(shortName: "c", description: "", DefaultValue = "ConnectionStrings:Default")] string configurationSection = "ConnectionStrings:Default"
    )
    {
    }
}
