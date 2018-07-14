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
    public partial class Form4 : Form
    {
        public int _counter = 4;
        public Form4()
        {
            InitializeComponent();
        
            Timer timer;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerEventProcessor);
     
            timer.Start();

        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
           
            _counter -= 1;
            if(_counter == 0) {

                Opacity = 1;      //first the opacity is 0

                timer2.Interval = 10;  //we'll increase the opacity every 10ms
                timer2.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
                timer2.Start();

            }
        }
        void fadeIn(object sender, EventArgs e)
        {

            if (Opacity == 0)
            {
                timer1.Stop();
                this.Hide();
                this.Close();
            }
            //this stops the timer if the form is completely displayed
            else
            {
                Opacity -= 0.025;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
