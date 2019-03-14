using Schnattern.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schnattern.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClient
    {
        public MainWindow()
        {
            InitializeComponent();
            usernameTb.Text = "Fred";
            LoginFailed("");
        }

        IServer server;
        private void Login(object sender, RoutedEventArgs e)
        {
            var tcp = new NetTcpBinding();
            var cf = new DuplexChannelFactory<IServer>(this, tcp, new EndpointAddress("net.tcp://localhost:1"));

            server = cf.CreateChannel();
            server.Login(usernameTb.Text);
        }

        public void ShowText(string text)
        {
            chatLb.Items.Add(text);
        }

        public void ShowImage(Stream image)
        {
            throw new NotImplementedException();
        }

        public void ShowUsers(IEnumerable<string> users)
        {
            usersLb.ItemsSource = users;
        }

        public void LoginOk()
        {
            loginBtn.IsEnabled = false;
            usernameTb.IsEnabled = false;
            logoutBtn.IsEnabled = true;
            sendImageBtn.IsEnabled = true;
            sendTextBtn.IsEnabled = true;
        }

        public void LoginFailed(string msg)
        {
            if (!string.IsNullOrWhiteSpace(msg))
                MessageBox.Show(msg);

            loginBtn.IsEnabled = !false;
            usernameTb.IsEnabled = !false;
            logoutBtn.IsEnabled = !true;
            sendImageBtn.IsEnabled = !true;
            sendTextBtn.IsEnabled = !true;
        }
    }
}
