using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form

    {
        public static string mdp;
        public static string notif;
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        int timeLeft = 5;

        public static string actual = "BETA V1.0";

       MySqlConnection achatconnection = new MySqlConnection("datasource=mysql-gamblehuboff.alwaysdata.net; port=3306;Initial Catalog='gamblehuboff_db';username=158469;password=K8LhE283et");
        

        DataTable table = new DataTable();

        public static string resetmdp;
        public static string sexe;
        public static int vips;
        public static float solde;
        public static string auteur = "";
        public static string game = "";
        public static string oponent = "";
        public static int amount;
        public static int tax;
        public static int slot;
        public static string winner = "";
        public static int total;

        void Form1_Shown(object sender, EventArgs e)
        {
        }


        public void info()
        {

            TcpClient client = new TcpClient(Form5.ip, 9856);
            NetworkStream n = client.GetStream();
            string msg = "PlayerInfoRequest:|" + Form5.mailstr;
            byte[] message = Encoding.Unicode.GetBytes(msg);
            n.Write(message, 0, message.Length);

            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = n.Read(buffer, 0, client.ReceiveBufferSize);
            string msgs = Encoding.Unicode.GetString(buffer, 0, data);
            msgs.Trim();
            string[] splitter = msgs.Split('|');
            nomcompte.Text = splitter[1];
            prenomcompte.Text = splitter[2];
            string sexe = splitter[3];
            if (sexe == "homme")
            {
                pictureBox5.Image = Properties.Resources.ACCOUNT_MALE1;
            }
            else
            {
                pictureBox5.Image = Properties.Resources.ACCOUNT_FEMALE1;
            }
            agecompte.Text = splitter[4];
            int vips = int.Parse(splitter[5]);
            if (vips == 0)
            {
                vipcompte.Text = "Joueur";
            }
            if (vips == 1)
            {
                vipcompte.Text = "Donateur";
            }
            if (vips == 2)
            {
                vipcompte.Text = "V.I.P";
            }
            if (vips == 3)
            {
                vipcompte.Text = "Premium";
            }
            if (vips > 3)
            {
                vipcompte.Text = "Administrateur";
            }
            emailcompte.Text = splitter[6];
            soldecompte.Text = splitter[7];
            n.Close();
            client.Close();
        }

        public Form1()
        {

            InitializeComponent();
            achatconnection.Open();
            timer3.Start();
            //pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            //flowLayoutPanel1.AutoScroll = false;
            //flowLayoutPanel1.Controls.Add(pictureBox3);
            //flowLayoutPanel1.HorizontalScroll.Enabled = false;
           // flowLayoutPanel1.AutoScroll = true;
            ROULETTE.Visible = false;
            info();
        }
        


        
       


        private void Form1_Load(object sender, EventArgs e)
        {
           
            



            //statutpaysafe.Text = new WebClient().DownloadString("http://paytoreceive.livehost.fr/wp-content/paysafecard.txt");

            //patchtext.Text = new WebClient().DownloadString("http://paytoreceive.livehost.fr/wp-content/version.txt");

           
            timer2.Interval = 3500;

        
            Opacity = 0;      //first the opacity is 0

            timer1.Interval = 10;  //we'll increase the opacity every 10ms
            timer1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
            timer1.Start();



           // da.Fill(table);
          

      

            

        }
        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                timer1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.025;
        }



        public void withdrawrequest()
        {
            TcpClient client = new TcpClient(Form5.ip, 9856);
            NetworkStream n = client.GetStream();
            string msg = "WithdrawRequest:|" + Form5.mailstr + "|" + solde.ToString();
            byte[] message = Encoding.Unicode.GetBytes(msg);
            n.Write(message, 0, message.Length);

            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = n.Read(buffer, 0, client.ReceiveBufferSize);
            string msgs = Encoding.Unicode.GetString(buffer, 0, data);

            if (msgs == "withdrawsucces")
            {
                Form6 frm = new Form6();
                Form1.notif = "retraitsucces";
                frm.Show();

            }
            if (msgs == "alreadywithdraw")
            {
                Form6 frm = new Form6();
                Form1.notif = "alreadyretrait";
                frm.Show();

            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient(Form5.ip, 9856);
            NetworkStream n = client.GetStream();
            string msg = "Nouvelle deconnexion";
            byte[] message = Encoding.Unicode.GetBytes(msg);
            n.Write(message, 0, message.Length);
            Application.Exit();
        }

        private void menu_Click(object sender, EventArgs e)
        {
            var logo1 = new Point(16, 32);
            var logo2 = new Point(271, 32);
            var news = new Point(316, 291);
            var newss = new Point(60, 56);
            if (sidemenu.Width == 60)
            {
                //panel1.Location = news;
                menu.Location = logo2;
                sidemenu.Visible = false;
                copyright.Visible = true;
                sidemenu.Width = 334;
                PanelAnimator2.ShowSync(sidemenu);


                //panel1.Location = news;


            }
            else
            {
                //panel1.Location = newss;
                menu.Location = logo1;
                copyright.Visible = false;
                sidemenu.Visible = false;
                sidemenu.Width = 60;
                PanelAnimator.ShowSync(sidemenu);


            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            //options

            //panel1.Visible = false;
            //flowLayoutPanel1.Visible = false;
            vippanel.Visible = false;
            jeuxpanel.Visible = false;
            contactpanel.Visible = false;
            moncomptepanel.Visible = true;
            info();
       
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            // premium

            //panel1.Visible = false;
            //flowLayoutPanel1.Visible = false;
            vippanel.Visible = false;
            jeuxpanel.Visible = true;
            contactpanel.Visible = false;
            moncomptepanel.Visible = false;
           

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //convertir

            //panel1.Visible = false;
            //flowLayoutPanel1.Visible = false;
            vippanel.Visible = true;
            jeuxpanel.Visible = false;
            contactpanel.Visible = false;
            moncomptepanel.Visible = false;
        }

        private void convertpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //contact

            //panel1.Visible = false;
            //flowLayoutPanel1.Visible = false;
            vippanel.Visible = false;
            jeuxpanel.Visible = false;
            contactpanel.Visible = true;
            moncomptepanel.Visible = false;
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            //informations

            //panel1.Visible = true;
            //flowLayoutPanel1.Visible = true;
            vippanel.Visible = false;
            jeuxpanel.Visible = false;
            contactpanel.Visible = false;
            moncomptepanel.Visible = false;
        }

        private void optionspanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void sidemenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {

        }

        private void contactpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void infopanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel11_Click(object sender, EventArgs e)
        {

        }

        private void optionspanel_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {



        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
        
            //paysafecard
        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void discord_Click(object sender, EventArgs e)
        {


        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/UzbmRGJ");
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            Process.Start("");
        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCgLqXl6TyDRjW7GAhcts_9g?view_as=subscrib");
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/UzbmRGJ");
        }

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/UzbmRGJ");
        }

        private void bunifuImageButton13_Click(object sender, EventArgs e)
        {
            Process.Start("");
        }

        private void bunifuImageButton14_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCgLqXl6TyDRjW7GAhcts_9g?view_as=subscrib");
        }

        private void bunifuImageButton15_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/UzbmRGJ");
        }

        private void bunifuImageButton16_Click(object sender, EventArgs e)
        {
            Process.Start("");
        }

        private void bunifuImageButton17_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCgLqXl6TyDRjW7GAhcts_9g?view_as=subscrib");
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            Process.Start("");
        }

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCgLqXl6TyDRjW7GAhcts_9g?view_as=subscrib");
        }

        private void bunifuImageButton18_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/UzbmRGJ");
        }

        private void bunifuImageButton19_Click(object sender, EventArgs e)
        {
            Process.Start("");
        }

        private void bunifuImageButton20_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCgLqXl6TyDRjW7GAhcts_9g?view_as=subscrib"); //Wut
        }

        private void bunifuMetroTextbox5_OnValueChanged(object sender, EventArgs e)
        {

        }


        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {


        }

        private void bunifuCustomLabel6_Click_1(object sender, EventArgs e)
        {
        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
          
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            

        }
        private void timer2_Tick(object sender, EventArgs e) //fade je crois
        {
            if (timeLeft > 0)
            {

                timeLeft = timeLeft - 1;

            }
            else
            {

            }

        }

        private void bunifuCheckbox1_OnChange_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {


   
         
        }
         


        private void texttc_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox4_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox5_OnValueChanged_1(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox6_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)//mail jsp a quoi sa va servir(contact je pense)
        {
            MailMessage mailss = new MailMessage();


            mailss.From = new MailAddress(bunifuCustomTextbox1.Text);
            mailss.To.Add(new MailAddress("gamblehuboff@gmail.com"));

            mailss.Subject = bunifuCustomTextbox2.Text;
            StringBuilder sbs = new StringBuilder();


            sbs.Append(bunifuCustomTextbox3.Text + "\n\n Ce mail a été envoyé par : " + bunifuCustomTextbox1.Text);



            mailss.Body = sbs.ToString();



            mailss.Priority = MailPriority.High;
            SmtpClient smtpss = new SmtpClient("smtp.gmail.com", 587);
            smtpss.EnableSsl = true;
            smtpss.Credentials = new NetworkCredential("gamblehuboff@gmail.com", "H2762KDbzt");
            smtpss.Send(mailss);
            bunifuCustomTextbox1.Clear();
            bunifuCustomTextbox2.Clear();
            bunifuCustomTextbox3.Clear();
            MessageBox.Show("Mail envoyé !");
        }

        private void bunifuCustomTextbox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomTextbox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void _optnewsletr_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void nomcompte_Click(object sender, EventArgs e)
        {

        }

        private void prenomcompte_Click(object sender, EventArgs e)
        {

        }

        private void emailcompte_Click(object sender, EventArgs e)
        {

        }

        private void vipcompte_Click(object sender, EventArgs e)
        {

        }

        private void agecompte_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click_2(object sender, EventArgs e)
        {
            
            Form7 frms = new Form7();
            frms.Show();
         

        }

        private void soldecompte_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton3_Click_1(object sender, EventArgs e)//donation 2$
        {
            
            float donate = solde - 2;
            if (solde < 2)
            {
               
                notif = "errorfunds";
                Form6 frm = new Form6();
                frm.Show();
            }

            if (solde >= 2)
            {
                if (vips <= 1)
                {
                    MySqlCommand donaterankcmd = new MySqlCommand("UPDATE users SET balance = '" + donate + "', vip = '1' WHERE email = '" + Form5.mailstr + "'", achatconnection);

                    using (MySqlDataReader reader = donaterankcmd.ExecuteReader())
                    {
                        //MessageBox.Show("Merci pour votre donation !");
                        notif = "donatethx";
                        Form6 frm = new Form6();
                        frm.Show();
                       
                    }
                }
                else
                {
                    MySqlCommand donatecmd = new MySqlCommand("UPDATE users SET balance = '" + donate + "' WHERE email = '" + Form5.mailstr + "'", achatconnection);

                    using (MySqlDataReader reader = donatecmd.ExecuteReader())
                    {
                        // MessageBox.Show("Merci pour votre donation !");
                        notif = "donatethx"; // <- ntm
                        Form6 frm = new Form6();
                        frm.Show();


                    }

                }
            
            }

            
           
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e) //vip 5$
        {
            float achvip = solde - 5;
            if (vips >= 2)
            {
                notif = "already";
              
                Form6 frm = new Form6();
                frm.Show();
            }
            if (solde < 5)
            {
                if (vips < 2)
                {
                    notif = "errorfunds";
                    Form6 frm = new Form6();
                    frm.Show();
                }
            }

            if (solde >= 5 && vips <= 1)
            {          
             
                MySqlCommand achatvip = new MySqlCommand("UPDATE users SET vip = '2', balance = '"+ achvip +"' WHERE email = '" + Form5.mailstr + "'", achatconnection);
                using (MySqlDataReader reader = achatvip.ExecuteReader())
                {
                    notif = "";
                    Form6 frm = new Form6();
                    frm.Show();



                }
              

            }

            
        }

        private void bunifuImageButton21_Click(object sender, EventArgs e) //premium 10$
        {
            float achpremium = solde - 10;
            if (vips >= 3)
            {
                notif = "already";
                Form6 frm = new Form6();
                frm.Show();
            }
            if (solde < 10)
            {
                if (vips < 3)
                {
                    notif = "errorfunds";
                    Form6 frm = new Form6();
                    frm.Show();
                }
            }

            if (solde >= 10 && vips < 3)
            {
                MySqlCommand achatvip = new MySqlCommand("UPDATE users SET vip = '3', balance = '" + achpremium + "' WHERE email = '" + Form5.mailstr + "'", achatconnection);
                using (MySqlDataReader reader = achatvip.ExecuteReader())
                {
                    notif = "";
                    Form6 frm = new Form6();
                    frm.Show();

                }
                
               

            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {

           
        } 
        public void matchmaking() // infos join
        {
           

           
        }
        private void refrsh()
        {
            this.Hide();
            this.Close();
            Form1 frm = new Form1();
            frm.Show();
        }//ferme & reouvre le logiciel


        private void bunifuFlatButton1_Click_2(object sender, EventArgs e)
        {

            
        } //ancien rafraichir

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
           }

        private void label1_Click_1(object sender, EventArgs e)
        {
      }

        private void join_match_1_Click(object sender, EventArgs e)
        {
          
        }

        private void poker_matchmaking_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton22_Click(object sender, EventArgs e)
        {
           }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void poker_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void bunifuImageButton22_Click_1(object sender, EventArgs e)
        {
            ROULETTE.Visible = true;
        }

        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {
            ROULETTE.Visible = false;
        }

        private void bunifuThinButton25_Click_1(object sender, EventArgs e)
        {
            withdrawrequest();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            
        }
    }
    }

