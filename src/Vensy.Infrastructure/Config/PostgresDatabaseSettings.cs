namespace Vensy.Infrastructure.Config;


public class PostgresDatabaseSettings
{
    public static string PropertyName = "DatabaseSettings:Postgres";

    public string Host {get; set;} = string.Empty;

    public string Database {get;set;} = string.Empty;
    public string Username {get;set;} = string.Empty;
    public string Password {get;set;} = string.Empty;

    public string Port {get;set;} = string.Empty;
    

    public string ConnectionString()
    {
        return $"Host={Host}; Database={Database}; Username={Username}; Password={Password}; Port={Port}";
    }
}