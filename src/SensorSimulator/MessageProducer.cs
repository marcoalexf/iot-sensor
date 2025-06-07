using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SensorSimulator;

public class MessageProducer(ILogger<MessageProducer> logger, KafkaProducerService producerService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("MessageProducer started");
        while (true)
        {
            var testSensorData = new SensorData
            {
                sensorId = Guid.NewGuid().ToString(),
                timestamp = DateTime.Now.Ticks,
                temperature = Random.Shared.Next(-20, 55),
                humidity = Random.Shared.Next(-20, 55),
            };

            //await Task.Delay(1000, stoppingToken);
            await producerService.ProduceMessage(testSensorData.ToKafkaMessage(testSensorData.sensorId));
            logger.LogInformation("Message {SensorId} produced", testSensorData.sensorId);
        }
    }
}