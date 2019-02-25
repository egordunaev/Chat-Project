using System;
using System.Text;
using ChatLib;
using System.Net;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// Основная программа сервера.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Сервер.
        /// </summary>
        static ChatServer _srv;

        /// <summary>
        /// Медленная очередь.
        /// </summary>
        static MessageQueue _queueSlow;
        /// <summary>
        /// Быстрая очередь.
        /// </summary>
        static MessageQueue _queueFast; 

        /// <summary>
        /// Основная программа где описывается подключение к серверу
        /// чата используя IP-Адресс и Порт подключения.
        /// </summary>
        /// <param name="args">Вводимые параметры запуска (если есть).</param>
        static void Main(string[] args)
        {
            _srv = new ChatServer(IPAddress.Loopback, 6000);
            _srv.DataReceivedEvent += _srv_DataReceived;
            _srv.ConnectionEvent += _srv_ConnectionEvent;
            _queueSlow = new MessageQueue(_srv);
            _queueFast = new MessageQueue(_srv);
            Console.ReadLine();
        }
        /// <summary>
        /// Событие подключения к серверу.
        /// </summary>
        /// <param name="status">Статус подключения к серверу.</param>
        /// <param name="client">Сведения о клиенте.</param>
        static void _srv_ConnectionEvent(ChatLib.ConnState status, System.Net.Sockets.TcpClient client)
        {
            Console.WriteLine("{0}: {1}", status, client.Client.RemoteEndPoint);
        }
        /// <summary>
        /// Событие получения данных.
        /// </summary>
        /// <param name="data">Полученные данные (сообщение).</param>
        /// <param name="client">Сведения о клиенте.</param>
        static void _srv_DataReceived(string data, System.Net.Sockets.TcpClient client)
        {
            if (data.Length > 100)
                _queueSlow.SetTask(data, client);
            else
                _queueFast.SetTask(data, client);
            
        }
        /*
         static void X(ref ServerTask _task, ref ChatServer server, ref Queue<ServerTask> queue)
        {
            Console.WriteLine("Client {0}: {1}",_task._client.Client.RemoteEndPoint, _task._data);
            _task._client.Client.Send(Encoding.Default.GetBytes("__"));
            System.Threading.Thread.Sleep(1000);
            server.Send("Message sent!"+"\n");
            //Console.WriteLine("Q{0}", queue.Count);
        } */
    }
}
