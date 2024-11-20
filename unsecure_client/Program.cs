class UnsecureClient
{
    static async Task Main()
    {
        using (HttpClient client = new HttpClient())
        {
            // Send a GET request to the HTTP server
            HttpResponseMessage response = await client.GetAsync("http://localhost:8080/page");
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Server Response:");
            Console.WriteLine(content);
        }
    }
}