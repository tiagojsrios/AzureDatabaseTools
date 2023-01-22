using AzureDatabaseTools.Commands;
using AzureDatabaseTools.Helpers;
using AzureDatabaseTools.Managers;
using AzureDatabaseTools.Services;
using AzureDatabaseTools.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

string[] commandLineArgs = Environment.GetCommandLineArgs();
string? verbosity = LoggingHelper.GetMinimumLevelFromArguments(commandLineArgs);

ConsoleAppBuilder builder = ConsoleApp
    .CreateBuilder(args)
    .ConfigureLogging((_, logging) =>
    {
        logging.ClearProviders();
        logging.AddSimpleConsole(x =>
        {
            x.IncludeScopes = true;
        });
        logging.SetMinimumLevel(verbosity.ConvertStringToLogLevel());
    });

builder.ConfigureServices(services =>
{
    services.AddSingleton<IDatabaseOperationService, SqlServerOperationService>();
    services.AddSingleton<DacServicesManager>();
});

ConsoleApp application = builder.Build();

application.AddCommands<ExportCommand>();

await application.RunAsync();
