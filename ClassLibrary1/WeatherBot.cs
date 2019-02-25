using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace BotLib
{
    /// <summary>
    /// Бот, который определяет погоду в указанном регионе.
    /// </summary>
    public class WeatherBot :IBot
    {
        /// <summary>
        /// Вывод погоды.
        /// </summary>
        /// <param name="data">Полученные данные.</param>
        /// <param name="var_client">Клиент сервера.</param>
        public void BotOutput(string data, TcpClient var_client)
        {
            string city = data.Substring(12);
            string weather_apikey = "---------REMOVED---------";
            var weatherwebpath = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&mode=html&units=metric&APPID=" + weather_apikey;
            System.Diagnostics.Process.Start("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&mode=html&units=metric&APPID=" + weather_apikey);
            byte[] _s = Encoding.Default.GetBytes("Weatherbot: Done!");
            var_client.Client.Send(_s);
        }
    }
}
