namespace AzureDatabaseTools.Services.Interfaces;

public interface IDatabaseOperationService
{
    void Export(string? environmentName, string sourceSection);
}