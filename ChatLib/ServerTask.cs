using System.Net.Sockets;

namespace ChatLib
{
    /// <summary>
    /// Структура серверной задачи.
    /// </summary>
    public struct ServerTask
    {
        /// <summary>
        /// Протокол обмена данными.
        /// </summary>
        public TcpClient _client;
        /// <summary>
        /// Данные в виде строки.
        /// </summary>
        public string _data;
    }
}
