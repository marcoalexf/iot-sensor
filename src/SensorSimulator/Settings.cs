namespace SensorSimulator;

public class KafkaSettings
{
    public string BootstrapServers { get; set; } = string.Empty;
    public SchemaRegistryAuth SchemaRegistry { get; set; } = new();
    public string TopicName { get; set; } = string.Empty;
    public ProducerSettings Producer { get; set; } = new();
}

public class SchemaRegistryAuth
{
    public string Username { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string AuthenticationType { get; set; } = "Basic"; 
}

public class ProducerSettings
{
    public int Acks { get; set; } = -1;
    public int Retries { get; set; } = 3;
    public int BatchSize { get; set; } = 16384;
    public int LingerMs { get; set; } = 5;
    public string CompressionType { get; set; } = "snappy";
    public int RequestTimeoutMs { get; set; } = 30000;
    public int DeliveryTimeoutMs { get; set; } = 120000;
}