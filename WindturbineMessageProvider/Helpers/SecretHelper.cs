using System.Xml.Linq;

public static class SecretManager
{
    private const string SECRETS_PATH = ".\\secrets.xml";
    private static Dictionary<string, string> LoadSecrets()
    {
        var secretsDocument = XDocument.Load(SECRETS_PATH);
        var secrets = secretsDocument.Descendants("Secret");
        var output = new Dictionary<string, string>();
        foreach (var secret in secrets)
        {
            var key = secret.Attribute("key").Value;
            var value = secret.Attribute("value").Value;
            output[key] = value;
        }
        return output;
    }

    public static string GetSecret(string key)
    {
        if (LoadSecrets().TryGetValue(key, out string value))
        {
            return value;
        }
        throw new ArgumentException($"Secret not found for key: {key}");
    }
}