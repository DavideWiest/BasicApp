using Npgsql;

namespace BasicApp.Modules.Db;

public static class NpgSqlDbManager
{

    public static string? ConStr;
    private static NpgsqlDataSourceBuilder? DataSourceBuilder;
    private static NpgsqlConnection? Con;

    public static async Task<bool> Connect(string conStr)
    {
        ConStr = conStr;
        try
        {
            DataSourceBuilder = new NpgsqlDataSourceBuilder(ConStr);
            var dataSource = DataSourceBuilder.Build();
            Con = await dataSource.OpenConnectionAsync();
            return true;
        }
        catch { return false; }
    }

    public static void Disconnect()
    {
        Con?.Dispose();
    }

    public static async Task<NpgsqlDataReader> ExecuteDbQuery(string query, Dictionary<string, string> DataPairs)
    {
        await using var cmd = new NpgsqlCommand(query, Con);
        foreach (var pair in DataPairs)
        {
            cmd.Parameters.Add(new NpgsqlParameter(pair.Key, pair.Value));
        }
        return await cmd.ExecuteReaderAsync();
    }

    public static async Task<int> ExecuteDbProcedure(string query, Dictionary<string, string> DataPairs)
    {
        await using var cmd = new NpgsqlCommand(query, Con);
        foreach (var pair in DataPairs)
        {
            cmd.Parameters.Add(new NpgsqlParameter(pair.Key, pair.Value));
        }
        return await cmd.ExecuteNonQueryAsync();
    }
}