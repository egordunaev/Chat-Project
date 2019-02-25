using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;



namespace ChatLib
{
    /// <summary>
    /// 
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Словарь в котором хранятся логины и пароли пользователей.
        /// </summary>
        private Dictionary<string, string> Credentials = new Dictionary<string, string>();
        /// <summary>
        /// Подключение пользователя последствием проверки логина и пароля из файла.
        /// </summary>
        public Authentication()
        {
            var users = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\UserInfo.txt");
            foreach (var user in users)
            {
                var parts = user.Split('/');
                Credentials.Add(parts[0].Trim(), parts[1].Trim());
            }
            
        }
        /// <summary>
        /// Сверяет введенные пользователем данные и выдает булевый результат.
        /// </summary>
        /// <returns></returns>
        public bool ValidateCredentials()
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();
            return Credentials.Any(entry => entry.Key == username && entry.Value == password);
        }
    }
}
