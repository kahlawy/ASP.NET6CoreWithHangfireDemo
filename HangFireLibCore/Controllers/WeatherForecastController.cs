using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangFireLibCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //BackgroundJob.Enqueue(() => sendMessage(" Message Sent"));

            RecurringJob.AddOrUpdate(() => sendMessage(" Message Sent"), Cron.Minutely);

          //  BackgroundJob.Schedule(() => sendMessage(" Message Sent" + DateTime.Now.AddSeconds(30)), TimeSpan.FromSeconds(30));
         

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [ApiExplorerSettings(IgnoreApi =true)]
        public void sendMessage(string message)
        {
            Console.WriteLine("Your Message:" + message);
        }
    }
}