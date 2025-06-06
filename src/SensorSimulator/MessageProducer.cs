using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SensorSimulator;

public class MessageProducer(ILogger<MessageProducer> logger, KafkaProducerService producerService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var testSensorData = new SensorData
        {
            sensorId = Guid.NewGuid().ToString(),
            timestamp = DateTime.Now.Ticks,
            temperature = Random.Shared.Next(-20, 55),
            humidity = Random.Shared.Next(-20, 55),
        };
        
        await producerService.ProduceMessage(testSensorData.ToKafkaMessage(testSensorData.sensorId));
        logger.LogInformation("Message {SensorId} produced", testSensorData.sensorId);
    }
}