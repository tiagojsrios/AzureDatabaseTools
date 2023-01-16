using AzureDatabaseTools.Services.Interfaces;

namespace AzureDatabaseTools.Commands;

internal class ExportCommand : ConsoleAppBase
{
    private readonly IDatabaseOperationService _databaseOperationService;

    public ExportCommand(IDatabaseOperationService databaseExportService)
    {
        _databaseOperationService = databaseExportService;
    }

    public void Export(
        [Option(shortName: "s", description: "description", DefaultValue = "Test")] string sourceEnvironmentName = "Test",
        [Option(shortName: "ss", description: "", DefaultValue = "ConnectionStrings:Default")] string sourceSection = "ConnectionStrings:Default"
    )
    {
        _databaseOperationService.Export(sourceEnvironmentName, sourceSection);
    }
}