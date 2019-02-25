
namespace ChatLib
{
    /// <summary>
    /// Обработчик события DataReceived, что клиент получает данные (сообщения).
    /// </summary>
    /// <param name="data"> Данные в виде строки</param>
    public delegate void ClientDataReceivedDelegate(string data);
    /// <summary>
    /// Интерфейс, определяющий правила для класса ChatClient.
    /// </summary>
    public interface IChatClient
    {
        /// <summary>
        /// Метод подключения к серверу.
        /// </summary>
        void Connect();
        /// <summary>
        /// Метод отравки данных.
        /// </summary>
        /// <param name="data"> Данные в виде строки</param>
        void Send(string data);
        /// <summary>
        /// Метод чтения данных.
        /// </summary>
        void Listen();
        /// <summary>
        /// Событие, что клиент получает данные (сообщения).
        /// </summary>
        event ClientDataReceivedDelegate DataReceived;

    }
}