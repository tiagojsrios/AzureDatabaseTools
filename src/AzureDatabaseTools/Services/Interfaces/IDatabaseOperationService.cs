using Microsoft.Extensions.Configuration;

namespace AzureDatabaseTools.Services.Interfaces;

public interface IDatabaseOperationService
{
    void Export(IConfigurationRoot configurationRoot, string configurationSection);

    void Clone(IConfigurationRoot configurationRoot, IConfigurationRoot developmentConfigurationRoot, string configurationSection);
}
