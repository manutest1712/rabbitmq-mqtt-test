using MQTTnet;
using System.Text.Json;

internal class Program
{
    private static async Task Main()
    {
        // Create MQTT client factory
        var mqttFactory = new MqttClientFactory();

        // Create client instance
        var mqttClient = mqttFactory.CreateMqttClient();

        // Build client options
        var mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883) // RabbitMQ MQTT port
            .WithCredentials("admin", "admin")
            .WithClientId("robot-publisher-1")
            .Build();

        // Sample robot message
        var robotData = new
        {
            theta = 90.5,
            z = 12.3,
            x = 100.2,
            y = 55.7
        };

        string json = JsonSerializer.Serialize(robotData);

        // Build MQTT message
        var message = new MqttApplicationMessageBuilder()
            .WithTopic("robot/motion")
            .WithPayload(json)
            .Build();

        // Connect & publish
        await mqttClient.ConnectAsync(mqttOptions);
        await mqttClient.PublishAsync(message);

        Console.WriteLine("Message sent!");
    }
}
