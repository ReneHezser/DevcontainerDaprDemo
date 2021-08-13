using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DemoDataGenerator
{
    class Program
    {
        private static int counter;
        private static DesiredProperties desiredProperties = new DesiredProperties();
        private static DataGenerationPolicy generationPolicy = new DataGenerationPolicy();
        private static volatile bool IsReset = false;
        private static HttpClient httpClient = new HttpClient();
        private static string daprHost = Environment.GetEnvironmentVariable("DAPR_HTTP_URL") ?? "demo-data-generator_dapr";
        private static string daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500";
        private static string topicName = Environment.GetEnvironmentVariable("TOPIC_NAME") ?? "sampleTopic";
        private static string publishUrl;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Demo Data Generator");
            publishUrl = $"http://{daprHost}:{daprPort}/v1.0/publish/pubsub/{topicName}";
            Console.WriteLine($"Sending messages to '{publishUrl}'");

            while (true)
            {
                for (int i = 0; i < desiredProperties.InstanceCount; i++)
                {
                    counter++;
                    if (counter == 1)
                    {
                        // first time execution needs to reset the data factory
                        IsReset = true;
                    }

                    var messageBody = TemperatureDataFactory.CreateTemperatureData(counter, i, generationPolicy, IsReset);
                    var messageString = JsonConvert.SerializeObject(messageBody);
                    IsReset = false;

                    Console.WriteLine($"\t{DateTime.UtcNow.ToShortDateString()} {DateTime.UtcNow.ToLongTimeString()}> Sending message: {counter}, Body: {messageString}");
                    await SendMessage(messageString, i);

                }
                await Task.Delay(desiredProperties.SendInterval);
            }
        }

        private static async Task<bool> SendMessage(string message, int instanceId)
        {
            try
            {
                var content = new StringContent(message, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(publishUrl, content))
                {
                    response.EnsureSuccessStatusCode();
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Send message to '{publishUrl}' with '{message}' HttpRequestException: {ex.ToString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send message to '{publishUrl}' with '{message}' error: {ex.ToString()}");
            }
            return false;
        }
    }
}
