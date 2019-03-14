using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Schnattern.Contracts;

namespace Schnattern.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SchnatternServer : IServer
    {
        private List<string> users = new List<string>();
        public void Login(string username)
        {
            var callBack = OperationContext.Current.GetCallbackChannel<IClient>();

            if (users.Any(x => x.Equals(username, StringComparison.CurrentCultureIgnoreCase)))
            {
                callBack.LoginFailed("Der Benutzername ist bereit in verwendung");
                Console.WriteLine($"Login: {username} FAILED");
            }
            else
            {
                Console.WriteLine($"Login: {username} OK");
                users.Add(username);
                callBack.ShowText($"Hallo {username}");
                callBack.ShowUsers(users);
                callBack.LoginOk();
            }
        }

        public void Logout()
        {
            Console.WriteLine($"Logout...");
        }

        public void SendImage(Stream image)
        {
            Console.WriteLine($"SendImage...");
        }

        public void SendText(string text)
        {
            Console.WriteLine($"SendText: {text}");
        }
    }
}
