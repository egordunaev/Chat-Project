using System.Net.Sockets;

namespace ChatLib
{
    /// <summary>
    /// Обработчик события DataReceivedEvent, что клиенту отправляется оповещение с сервера, что данные получены сервером.
    /// </summary>
    /// <param name="data"> Данные в виде строки</param>
    /// <param name="client"> Протокол обмена данными</param>
    public delegate void ServerDataReceivedDelegate(string data, TcpClient client);
    /// <summary>
    /// Обработчик события ConnectionEvent, указывающего статус подключения клиента с конечной удаленной точкой.
    /// </summary>
    /// <param name="status"> Статус подключения</param>
    /// <param name="client"> Протокол обмена данными</param>
    public delegate void ServerConnectionDelegate(ConnState status, TcpClient client);
    /// <summary>
    /// Интерфейс, определяющий правила для класса ChatServer.
    /// </summary>
    public interface IChatServer
    {
        /// <summary>
        /// Метод прослушивания подключений.
        /// </summary>
        void ListenConnects();
        /// <summary>
        /// Метод отправки данных всем подключенным клиентам.
        /// </summary>
        /// <param name="data"></param>
        void Send(string data);
        /// <summary>
        /// Метод чтения.
        /// </summary>
        void ListenData();
        /// <summary>
        /// Отправление оповещения клиенту с сервера, что данные получены сервером.
        /// </summary>
        event ServerDataReceivedDelegate DataReceivedEvent;
        /// <summary>
        /// Событие, указывающее статус подключения клиента с конечной удаленной точкой.
        /// </summary>
        event ServerConnectionDelegate ConnectionEvent;
    }
}
