namespace MiniDefinition;

public static class MiniDefinitionDbProperties
{
    public static string DbTablePrefix { get; set; } = "MiniDefinition";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "MiniDefinition";
}
