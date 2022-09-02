using Microsoft.AspNetCore.Mvc;

namespace WeatherForecasts
{
    public interface IForecastWorker
    {
        IActionResult Create(); 
        IActionResult Update(string time, int newTemperatureC, string newDescription);
        IActionResult Read(string startTime, string endTime); 
        IActionResult Delete(string startTime, string endTime);
    }
}
