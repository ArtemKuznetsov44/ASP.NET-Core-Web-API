using MetricManager.Client;

namespace MetricsManager.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Initializing our client: 
            AgentsClient agentClient = new AgentsClient("http://localhost:5105", new HttpClient());
            CpuMetricsClient cpuMetricsClient = new CpuMetricsClient("http://localhost:5105", new HttpClient());

            // MetricsAgent port: http://localhost:5155

            // This is a ASYNC method for registration. 
            // Such method can return Task class, whitch can be template or specified.
            // We can wait while our method works, but in this case we need to do it. 
            await agentClient.RegisterAsync(new AgentInfoDto
            {
                AgentUri = "http://localhost:5155/",
                AgentId = 1,
                Enable = true
            });
            while (true)
            {


                Console.Clear();
                Console.WriteLine("Tasks");
                Console.WriteLine("===============================================");
                Console.WriteLine("1 - Get metrics for last minute (CPU).");
                Console.WriteLine("0 - App shutdown.");
                Console.WriteLine("===============================================");
                Console.Write("Enter a task number: ");

                if (int.TryParse(Console.ReadLine(), out int taskNumber))
                {
                    switch (taskNumber)
                    {
                        case 0:
                            Console.WriteLine("Finishing app.");
                            Console.ReadKey(true);
                            break;
                        case 1:
                            try
                            {
                                TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                                TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                                // Async method too:
                                CpuMetricsResponse response = await cpuMetricsClient.ToGetAsync(
                                   1,
                                   fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                   toTime.ToString("dd\\.hh\\:mm\\:ss"));

                                foreach (var metric in response.Metrics)
                                {
                                    Console.WriteLine($"" +
                                        $"Time : {TimeSpan.FromSeconds(metric.Time).ToString("dd\\.hh\\:mm\\ss")} >>> " +
                                        $"Value: {metric.Value}");
                                   
                                }
                                Console.WriteLine("Please, enter any key...");
                                Console.Read(); 
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }

                            break;
                        default:
                            Console.WriteLine("Enter a currect task number.");
                            break;

                    }
                }
            }


        }
    }
}