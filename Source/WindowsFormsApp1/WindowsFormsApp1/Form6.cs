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

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public System.Windows.Forms.Timer timer3;
        public int counter = 3;
        public Form6()
        {
            InitializeComponent();
            this.TopMost = true;
            
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            if(Form1.notif == "errorfunds")
            {
                pictureBox2.Image = Properties.Resources.Delete_64px;
                this.BackColor = Color.Brown;
                bunifuCustomLabel1.Text = "Fonds Insuffisants !";
            }
            if(Form1.notif == "donatethx")
            {
                pictureBox2.Image = Properties.Resources.Checkmark_64px;
                bunifuCustomLabel1.Text = "Merci pour votre donation !";
                this.BackColor = Color.SeaGreen;
            }
            if (Form1.notif == "already")
            {
                bunifuCustomLabel1.Location = new Point(bunifuCustomLabel1.Location.X ,bunifuCustomLabel1.Location.Y - 13); 
                pictureBox2.Image = Properties.Resources.Delete_64px;
                this.BackColor = Color.DarkOrange;
                bunifuCustomLabel1.Text = "Vous possédez déjà ces \n       avantages !";
            }
            if (Form1.notif == "")
            {
                pictureBox2.Image = Properties.Resources.Checkmark_64px;
                this.BackColor = Color.SeaGreen;
                bunifuCustomLabel1.Text = "Merci pour votre achat !";
            }
            if(Form1.notif == "rstsuccess")
            {
                pictureBox2.Image = Properties.Resources.Checkmark_64px;
                this.BackColor = Color.FromArgb(0, 102, 204);
                bunifuCustomLabel1.Text = "Mot de passe réinitialisé !";

                    //0;102;204
            }
            if(Form1.notif == "sendmail")
            {
                pictureBox2.Image = Properties.Resources.Checkmark_64px;
                this.BackColor = Color.FromArgb(0, 102, 204);
                bunifuCustomLabel1.Text = "Mot de passe envoyé !";
            }
            if(Form1.notif == "gamefull")
            {
                pictureBox2.Image = Properties.Resources.Delete_64px;
                this.BackColor = Color.Brown;
                bunifuCustomLabel1.Text = "La partie est pleine !";
            }
            if (Form1.notif == "joinsuccess")
            {
                pictureBox2.Image = Properties.Resources.Checkmark_64px;
                this.BackColor = Color.SeaGreen;
                bunifuCustomLabel1.Text = "Rejoind avec succès !";
            }
            if (Form1.notif == "retraitsucces")
            {
                pictureBox2.Image = Properties.Resources.Checkmark_64px;
                this.BackColor = Color.SeaGreen;
                bunifuCustomLabel1.Text = "Retrait demandé !";
            }
            if (Form1.notif == "alreadyretrait")
            {
                pictureBox2.Image = Properties.Resources.Checkmark_64px;
                this.BackColor = Color.Brown;
                bunifuCustomLabel1.Text = "Retrait déjà en cours !";
            }

            Opacity = 0;      //first the opacity is 0

            timer1.Interval = 10;  //we'll increase the opacity every 10ms
            timer1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
            timer1.Start();
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width - 40, screen.WorkingArea.Top - this.Height + 175);

        }

        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
            {

               
                timer1.Stop();
                //this stops the timer if the form is completely displayed


                //temps affiché pour les connard comme thomas
                Task.Delay(2000).Wait();
                

                    //timer2.Interval = 10;
                    //timer2.Tick += new EventHandler(fadeOut);
                    //timer2.Start();
                
            }
            else
                Opacity += 0.025;
        }
        void fadeOut(object sender, EventArgs e)
        {
            if(Opacity <= 0)
            {
                timer2.Stop();
                this.Hide();
                this.Close();
            }
            else
                Opacity -= 0.025;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
         
           
     
     
        }
    }
}
