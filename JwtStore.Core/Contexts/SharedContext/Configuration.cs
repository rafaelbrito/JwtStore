namespace JwtStore.Core.Contexts.SharedContext;

public static class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();
    public static DatabaseConfiguration Database { get; set; } = new();

    public static SendGridConfiguration SendGrid { get; set; } = new();
    public static EmailConfiguration Email { get; set; } = new();


    public class SecretsConfiguration
    {
        public string ApiKey { get; set; } = String.Empty;
        public string JwtPrivateKey { get; set; } = String.Empty;
        public string PasswordSaltKey { get; set; } = String.Empty;
    }

    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = String.Empty;
    }

    public class SendGridConfiguration
    {
        public string ApiKey { get; set; } = String.Empty;
    }

    public class EmailConfiguration
    {
        public string DefaultFromEmail { get; set; } = "test@rafaelbrito.com";
        public string DefaultFromName { get; set; } = "Rafael";
    }
}