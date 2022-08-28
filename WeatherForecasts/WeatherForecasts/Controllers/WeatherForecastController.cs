using Microsoft.AspNetCore.Mvc;

namespace WeatherForecasts.Controllers
{
    [ApiController]
    [Route("/")]
    public class WeatherForecastController : ControllerBase, IForecastWorker
    {
        private readonly Repository _repository;

        public WeatherForecastController(Repository repository) => _repository = repository;

        // This method for creation forecasts:
        [HttpPost("create")]
        public IActionResult Create()
        {
            WeatherForecast weatherForecast = new();
            int prevLeanght = _repository.Forecasts.Count;
            _repository.Forecasts.Add(weatherForecast);
            int currentLeanght = _repository.Forecasts.Count;
            if (prevLeanght + 1 == currentLeanght) return Ok(weatherForecast);
            else return BadRequest("Sorry, but something went wrong...");
        }

        // This method for deleting items:
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string startTime, [FromQuery] string endTime)
        {
            List<WeatherForecast> forDelete = GetForecastsByPeriod(startTime, endTime);
            int prevRepCount = _repository.Forecasts.Count; 

            // Deleting items:
            foreach (var forecast in forDelete)
                _repository.Forecasts.Remove(forecast);

            if (prevRepCount != _repository.Forecasts.Count)
                return Ok($"Deleting was successful: {_repository.Forecasts}");

            return BadRequest("No mathces were found");
        }

        // This method for geting some items:
        [HttpGet("read")]
        public IActionResult Read([FromQuery] string startTime,[FromQuery] string endTime)
        {
            var resultForecasts = GetForecastsByPeriod(startTime, endTime);
            if (resultForecasts.Count > 0) return Ok(resultForecasts);
            else return BadRequest("There are no forecasts for given period...");

        }

        // This method can return all items:
        [HttpGet("read_all")]
        public IActionResult ReadAll()
        {
            return Ok(_repository.Forecasts);
        }
       
        // This method give an apportynity to update item:
        [HttpPut("update")]
        public IActionResult Update([FromQuery] string time,[FromQuery] int newTemperatureC, [FromQuery] string newDescription)
        {
            foreach (WeatherForecast forecast in _repository.Forecasts)
            {
                if (forecast.Time == time)
                {
                    var prevForecast = forecast;
                    forecast.TemperatureC = newTemperatureC;
                    forecast.Description = newDescription;
                    return Ok($"Prev: {prevForecast}\nUpdate: {forecast}");
                }
            }
            return BadRequest("No mathces were found...");
        }

        // This method just for finding items in given period:
        private List<WeatherForecast> GetForecastsByPeriod(string startTime, string endTime)
        {
            List<WeatherForecast> forecastsInPeriod = new();
            var timeInStartTime = TimeOnly.Parse(startTime);
            var timeInEndTime = TimeOnly.Parse(endTime);

            // Searching and adding forecasts to the result list: 
            foreach(var forecast in _repository.Forecasts)
            {
                var forecastTime = TimeOnly.Parse(forecast.Time);
                if (forecastTime >= timeInStartTime && forecastTime <= timeInEndTime)
                    forecastsInPeriod.Add(forecast);
            }
            return forecastsInPeriod;
        }
    }
}