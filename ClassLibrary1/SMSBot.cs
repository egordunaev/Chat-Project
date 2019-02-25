using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BotLib
{
    /// <summary>
    /// СМС бот.
    /// </summary>
    public class SMSBot :IBot
    {
        /// <summary>
        /// Метод отправки смс-сообщений.
        /// </summary>
        /// <param name="data">Полученные данные.</param>
        /// <param name="_client">Клиент сервера.</param>
        public void BotOutput(string data, TcpClient _client)
        {
            string actual_message = string.Empty;
            string number_sendto = string.Empty;
            actual_message = data.Substring(20);
            string[] splitted = data.Split(new char[] { ' ' });
            if (splitted[1].StartsWith("+")) number_sendto = splitted[1];
            string twilio_number = "---------REMOVED---------"; 
            string twilio_account_SID = "---------REMOVED---------";
            string twilio_account_TOCKEN = "---------REMOVED---------";
            TwilioClient.SetUsername("---------REMOVED---------");
            TwilioClient.SetPassword("---------REMOVED---------");
            //Инициализирует клиента сервиса Twilio куда входят SID-номер клиента, API-TOCKEN пользователя.
            TwilioClient.Init(username: twilio_account_SID, password: twilio_account_TOCKEN, accountSid: twilio_account_SID);
            try
            {
                // Создание и отправка смс-сообщения, куда входят номер получателя, номер отправки и сообщение.
                MessageResource.Create(to: new PhoneNumber(number_sendto), from: new PhoneNumber(twilio_number), body: actual_message);
                byte[] _s = Encoding.Default.GetBytes("SMSBot: Done!" + "\n");
                _client.Client.Send(_s);
            }
            catch (Twilio.Exceptions.TwilioException exc)
            {
                byte[] _s = Encoding.Default.GetBytes("ERROR: "+exc.Message + "\n");
                _client.Client.Send(_s);
            }
            
        }
    }
}
