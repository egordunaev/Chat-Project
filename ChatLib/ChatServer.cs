using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System;
using BotLib;

namespace ChatLib
{
    /// <summary>
    /// Сервер обработки и передачи данных.
    /// </summary>
    public class ChatServer : IChatServer
    {
        /// <summary>
        /// Список ботов.
        /// </summary>
        protected List<IBot> Botlist = new List<IBot>();
        /// <summary>
        /// Бот даты.
        /// </summary>
        DateBot dbot = new DateBot();
        /// <summary>
        /// Поисковой бот.
        /// </summary>
        SearchBot sbot = new SearchBot();
        /// <summary>
        /// Отображение состояния очереди и сообщение о состоянии отправки сообщений из очереди.
        /// </summary>
        Task task = new Task();
        /// <summary>
        /// Поток, выполняющий метод ListenConnects - прослушивание подключений.
        /// </summary>
        protected Thread lcThread;
        /// <summary>
        /// Поток, выполняющий метод ListenData - статус данных.
        /// </summary>
        protected Thread ldThread;
        /// <summary>
        /// Поток, выполняющий метод CheckConnections - проверка подключения клиента к серверу.
        /// </summary>
        protected Thread aThread;
        /// <summary>
        /// IP адрес пользователя (клиента).
        /// </summary>
        protected IPAddress address;
        /// <summary>
        /// Порт пользователя (клиента).
        /// </summary>
        protected int port;
        /// <summary>
        /// Протокол обмена данными.
        /// </summary>
        protected List<TcpClient> clients = new List<TcpClient>();
        /// <summary>
        /// Протокол обмена данными сервера.
        /// </summary>
        protected TcpListener server;
        /// <summary>
        /// Отправление оповещения клиенту с сервера, что данные получены сервером.
        /// </summary>
        public event ServerDataReceivedDelegate DataReceivedEvent;
        /// <summary>
        /// Событие, указывающее статус подключения клиента с конечной удаленной точкой.
        /// </summary>
        public event ServerConnectionDelegate ConnectionEvent;
        /// <summary>
        /// Конструктор класса ChatServer, создающий экземпляр (объект) - сервер.
        /// </summary>
        /// <param name="addr"> IP-адрес</param>
        /// <param name="port"> Порт</param>
        public ChatServer(IPAddress addr, int port)
        {
            address = addr;
            this.port = port;
            server = new TcpListener(addr, port);
            server.Start(10);
            lcThread = new Thread(new ThreadStart(ListenConnects)) { IsBackground = true };
            lcThread.Start();
            ldThread = new Thread(new ThreadStart(ListenData)) { IsBackground = true };
            ldThread.Start();
            aThread = new Thread(new ThreadStart(CheckConnections)) { IsBackground = true };
            aThread.Start();
        }
        /// <summary>
        /// Метод прослушивания подключений.
        /// </summary>
        public void ListenConnects()
        {
            while (true)
            {
                try
                {
                    TcpClient _client = server.AcceptTcpClient();
                    lock (clients)
                    {
                        clients.Add(_client);
                        if (ConnectionEvent != null)
                            ConnectionEvent(ConnState.Connected, _client);
                    }
                }
                catch (Exception _exc)
                {
                    Console.WriteLine(_exc.Message);
                }
            }

        }
        /// <summary>
        /// Метод отправки данных.
        /// </summary>
        /// <param name="data"> Данные в виде строки</param>
        public void Send(string data)
        {
            byte[] _s = Encoding.Default.GetBytes(data);
            lock (clients)
            {
                for (int _i = 0; _i < clients.Count; _i++)
                {
                    TcpClient _client = clients[_i];
                    try
                    {
                        _client.Client.Send(_s);
                    }
                    catch (SocketException _exc)
                    {
                        Console.WriteLine(_exc.Message);
                        clients.Remove(_client);
                        _i--;
                        if (ConnectionEvent != null)
                            ConnectionEvent(ConnState.Disconnected, _client);
                       
                    }
                }
            }
        }
        /// <summary>
        /// Прослушивание данных сервером.
        /// </summary>
        public void ListenData()
        {
            byte[] _buff = new byte[server.Server.ReceiveBufferSize];
            int _readed = 0;
            while (true)
            {
                lock (clients)
                {
                    for (int _i = 0; _i < clients.Count; _i++)
                    {
                        TcpClient _client = clients[_i];
                        try
                        {
                            if (_client.Client.Available > 0)
                            {
                                _readed = _client.Client.Receive(_buff);
                                if (DataReceivedEvent != null)
                                    DataReceivedEvent(Encoding.Default.GetString(_buff, 0, _readed), _client);
                                foreach (IBot bots in Botlist)
                                {
                                    bots.BotOutput(Encoding.Default.GetString(_buff, 0, _readed), _client);
                                }
                            }
                        }
                        catch (SocketException _exc)
                        {
                            Console.WriteLine(_exc.Message);
                            clients.Remove(_client);
                            _i--;
                            if (ConnectionEvent != null)
                                ConnectionEvent(ConnState.Disconnected, _client);
                        }
                    }
                    System.Threading.Thread.Sleep(1);
                }
            }
        }
        /// <summary>
        /// Проверка подключений к серверу.
        /// </summary>
        protected void CheckConnections()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Send("_");
            }
        }
        /// <summary>
        /// Показывает список подключенных пользователей.
        /// </summary>
        /// <param name="data">Полученные данные.</param>
        /// <param name="var_client">Клиент сервера.</param>
        public void ClientLister(string data, TcpClient var_client)
        {
            byte[] _s = Encoding.Default.GetBytes("Available Users:"+"\n");
            var_client.Client.Send(_s);
            foreach (var client in clients)
            {
                _s = Encoding.Default.GetBytes("User: "+client.Client.RemoteEndPoint.ToString()+"\n");

                //_s = Encoding.Default.GetBytes("Client "+var_client.Client.RemoteEndPoint + "\n");
                var_client.Client.Send(_s);
            }
        }
        /// <summary>
        /// Отправляет сообщение конкретному пользователю.
        /// </summary>
        /// <param name="data">Полученные данные</param>
        public void SendTo(string data)
        {
            var senderto = data.Substring(12).TrimEnd(data.Substring(14).ToCharArray());
            var actual_message = data.Substring(14);
            byte[] _s = Encoding.Default.GetBytes(actual_message+"\n");
            lock (clients)
            {
                try
                {
                    TcpClient _client = clients[Convert.ToInt32(senderto) - 1];
                    _client.Client.Send(_s);
                }
                catch (Exception _exc)
                {
                    Console.WriteLine("ERROR: "+_exc.Message);
                }
                
            }
        }


    }
}
