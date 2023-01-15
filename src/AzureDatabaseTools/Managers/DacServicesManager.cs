using Microsoft.SqlServer.Dac;
using System.Data.Common;

namespace AzureDatabaseTools.Managers;

public static class DacServicesManager
{
    public static void ExportDatabase(DbConnectionStringBuilder connectionString, string databaseName)
    {
        DacServices dac = new(connectionString.ToString());

        dac.ProgressChanged += (o, e) =>
        {
            Console.WriteLine(e.Message);
        };

        dac.ExportBacpac(packageFileName: $"../../../{databaseName}.bacpac", databaseName);
    }
}
