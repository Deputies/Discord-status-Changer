using System.Text;

string baseUrl = "https://discord.com/api/v9/users/@me/settings-proto/1";
Console.WriteLine("Enter Auth Token:");
string authToken = Console.ReadLine();

string[] payloads =
{
                "WiQKBQoDZG5kEhcRRoCC3zpRRwwaDHN1cF9DYXRWaWJlMhoCCAE=",
                "WicKCAoGb25saW5lEhcRRoCC3zpRRwwaDHN1cF9DYXRWaWJlMhoCCAE=",
                "WiUKBgoEaWRsZRIXEUaAgt86UUcMGgxzdXBfQ2F0VmliZTIaAggB"
            };

using (HttpClient client = new HttpClient())
{
    client.DefaultRequestHeaders.Add("Host", "discord.com");
    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
    client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en-AU;q=0.9,ar-EG;q=0.8");
    client.DefaultRequestHeaders.Add("Authorization", authToken);
    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) discord/1.0.9013 Chrome/108.0.5359.215 Electron/22.3.2 Safari/537.36");

    while (true)
    {
        foreach (string payload in payloads)
        {
            var content = new StringContent($"{{\"settings\":\"{payload}\"}}", Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(baseUrl),
                Content = content
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"Settings updated with payload: {payload}");
            await Task.Delay(15000); // Wait for 3 seconds before the next change
        }
    }
}
