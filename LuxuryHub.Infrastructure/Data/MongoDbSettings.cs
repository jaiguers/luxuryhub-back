namespace LuxuryHub.Infrastructure.Data;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public Collections Collections { get; set; } = new();
}

public class Collections
{
    public string Owners { get; set; } = string.Empty;
    public string Properties { get; set; } = string.Empty;
    public string PropertyImages { get; set; } = string.Empty;
    public string PropertyTraces { get; set; } = string.Empty;
}
