using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    /// <summary>
    ///  Интерфейс, определяющий правила для класса Task.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Ответное сообщение клиенту от сервера об успешном отправлении данных. Счетчик очереди.
        /// </summary>
        /// <param name="_task">Экземпляр класса ServerTask</param>
        /// <param name="server">Сервер</param>
        /// <param name="queue">Очередь</param>
        void MessageSucc(ref ServerTask _task, ref ChatServer server, ref Queue<ServerTask> queue);
    }
}
