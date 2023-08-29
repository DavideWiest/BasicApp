namespace Modules.Db;

using Newtonsoft.Json;
using Npgsql;
using Data.Db;


public static class DbManager
{

    public static string ConStr = string.Empty;

    private static NpgsqlDataSourceBuilder DataSourceBuilder = new NpgsqlDataSourceBuilder(ConStr);
    private static NpgsqlConnection Con;

    public static async Task Connect(string conStr)
    {
        ConStr = conStr;
        var dataSource = DataSourceBuilder.Build();
        Con = await dataSource.OpenConnectionAsync();
    }

    public static void Disconnect()
    {
        Con.Dispose();
    }

    public static async Task ExecuteDbProcedure(string query)
    {
        await using (var cmd = new NpgsqlCommand(query, Con))
        {
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
