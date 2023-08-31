using System.Text.Json;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace WebApiRdsApp;

public static class SecretsManagerService
{
    public static async Task<string> GetConnectionStringFromSecret(DatabaseAndSecretConfiguration dbAndSecretConfig)
    {
        ////xxxxx
        // return "Server=localhost;Database=SchoolRecords;User Id=SA;Password=A!VeryComplex123Password;MultipleActiveResultSets=true;TrustServerCertificate=True;";
        // ///xxxxx
        IAmazonSecretsManager client = string.IsNullOrWhiteSpace(dbAndSecretConfig.Secret.Region)  
            ? new AmazonSecretsManagerClient() 
            : new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(dbAndSecretConfig.Secret.Region));

        GetSecretValueRequest getSecretValueRequest = new GetSecretValueRequest(){
            SecretId =  dbAndSecretConfig.Secret.Name,
            VersionStage = dbAndSecretConfig.Secret.VersionStage 
        };

        try
        {
            GetSecretValueResponse getSecretValueResponse = await client.GetSecretValueAsync(getSecretValueRequest);
            SecretModel sqlServerSecret = JsonSerializer.Deserialize<SecretModel>(getSecretValueResponse.SecretString);
            Console.WriteLine(sqlServerSecret.Host);
            return sqlServerSecret.BuildConnectionString(dbAndSecretConfig.Database.Name, dbAndSecretConfig.Database.MultipleActiveResultSets);
        }
        catch (Exception ex )
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            throw;
        }
    }
}