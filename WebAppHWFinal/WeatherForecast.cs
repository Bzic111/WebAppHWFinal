namespace WebAppHWFinal;

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }

    public WeatherForecast() { }

    public WeatherForecast(string date, int tempC, string str)
    {
        string[] temp = date.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (temp.Length == 3)
        {
            int day, month, year;
            int.TryParse(temp[0], out day);
            int.TryParse(temp[1],out month);
            int.TryParse(temp[2],out year);
            Date = new DateTime(day: day, month: month, year: year);
            TemperatureC = tempC;
            Summary = str;
        }
    }
    public static bool operator ==(WeatherForecast wf1, WeatherForecast wf2) => wf1.Date == wf2.Date && wf1.TemperatureC == wf2.TemperatureC && wf1.Summary == wf2.Summary;
    public static bool operator !=(WeatherForecast wf1, WeatherForecast wf2) => !(wf1.Date == wf2.Date && wf1.TemperatureC == wf2.TemperatureC && wf1.Summary == wf2.Summary);
}
