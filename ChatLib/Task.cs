using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{ 
    /// <summary>
    /// Реализует статус отправки сообщения из очереди и показывает количество сообщений в очереди.
    /// </summary>
    public class Task: ITask
    {
        /// <summary>
        /// Ответное сообщение клиенту от сервера об успешном отправлении данных. Счетчик очереди.
        /// </summary>
        /// <param name="_task">Экземпляр класса ServerTask.</param>
        /// <param name="server">Сервер.</param>
        /// <param name="queue">Очередь.</param>
       public void MessageSucc(ref ServerTask _task, ref ChatServer server, ref Queue<ServerTask> queue)
        {
            Console.WriteLine("Client {0}: {1}", _task._client.Client.RemoteEndPoint, _task._data);
            _task._client.Client.Send(Encoding.Default.GetBytes("Queue: Message Sent!"));
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Queue message: {0}", queue.Count);
        }
    }
}
