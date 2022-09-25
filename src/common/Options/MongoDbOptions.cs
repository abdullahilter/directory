namespace common.Options;

public class MongoDbOptions
{
    public string DatabaseName { get; init; } = default!;

    public string ConnectionString { get; init; } = default!;
}