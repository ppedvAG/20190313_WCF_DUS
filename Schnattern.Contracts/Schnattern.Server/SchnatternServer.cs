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
        private Dictionary<string, IClient> users = new Dictionary<string, IClient>();
        public void Login(string username)
        {
            var callBack = OperationContext.Current.GetCallbackChannel<IClient>();

            if (users.Any(x => x.Key.Equals(username, StringComparison.CurrentCultureIgnoreCase)))
            {
                callBack.LoginFailed("Der Benutzername ist bereit in verwendung");
                Console.WriteLine($"Login: {username} FAILED");
            }
            else
            {
                Console.WriteLine($"Login: {username} OK");
                users.Add(username, callBack);
                callBack.ShowText($"Hallo {username}");

                SendUserListToAll();

                callBack.LoginOk();
            }
        }

        private void SendUserListToAll()
        {
            Console.WriteLine($"SendUserListToAll...");
            SendToAll(x => x.ShowUsers(users.Select(y => y.Key)));
        }

        public void Logout()
        {
            var sender = users.First(x => x.Value == OperationContext.Current.GetCallbackChannel<IClient>());
            users.Remove(sender.Key);
            SendUserListToAll();
            Console.WriteLine($"{sender.Key} logged out");
        }

        public void SendImage(Stream image)
        {
            Console.WriteLine($"SendImage...");

            var ms = new MemoryStream();
            image.CopyTo(ms);
            SendToAll(x =>
            {
                ms.Position = 0;
                x.ShowImage(ms);
            });
        }

        public void SendText(string text)
        {
            var sender = users.First(x => x.Value == OperationContext.Current.GetCallbackChannel<IClient>());

            var msg = $"{sender.Key}: {text}";

            Console.WriteLine($"SendText: {msg}");
            SendToAll(x => x.ShowText(msg));
        }

        private void SendToAll(Action<IClient> client)
        {
            foreach (var item in users.ToList())
            {
                try
                {
                    client.Invoke(item.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"SendToAll: {ex.Message}");
                    Console.WriteLine($"{item.Key} wurde entfernt!");
                    users.Remove(item.Key);
                    SendUserListToAll();
                }
            }
        }
    }
}
