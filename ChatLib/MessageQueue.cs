using System;
using System.Collections.Generic;
using System.Threading;
using BotLib;

namespace ChatLib
{
    /// <summary>
    /// Класс очереди сообщений.
    /// </summary>
    public class MessageQueue: Task
    {
        /// <summary>
        /// 
        /// </summary>
        DateBot dbot = new DateBot();
        /// <summary>
        /// 
        /// </summary>
        SearchBot sbot = new SearchBot();
        /// <summary>
        /// 
        /// </summary>
        WeatherBot wbot = new WeatherBot();
        /// <summary>
        /// 
        /// </summary>
        SMSBot smsbot = new SMSBot();
        /// <summary>
        /// Очередь сообщений.
        /// </summary>
        protected Queue<ServerTask> queue = new Queue<ServerTask>();
        /// <summary>
        /// Поток очереди.
        /// </summary>
        protected Thread queueThread;
        /// <summary>
        /// Ссылка на основной сервер.
        /// </summary>
        protected ChatServer server;
        /// <summary>
        /// Метод очереди сообщений.
        /// </summary>
        /// <param name="srv">Сервер</param>
        public MessageQueue(ChatServer srv)
        {
            server = srv;
            queueThread = new Thread(new ThreadStart(messProcessor)) { IsBackground = true };
            queueThread.Start();
            
        }

        /// <summary>
        /// Обработчик выполнения очереди сообщений.
        /// </summary>
        protected void messProcessor()
        {
            ServerTask _task;
            while (true)
            {
                try
                {
                    if (queue.Count == 0)
                        Thread.Sleep(1);
                    else
                    {
                        _task = queue.Dequeue();
                        MessageSucc(ref _task, ref server, ref queue);
                    }
                }
                catch (Exception _exc)
                {
                    Console.WriteLine("ERROR: "+_exc.Message);
                }
            }
        }
        /// <summary>
        /// Запись сообщений в очередь.
        /// </summary>
        /// <param name="data">Данные в виде строки</param>
        /// <param name="client">Клиент</param>
        public void SetTask(string data, System.Net.Sockets.TcpClient client)
        {
            queue.Enqueue(new ServerTask() { _client = client, _data = data });
            if (data == "/datebot") dbot.BotOutput(data,client);
            if (data.Contains("/searchbot ")) sbot.BotOutput(data, client);
            if (data.Contains("/weatherbot")) wbot.BotOutput(data,client);
            if (data.StartsWith("/smsbot")) smsbot.BotOutput(data, client);
            if (data.StartsWith("/users")) server.ClientLister(data,client);
            if (data.StartsWith("/sendto")) server.SendTo(data);
        }
        /// <summary>
        /// Запись сообщений в очередь.
        /// </summary>
        /// <param name="t">Ссылка на готовый объект структуры ServerTask</param>
        public void SetTask(ServerTask t)
        {
            queue.Enqueue(t);
        }
    }
}