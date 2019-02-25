using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using BotLib;


namespace ChatLib
{
    
    /// <summary>
    /// Класс, представляющий собой пользователя (клиента) чата.
    /// </summary>
    public class ChatClient : TcpClient, IChatClient
    {
        /// <summary>
        /// IP-адрес пользователя.
        /// </summary>
        protected IPAddress address;
        /// <summary>
        /// Порт подключения пользователя (клиента).
        /// </summary>
        protected int port;
        /// <summary>
        /// Протокол обмена данными.
        /// </summary>
        protected TcpClient client;
        /// <summary>
        /// Поток, выполняющий метод Listen - чтения данных.
        /// </summary>
        protected Thread thread;
        /// <summary>
        /// Событие, что клиент получает данные (сообщения).
        /// </summary>
        public event ClientDataReceivedDelegate DataReceived;
        /// <summary>
        /// Конструктор класса ChatClient, создающий экземпляр (объект) - клиент.
        /// </summary>
        /// <param name="addr">IP-адрес клиента сервера.</param>
        /// <param name="port">Порт клиента сервера.</param>
        public ChatClient(IPAddress addr, int port)
        {
            address = addr;
            this.port = port;
            client = new TcpClient();
            thread = new Thread(new ThreadStart(Listen)) { IsBackground = true };
        }
        /// <summary>
        /// Метод подключения пользователя к серверу.
        /// </summary>
        public void Connect()
        {
            client.Connect("127.0.0.1", port);
            thread.Start();
        }
        /// <summary>
        /// Метод отправки данных.
        /// </summary>
        /// <param name="data">Отправляемое сообщение.</param>
        public void Send(string data)
        {
             client.Client.Send(Encoding.Default.GetBytes(data));

        }
        /// <summary>
        /// Метод прослушивания (чтения) данных, 
        /// </summary>
        public void Listen()
        {
            byte[] _buff = new byte[client.Client.ReceiveBufferSize];
            int _readed = 0;
            string _res = string.Empty;
            while (true)
            {
                if (client.Client.Available > 0)
                {
                    _readed = client.Client.Receive(_buff);
                    _res = Encoding.Default.GetString(_buff, 0, _readed);
                    if (!_res.Equals("_") && (DataReceived != null))
                        DataReceived(_res);
                }
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
