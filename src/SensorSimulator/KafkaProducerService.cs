using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SensorSimulator;

public class KafkaProducerService(ILogger<KafkaProducerService> logger, IOptions<KafkaSettings> kafkaSettings)
{
    private ISchemaRegistryClient _schemaRegistryClient;

    public async Task ProduceMessage<T>(KafkaMessage<T> message)
    {
        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = kafkaSettings.Value.SchemaRegistry.Url,
            BasicAuthUserInfo = $"{kafkaSettings.Value.SchemaRegistry.Username}:{kafkaSettings.Value.SchemaRegistry.Password}"
        };

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers
        };

        _schemaRegistryClient = new CachedSchemaRegistryClient(schemaRegistryConfig);
        using var producer = new ProducerBuilder<string, T>(producerConfig)
            .SetValueSerializer(new AvroSerializer<T>(_schemaRegistryClient))
            .Build();

        try
        {
            var result = await producer.ProduceAsync(kafkaSettings.Value.TopicName, new Message<string, T> { Key = message.Key, Value = message.Message });
            logger.LogInformation("Produced kafka message: {@message} to topic {@topic}.", message.Message, kafkaSettings.Value.TopicName);
        }
        catch (ProduceException<string, T> ex)
        {
            logger.LogError(ex, "Error producing message: {@message}.", ex.Message);
        }
    }
}