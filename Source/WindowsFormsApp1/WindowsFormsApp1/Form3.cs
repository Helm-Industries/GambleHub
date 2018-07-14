using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public static string mailstr;
        public static string password;
        public static string ip = "127.0.0.1";
        public Form5()
        {
            InitializeComponent();
            
        }

       
        MySqlDataAdapter adapter;

        DataTable table = new DataTable();

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
          
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e) //Connexion
        {
           
                TcpClient sendinfo = new TcpClient(ip, 9856);
           
            NetworkStream nsendinfo = sendinfo.GetStream();
            string msgsendinfo = "AuthRequest:|" + textBox_username.Text + "|" + textBox_password.Text;
            byte[] messagesendinfo = Encoding.Unicode.GetBytes(msgsendinfo);
            nsendinfo.Write(messagesendinfo, 0, messagesendinfo.Length);
                                            
                byte[] buffer = new byte[sendinfo.ReceiveBufferSize];
                int data = nsendinfo.Read(buffer, 0, sendinfo.ReceiveBufferSize);
                string msg = Encoding.Unicode.GetString(buffer, 0, data);




            if (msg.Contains("Connexion refusée")) // return
            {

                textBox_password.BorderColorFocused = Color.Red;
                textBox_username.BorderColorFocused = Color.Red;
                textBox_password.BorderColorIdle = Color.Red;
                textBox_username.BorderColorIdle = Color.Red;
                }
            if (msg.Contains("Connexion acceptée"))
            {
                //bon
                TcpClient clientsuc = new TcpClient(ip, 9856);
                NetworkStream nsuc = clientsuc.GetStream();
                string msgsuc = "Nouvelle connexion";
                byte[] messagesuc = Encoding.Unicode.GetBytes(msgsuc);
                nsuc.Write(messagesuc, 0, messagesuc.Length);
                this.Hide();
                Form1 form1 = new Form1();
                form1.ShowDialog();
                this.Close();
            }

            table.Clear();
        }

        private void textBox_password_OnValueChanged(object sender, EventArgs e)
        {
            password = textBox_password.Text;
        }

        private void textBox_username_OnValueChanged(object sender, EventArgs e)
        {
            mailstr = textBox_username.Text;
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            // inscription
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e) //rstmdp
        {

            TcpClient passrequest = new TcpClient(ip, 9856);
            NetworkStream nsuc = passrequest.GetStream();
            string msgsuc = "PasswordRequest:|" + mailstr;
            byte[] messagesuc = Encoding.Unicode.GetBytes(msgsuc);
            nsuc.Write(messagesuc, 0, messagesuc.Length);

            byte[] buffer = new byte[passrequest.ReceiveBufferSize];
            int data = nsuc.Read(buffer, 0, passrequest.ReceiveBufferSize);
            string msg = Encoding.Unicode.GetString(buffer, 0, data);

            if (msg == "Mail envoyé !")
            {
                Form1.notif = "sendmail";
                Form6 frm = new Form6();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Mail non trouvé / non envoyé");
            }
            
        }
    }
}
