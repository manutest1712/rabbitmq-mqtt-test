using MQTTnet;
using System.Text;
using System.Text.Json;

internal class Program
{
    private static async Task Main()
    {
        // Create MQTT client factory
        var mqttFactory = new MqttClientFactory();

        // Create client instance
        var mqttClient = mqttFactory.CreateMqttClient();

        // Subscribe event handler
        mqttClient.ApplicationMessageReceivedAsync += async context =>
        {
            try
            {
                var payload = context.ApplicationMessage.Payload;
                string json = Encoding.UTF8.GetString(payload);

                Console.WriteLine("Received message:");
                Console.WriteLine(json);

                // Deserialize (optional)
                var data = JsonSerializer.Deserialize<RobotData>(json);
                Console.WriteLine($"theta={data.theta}, x={data.x}, y={data.y}, z={data.z}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing message: " + ex.Message);
            }

            await Task.CompletedTask;
        };

        // Build connection options
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .WithCredentials("admin", "admin")
            .WithClientId("robot-subscriber-1")
            .Build();

        // Connect to the broker
        await mqttClient.ConnectAsync(options);
        Console.WriteLine("Subscriber connected.");

        // Subscribe to topic
        await mqttClient.SubscribeAsync("robot/motion");
        Console.WriteLine("Subscribed to robot/motion");

        Console.WriteLine("Listening... Press Ctrl+C to exit.");
        await Task.Delay(-1);
    }

    public class RobotData
    {
        public double theta { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
}
