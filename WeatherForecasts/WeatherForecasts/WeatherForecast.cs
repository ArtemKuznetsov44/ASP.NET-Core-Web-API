namespace WeatherForecasts
{
    public class WeatherForecast
    {
        // Fields:
        private static readonly string[] Descriptions = new string[] { "Freezing", "Cold", "Bracing", "Cool", "Warm", "Hot" };
        private string _time;
        private int _temperatureC;
        private string _description; 

        public WeatherForecast()
        {
            SetTime();
            SetTemperature();
            SetDescription(); 
        }

        // Methods:
        // This method for setting time.
        private void SetTime() => _time = DateTime.Now.ToLongTimeString().Trim().Split()[0];    

        // Get current time.
        private void SetTemperature() => _temperatureC = new Random().Next(-55, 55); 
        private void SetDescription()
        {
            if (_temperatureC >= -5 && _temperatureC <= 2) _description = Descriptions[0]; 
            else if (_temperatureC <= -6) _description = Descriptions[1];
            else if (_temperatureC >= 3 && _temperatureC <= 17) _description = Descriptions[2];
            else if (_temperatureC >= 18 && _temperatureC <= 24) _description = Descriptions[3];
            else if (_temperatureC >= 25 && _temperatureC <= 32) _description = Descriptions[4];
            else if (_temperatureC >= 33) _description = Descriptions[5];
        }

        // Properties:
        public string Time
        {
            get => _time;
            set => _time = value;
        }
        public int TemperatureC
        {
            get => _temperatureC;
            set => _temperatureC = value;
        }
        public string Description
        {
            get => _description; 
            set => _description = value;
        }
    }
}