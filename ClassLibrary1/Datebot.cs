using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;



namespace BotLib
{
    /// <summary>
    /// Реализует вывод даты и времени.
    /// </summary>
    public class DateBot : IBot 
    {
        /// <summary>
        /// Выводит дату и время на данный момент.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <param name="var_client">Клиент сервера.</param>
        public void BotOutput(string date, TcpClient var_client)
        {
            byte[] _s = Encoding.Default.GetBytes("DateBot: Today is " + DateTime.Now.ToString()+"\n");
            var_client.Client.Send(_s);
        }
    }
}
