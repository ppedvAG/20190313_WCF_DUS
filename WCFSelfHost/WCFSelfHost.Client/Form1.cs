using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFSelfHost.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var cf = new ChannelFactory<ITrainDepot>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:1"));
            //var cf = new ChannelFactory<ITrainDepot>(new BasicHttpBinding(), new EndpointAddress("http://localhost:2"));
            //var cf = new ChannelFactory<ITrainDepot>(new WSHttpBinding(), new EndpointAddress("http://localhost:3"));
            //var cf = new ChannelFactory<ITrainDepot>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/Trains"));



            var client = cf.CreateChannel();
            dataGridView1.DataSource = client.GetTrains();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var cf = new ChannelFactory<ITrainDepot>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/Trains"));
            var cf = new ChannelFactory<ITrainDepot>(new BasicHttpBinding(), new EndpointAddress("http://localhost:2"));
            var client = cf.CreateChannel();

            try
            {

                dataGridView1.DataSource = client.GetTrainsWithException();
            }
            catch (FaultException<TrainException> tex)
            {
                MessageBox.Show($"Fehler: {tex.Message} Leistung:{tex.Detail.DefectTrain.Leistung}");
            }
        }
    }
}
