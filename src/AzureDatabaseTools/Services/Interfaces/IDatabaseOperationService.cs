namespace AzureDatabaseTools.Services.Interfaces;

public interface IDatabaseOperationService
{
    void Export(string sourceEnvironmentName, string sourceSection);
}