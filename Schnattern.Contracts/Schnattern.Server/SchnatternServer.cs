using System;
using System.Collections.Generic;
using System.IO;
using Schnattern.Contracts;

namespace Schnattern.Server
{
    public class SchnatternServer : IServer
    {
        private List<string> users = new List<string>();
        public void Login(string username)
        {
            users.Add(username);
            Console.WriteLine($"{username} hat sich eingeloggt");
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
