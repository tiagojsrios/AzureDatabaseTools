using AzureDatabaseTools.Commands;
using AzureDatabaseTools.Services;
using AzureDatabaseTools.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

ConsoleAppBuilder builder = ConsoleApp.CreateBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddSingleton<IDatabaseOperationService, SqlServerOperationService>();
});

ConsoleApp application = builder.Build();

application.AddSubCommands<DatabaseCommand>();

await application.RunAsync();