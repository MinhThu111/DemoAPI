using Microsoft.AspNetCore.Mvc;

namespace API.Demo.OPS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastOPSController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastOPSController> _logger;

        public WeatherForecastOPSController(ILogger<WeatherForecastOPSController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastOPS> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastOPS
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
