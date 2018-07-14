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

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        public static string mailstr;
        public Form5()
        {
            InitializeComponent();

           
        }
      
        MySqlConnection connection = new MySqlConnection("datasource=mysql-gamblehuboff.alwaysdata.net; port=3306;Initial Catalog='gamblehuboff_db';username=158469;password=K8LhE283et");

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
            adapter = new MySqlDataAdapter("SELECT `email`, `password` FROM `users` WHERE `email` = '" + textBox_username.Text + "' AND `password` = '" + textBox_password.Text + "'", connection);
            adapter.Fill(table);

            if (table.Rows.Count <= 0)
            {
                //mauvais
                textBox_password.BorderColorFocused = Color.Red;
                textBox_username.BorderColorFocused = Color.Red;
                textBox_password.BorderColorIdle = Color.Red;
                textBox_username.BorderColorIdle = Color.Red;
            }
            else
            {
                //bon
                this.Hide();
                Form1 form1 = new Form1();
                form1.ShowDialog();
                this.Close();
            }

            table.Clear();
        }

        private void textBox_password_OnValueChanged(object sender, EventArgs e)
        {

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
    }
}
