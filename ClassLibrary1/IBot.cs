using System.Net.Sockets;
namespace BotLib
{
    /// <summary>
    /// Интерфейс ботов.
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Метод вывода для всех ботов.
        /// </summary>
        /// <param name="b_out">Вывод бота.</param>
        /// <param name="var_client">Клиент сервера.</param>
        void BotOutput(string b_out, TcpClient var_client);
        
    }
}
