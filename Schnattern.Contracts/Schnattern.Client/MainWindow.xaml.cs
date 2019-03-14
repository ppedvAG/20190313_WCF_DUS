using Microsoft.Win32;
using Schnattern.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            usernameTb.Text = $"Fred_{Guid.NewGuid().ToString().Substring(0, 4)}";

            textTb.KeyUp += (s, e) =>
            {
                if (e.Key == Key.Enter)
                    SendText(s, null);
            };
            Closing += (s, e) => Logout(this, null);

            LoginFailed("");
        }

        IServer server;
        private void Login(object sender, RoutedEventArgs e)
        {
            var tcp = new NetTcpBinding();
            tcp.Security.Mode = SecurityMode.TransportWithMessageCredential;
            //tcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            tcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;



            tcp.MaxReceivedMessageSize = int.MaxValue;
            var adr = "net.tcp://192.168.178.156:1";

            EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity("RootCA");
            EndpointAddress address = new EndpointAddress(new Uri(adr), identity);
            var cf = new DuplexChannelFactory<IServer>(this, tcp, address);
            //cf.Credentials.Windows.ClientCredential.UserName = "Fred";
            //cf.Credentials.Windows.ClientCredential.Password = "123456";
            cf.Credentials.ClientCertificate.SetCertificate(StoreLocation.CurrentUser, StoreName.My,
                X509FindType.FindByThumbprint, "69ff17bd74b7e71da25dd06e2952276a4b956a36");

            cf.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;

            server = cf.CreateChannel();
            server.Login(usernameTb.Text);
        }

        public void ShowText(string text)
        {
            chatLb.Items.Add(text);
        }

        public void ShowImage(Stream image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);
            ms.Position = 0;

            var vb = new Viewbox();

            var img = new Image();
            img.BeginInit();
            img.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            img.Stretch = Stretch.None;
            img.EndInit();
            vb.Child = img;
            chatLb.Items.Add(vb);


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
            usersLb.ItemsSource = null;
        }

        private void SendText(object sender, RoutedEventArgs e)
        {
            server?.SendText(textTb.Text);
            textTb.Clear();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            server?.Logout();
            LoginFailed("");
            server = null;
        }

        private void SendImage(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "Katzenbilder|*.png;*.jpg|Alle Dateien|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                using (var fileStream = File.OpenRead(dlg.FileName))
                    server?.SendImage(fileStream);
            }

        }
    }
}
