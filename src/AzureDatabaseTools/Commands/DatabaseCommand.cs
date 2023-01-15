using AzureDatabaseTools.Services.Interfaces;

namespace AzureDatabaseTools.Commands;

[Command(commandName: "database")]
public class DatabaseCommand : ConsoleAppBase
{
    private readonly IDatabaseOperationService _databaseOperationService;

    public DatabaseCommand(IDatabaseOperationService databaseExportService)
    {
        _databaseOperationService = databaseExportService;
    }

    [Command(commandName: "export")]
    public void Export(
        [Option(shortName: "s", description: "description", DefaultValue = "Test")] string sourceEnvironmentName = "Test",
        [Option(shortName: "ss", description: "", DefaultValue = "ConnectionStrings:Default")] string sourceSection = "ConnectionStrings:Default"
    )
    {
        _databaseOperationService.Export(sourceEnvironmentName, sourceSection);
    }
}