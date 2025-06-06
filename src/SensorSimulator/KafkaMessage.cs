namespace SensorSimulator;

public class KafkaMessage<T>
{
    public required T Message { get; set; } = default!;
    public required string Key { get; set; }
}