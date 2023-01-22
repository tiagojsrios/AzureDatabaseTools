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
    ///         database export -v {Verbosity} -c {ConfigurationSection} -e {EnvironmentName}
    /// </summary>
    public void Export(
        [Option(
            shortName: "v", 
            description: "Sets the minimum level used by the Microsoft logging framework. Supported values are Trace, Debug, Information, Warning, Error and Critical. ", 
            DefaultValue = "Information"
        )] string verbosity = "Information",
        
        [Option(
            shortName: "e",
            description: "Environment name used by your application, where the connection string from the upstream database can be found."
        )] string? environmentName = null,

        [Option(
            shortName: "c", 
            description: "Section name where the database connection string can be found within the appsettings file. To represent JSON nesting, use colon (:) as a separator.", 
            DefaultValue = "ConnectionStrings:Default"
        )] string configurationSection = "ConnectionStrings:Default"
    )
    {
        _logger.LogDebug(message: "Verbosity argument is set to {LogLevel}", verbosity);
        _logger.LogDebug(message: "Environment name argument is set to {EnvironmentName}", environmentName);
        _logger.LogDebug(message: "Configuration section argument is set to {ConfigurationSection}", configurationSection);
        
        _databaseOperationService.Export(environmentName, configurationSection);
    }

    /// <summary>
    ///     Application entry point to clone a database into Sql Express. This command can be used as the following:
    ///         database clone -v {Verbosity} -c {ConfigurationSection} -e {EnvironmentName} -d {DevelopmentEnvironmentName}
    /// </summary>
    public void Clone(
        [Option(shortName: "v", description: "description", DefaultValue = "Information")] string verbosity = "Information",
        [Option(shortName: "e", description: "description", DefaultValue = "Test")] string environmentName = "Test",
        [Option(shortName: "c", description: "", DefaultValue = "ConnectionStrings:Default")] string configurationSection = "ConnectionStrings:Default",
        [Option(shortName: "d", description: "", DefaultValue = "Development")] string developmentEnvironmentName = "Development"
    )
    {
    }
}
