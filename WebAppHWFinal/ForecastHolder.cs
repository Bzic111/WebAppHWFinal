using System.Globalization;
namespace WebAppHWFinal;

public class ForecastHolder
{
    private List<WeatherForecast> _forecasts { get; set; }

    private CultureInfo _culture = new CultureInfo("ru-RU");

    public ForecastHolder() => _forecasts = new List<WeatherForecast>();

    public void AddForecast(DateTime date, int tempC, string str) => _forecasts.Add(new WeatherForecast() { Date = date, TemperatureC = tempC, Summary = str });

    public bool AddForecast(WeatherForecast wf)
    {
        _forecasts.Add(wf);
        return wf == _forecasts[^1];
    }

    public WeatherForecast? GetForecastById(int count)
    {
        if (_forecasts.Count > count)
            return _forecasts[count];
        else
            return null;
    }
    
    public WeatherForecast? GetForecastByDate(string date)
    {
        foreach (var item in _forecasts)
            if (item.Date.ToShortDateString() == FromString(date).ToShortDateString())
                return item;
        return null;
        //DateTime? current = FromString(date);
        //if (current is not null)
    }

    public bool DeleteForecast(int count)
    {
        if (count < _forecasts.Count)
        {
            _forecasts.RemoveAt(count);
            return true;
        }
        return false;
    }

    public bool DeleteForecast(string date)
    {
        var temp = GetForecastByDate(date);
        if (temp is not null)
        {
            _forecasts.Remove(temp);
            return true;
        }
        return false;
    }

    public bool DeleteForecast(WeatherForecast wf)
    {
        for (int i = 0; i < _forecasts.Count; i++)
            if (_forecasts[i] == wf)
            {
                _forecasts.RemoveAt(i);
                return true;
            }
        return false;
    }

    public bool UpdateForecast(string date, int tempC, string str)
    {
        var temp = GetForecastByDate(date);
        if (temp is not null)
        {
            temp.TemperatureC = tempC;
            temp.Summary = str;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateForecast(int count,int tempC,string str)
    {
        var temp = GetForecastById(count);
        if (temp is not null)
        {
            temp.TemperatureC = tempC;
            temp.Summary = str;
            return true;
        }
        else
        {
            return false;
        }
    }

    private DateTime FromString(string date)
    {
        int day, month, year;
        string[] temp = date.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (temp.Length == 3)
        {
            int.TryParse(temp[0], out day);
            int.TryParse(temp[1], out month);
            int.TryParse(temp[2], out year);
            return new DateTime(day: day, month: month, year: year);
        }
        return new DateTime(0,0,0);
    }
}