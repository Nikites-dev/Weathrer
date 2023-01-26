using Microsoft.AspNetCore.Mvc;
using WebAppServer.MongoDb;

namespace WebAppServer.Controllers;

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
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }


    [HttpPost(Name = "PostWeatherForecast")]
    public async Task<ActionResult>Post(WeatherForecast forecast)
    {
        if (forecast == null)
        {
            return BadRequest();
        } else
        {
            MongoDBAction.AddToDatabase(forecast);
            return Ok(forecast);
        }
    }

    [HttpDelete(Name = "DeleteWeatherForecast")]
    public async Task<ActionResult> Delete(String id)
    {
        MongoDBAction.DeleteById(id);
        return Ok("Delete succes");

    }

    [HttpPatch(Name = "PatchWeatherForecast")]
    public async Task<ActionResult> Update(String id, WeatherForecast fs)
    {
        MongoDBAction.UpdateByName(id, fs);
        return Ok("Update succes");
    }
}