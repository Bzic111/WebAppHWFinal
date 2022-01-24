using Microsoft.AspNetCore.Mvc;

namespace WebAppHWFinal.Controllers;

[ApiController]
[Route("WeatherForecast")]
public class WeatherForecastController : ControllerBase
{
    private readonly ForecastHolder _forecastHolder;
    
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,ForecastHolder holder)
    {
        _forecastHolder = holder;
        _logger = logger;
    }

    [HttpGet("api/find/{date}")]
    public IActionResult Read([FromRoute] string date)
    {
        return Ok(_forecastHolder.GetForecastByDate(date));
    }

    [HttpPost("api/create/{date}/{tempC}/{summary}")]
    public IActionResult Create([FromRoute] string date, [FromRoute] int tempC, [FromRoute] string summary)
    {
        return Ok(_forecastHolder.AddForecast(new(date, tempC, summary)));
    }

    [HttpPatch("api/change/{date}/{tempC}/{summary}")]
    public IActionResult Update([FromRoute] string date, [FromRoute] int tempC, [FromRoute] string summary)
    {
        return Ok(_forecastHolder.UpdateForecast(date, tempC, summary));
    }

    [HttpDelete("api/delete_by/{date}")]
    public IActionResult DeleteByDate([FromRoute] string date)
    {
        return Ok(_forecastHolder.DeleteForecast(date));
    }

    //[HttpDelete("api/delete_by/{id}")]
    //public IActionResult DeleteById([FromRoute] int id)
    //{
    //    return Ok(_forecastHolder.DeleteForecast(id));
    //}

}
