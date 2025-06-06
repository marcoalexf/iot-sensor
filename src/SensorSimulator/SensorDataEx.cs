namespace SensorSimulator;

public static class SensorDataEx
{
    public static KafkaMessage<SensorData> ToKafkaMessage(this SensorData sensorData, string key)
    {
        return new KafkaMessage<SensorData>()
        {
            Message = sensorData,
            Key = key
        };
    }
}