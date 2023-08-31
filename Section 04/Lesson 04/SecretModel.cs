using System.Text.Json.Serialization;

namespace WebApiRdsApp;

public class SecretModel
{

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("engine")]
    public string? Engine { get; set; }

    [JsonPropertyName("host")]
    public string Host { get; set; }

    [JsonPropertyName("port")]
    public long Port { get; set; }

    [JsonPropertyName("dbInstanceIdentifier")]
    public string? DbInstanceIdentifier { get; set; }

    public string BuildConnectionString(string databaseName, bool multipleActiveResultSets = true)
    {
        return $"Server={Host},{Port};Database={databaseName};User Id={Username};Password={Password};MultipleActiveResultSets={multipleActiveResultSets}";
    }
}