using System;

namespace SwashbuckleWithUtf8JsonTest
{
    /// <summary>
    /// Le forecast class
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Le date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Le temperature in Celsius
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Le temperature not in Celsius
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Le Summary de le forecast
        /// </summary>
        public string Summary { get; set; }
    }
}
