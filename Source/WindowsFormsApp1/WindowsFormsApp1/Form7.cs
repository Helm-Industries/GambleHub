using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        MySqlConnection restmdp = new MySqlConnection("datasource=mysql-gamblehuboff.alwaysdata.net; port=3306;Initial Catalog='gamblehuboff_db';username=158469;password=K8LhE283et");
        public Form7()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            // valider
            if(bunifuMetroTextbox1.Text == Form1.mdp && bunifuMetroTextbox2.Text != Form1.mdp)
            {
                restmdp.Open();
                MySqlCommand mdpr = new MySqlCommand("UPDATE users SET password = '"+ bunifuMetroTextbox2.Text + "' WHERE email = '"+ Form5.mailstr +"'",restmdp);
                using (MySqlDataReader reader = mdpr.ExecuteReader())
                {
                    Form1.notif = "rstsuccess";
                    Form6 ntf = new Form6();
                    ntf.Show();
                }
                this.Hide();
                this.Close();
                //hide & close
            }
            if (bunifuMetroTextbox1.Text != Form1.mdp)
            {
                bunifuMetroTextbox1.BorderColorIdle = Color.Red;
            }
        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
