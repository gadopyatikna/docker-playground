class UnsecureClient
{
    static async Task Main()
    {
        using (HttpClient client = new HttpClient())
        {
            // Send a GET request to the HTTP server
            HttpResponseMessage response = await client.GetAsync("http://127.0.0.1:8080");
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server Response:");
            Console.WriteLine(content);
        }
    }
}