using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            this.Hide();


            info();
            timer1.Start();
        }
        public void info()
        {

           

            //    TcpClient client = new TcpClient("78.114.55.200", 9856);
            //    NetworkStream n = client.GetStream();
            //    byte[] buffer = new byte[client.ReceiveBufferSize];
            //    int data = n.Read(buffer, 0, client.ReceiveBufferSize);
            //    string msgs = Encoding.Unicode.GetString(buffer, 0, data);
                
            //    if (msgs.Contains("SendNotifRequest"))
            //    {
            //    MessageBox.Show(msgs);
            //        string[] splitter = msgs.Split('|');
            //        string id = splitter[0];
            //        string username = splitter[1];
            //        string texte = splitter[2];
            //        Form6 frm = new Form6();
            //        Form6.texte = texte;
            //    if (username == Form1.usernameacc)
            //    {
            //        frm.Show();
            //    }


            
            //    }
            //else
            //{
              
            //}

            
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //info();
        }
    }
}
