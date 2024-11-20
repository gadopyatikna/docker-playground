using StackExchange.Redis;
using RedisCounterApp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Connect to Redis
var redis = ConnectionMultiplexer.Connect("redis"); // "redis" is the container name we'll define in Docker Compose
var db = redis.GetDatabase();

app.MapGet("/page", async context =>
{
    // Increment the counter
    var count = await db.StringIncrementAsync("page_visits");

    // Display the counter value
    await context.Response.WriteAsync($"Page visits: {count}\n");
});

app.MapGet("/ping", async context =>
{
    var pingCommand = new PingCommand();
    await context.Response.WriteAsync($"IsInternetAvailable={pingCommand.Exec("google.com")}\n");
});

app.Urls.Add("http://0.0.0.0:80");

app.Run();