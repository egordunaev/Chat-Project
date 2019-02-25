using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChatLib
{
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    public class Signup
    {
        Authentication auth = new Authentication();
        /// <summary>
        /// Словарь, в который записываются данные пользователей.
        /// </summary>
        private Dictionary<string, string> Credentials = new Dictionary<string, string>();
        /// <summary>
        /// Проверка файла с информацией пользователей.
        /// </summary>
        public void ValidateFiles()
        {
            var users = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\UserInfo.txt");
            foreach (var user in users)
            {
                var parts = user.Split('/');
                Credentials.Add(parts[0].Trim(), parts[1].Trim());
            }
        }
        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        public void SigningUp()
        {
            while (true)
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                if (!Credentials.ContainsKey(username))
                {
                    Console.Write("Enter password: ");
                    string password = Console.ReadLine();

                    File.AppendAllText(Directory.GetCurrentDirectory() + "\\UserInfo.txt", "\n"+username + "/" + password);
                    Console.WriteLine("You've signed up!");
                    auth.ValidateCredentials();
                    break;
                }
                else
                {
                    Console.WriteLine("ERROR: Current user already exists!");
                }
            }
        }
    }
}
