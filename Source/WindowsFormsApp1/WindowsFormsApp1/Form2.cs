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
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {


        public string prenomstr;
        public string nomstr;
        public string mailstr;
        public static int vip;
        public static int age;
        public static int balance;
        public Form2()
        {
            InitializeComponent();
            prenomstr = prenom.Text;
            nomstr = nom.Text;
            mailstr = email.Text;


        }

  
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string sexes = "";
            if (homme.Checked == true)
            {
                sexes = "homme";
            }

            if (femme.Checked == true)
            {
                sexes = "femme";
            }
            if (prenom.Text.Length == 0 || prenom.Text == "Prénom")
            {
                prenom.BorderColorIdle = Color.Red;
                prenom.BorderColorFocused = Color.Red;
            }
            else
            {
                if (nom.Text.Length == 0 || nom.Text == "Nom")
                {
                    nom.BorderColorIdle = Color.Red;
                    nom.BorderColorFocused = Color.Red;
                }
                else
                {
                    if (email.Text.Length == 0 || email.Text == "E-mail")
                    {
                        email.BorderColorIdle = Color.Red;
                        email.BorderColorFocused = Color.Red;
                    }
                    else
                    {
                        if (ages.Text.Length == 0 || ages.Text == "Age") //ajouter chiffre seulement
                        {
                            ages.BorderColorIdle = Color.Red;
                            ages.BorderColorFocused = Color.Red;
                        }
                        else
                        {
                            if (homme.Checked == false && femme.Checked == false)
                            {
                                homme.BackColor = Color.Red;
                                femme.BackColor = Color.Red;
                            }
                            else
                            {
                                if (mdp.Text.Length == 0 || mdp.Text == "Mot de passe")
                                {
                                    mdp.BorderColorIdle = Color.Red;
                                    mdp.BorderColorFocused = Color.Red;
                                }
                                else
                                {
                                    if (pseudo.Text.Length == 0 || pseudo.Text == "Pseudo")
                                    {
                                        pseudo.BorderColorIdle = Color.Red;
                                        pseudo.BorderColorFocused = Color.Red;
                                    }
                                    else
                                    {
                                        TcpClient sendregisterinfo = new TcpClient(Form5.ip, 9856);
                                        NetworkStream nsendinfo = sendregisterinfo.GetStream();
                                        string msgsendinfo = "RegisterRequest:|" + prenom.Text + "|" + nom.Text + "|" + email.Text + "|" + ages.Text + "|" + sexes + "|" + mdp.Text + "|" + "0" + "|" + "0" + "|" + "0" + "|" + pseudo.Text;
                                        byte[] messagesendregisterinfo = Encoding.Unicode.GetBytes(msgsendinfo);
                                        nsendinfo.Write(messagesendregisterinfo, 0, messagesendregisterinfo.Length);
                                        // on attend la réponse
                           
                                         byte[] buffer = new byte[sendregisterinfo.ReceiveBufferSize];
                                        int data = nsendinfo.Read(buffer, 0, sendregisterinfo.ReceiveBufferSize);
                                        string msg = Encoding.Unicode.GetString(buffer, 0, data);

                                        if(msg == "Mail déjà pris")
                                        {
                                            email.BorderColorIdle = Color.Red;
                                            email.BorderColorFocused = Color.Red;
                                            MessageBox.Show("Mail déjà pris !");
                                        }
                                        else
                                        {
                                            if(msg == "Pseudo déjà pris")
                                            {
                                                pseudo.BorderColorIdle = Color.Red;
                                                pseudo.BorderColorFocused = Color.Red;
                                                MessageBox.Show("Pseudo déjà pris !");
                                            }
                                            else
                                            {
                                                if(msg == "Inscrit avec succès")
                                                {
                                                    this.Hide();
                                                    Form5 frm = new Form5();
                                                    frm.ShowDialog();
                                                    this.Close();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
       
           
          }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5();
            form5.ShowDialog();
            this.Close();
        }

        
        private void femme_OnChange(object sender, EventArgs e)
        {
            if (femme.Checked == true)
            {
                homme.Checked = false;
            }
            if (homme.Checked == true)
            {
                femme.Checked = false;
            }
        }

        private void homme_OnChange(object sender, EventArgs e)
        {
            if (homme.Checked == true)
            {
                femme.Checked = false;
            }
            if (femme.Checked == true)
            {
                homme.Checked = false;
            }
        }

        private void prenom_OnValueChanged(object sender, EventArgs e)
        {
            
    }

        private void nom_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void email_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void pseudo_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void age_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void mdp_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
