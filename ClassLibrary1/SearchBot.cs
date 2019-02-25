using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace BotLib
{
    /// <summary>
    /// Реализует поиск по ключевому слову или фразе.
    /// </summary>
    public class SearchBot : IBot
    {
        /// <summary>
        /// "Обрезает" полученные данные и выводит результаты поиска по ключевому слову.
        /// </summary>
        /// <param name="searchword">Критерии поиска.</param>
        /// <param name="var_client">Клиент сервера.</param>
        public void BotOutput(string searchword, TcpClient var_client)
        {
            string tempsearch = searchword.Substring(10);
            System.Diagnostics.Process.Start("https://www.google.com/search?q=" + tempsearch);
            byte[] _s = Encoding.Default.GetBytes("Searchbot: Done!");
            var_client.Client.Send(_s);
        }
    }
}
