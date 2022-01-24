using System.Globalization;
using WebAppHWFinal;

var holder = new ForecastHolder();
var forecast = new WeatherForecast() { Date = DateTime.Now, TemperatureC = 21, Summary = "asdf" };
holder.AddForecast(forecast);
string str = holder.GetForecastByDate(DateTime.Now.ToShortDateString())!.TemperatureF.ToString();
Console.WriteLine(str);
Console.WriteLine(holder.UpdateForecast(DateTime.Now.ToShortDateString(), 35, "dfsgs"));
str = holder.GetForecastByDate(DateTime.Now.ToShortDateString())!.TemperatureF.ToString(); 
Console.WriteLine(str);