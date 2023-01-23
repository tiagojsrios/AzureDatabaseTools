using AzureDatabaseTools.Helpers;
using AzureDatabaseTools.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureDatabaseTools.Commands;

internal sealed class ExportCommand : ConsoleAppBase
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

        try
        {
            IConfigurationRoot configurationBuilder = new ConfigurationBuilder()
                .AddJsonFileByEnvironment(environmentName, _logger)
                .Build();

            _databaseOperationService.Export(configurationBuilder, configurationSection);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error has occurred while exporting the database");
        }
    }

    /// <summary>
    ///     Application entry point to clone a database into Sql Express. This command can be used as the following:
    ///         database clone -v {Verbosity} -c {ConfigurationSection} -e {EnvironmentName} -d {DevelopmentEnvironmentName}
    /// </summary>
    public void Clone(
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
            shortName: "d",
            description: "Environment name used by your application for local development.",
            DefaultValue = "Development"
        )] string developmentEnvironmentName = "Development",

        [Option(
            shortName: "c",
            description: "Section name where the database connection string can be found within the appsettings file. To represent JSON nesting, use colon (:) as a separator.",
            DefaultValue = "ConnectionStrings:Default"
        )] string configurationSection = "ConnectionStrings:Default"
    )
    {
        _logger.LogDebug(message: "Verbosity argument is set to {LogLevel}", verbosity);
        _logger.LogDebug(message: "Environment name argument is set to {EnvironmentName}", environmentName);
        _logger.LogDebug(message: "Development environment name argument is set to {DevelopmentEnvironmentName}", developmentEnvironmentName);
        _logger.LogDebug(message: "Configuration section argument is set to {ConfigurationSection}", configurationSection);

        try
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFileByEnvironment(environmentName, _logger)
                .Build();

            IConfigurationRoot developmentConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFileByEnvironment(developmentEnvironmentName, _logger)
                .Build();

            _databaseOperationService.Clone(configurationRoot, developmentConfigurationRoot, configurationSection);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error has occurred while cloning the database");
        }
    }
}
