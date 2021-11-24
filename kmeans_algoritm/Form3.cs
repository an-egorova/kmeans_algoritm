using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kmeans_algoritm
{
    public partial class Form3 : Form
    {
        //public Form3(double correspondence, bool nextClasterization)
        //{
        //    InitializeComponent();
        //    label2.Text = "J = "+correspondence;
        //}
        public Form3()
        {
            InitializeComponent();
            label2.Text = "J = " + Form1.correspondence+", количество итераций = " + Form1.iteration;
            textBox1.Enabled = false;
            textBox1.Visible = false;
            button3.Enabled = false;
            button3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.nextClasterization = true;
            label3.Text = "Текущее минимальное значение J = "+Form1.minEqualVar;
            label4.Text = "Введите новое значение:";
            button1.Enabled = false;
            button2.Enabled = false;
            button1.Visible = false;
            button2.Visible = false;
            textBox1.Enabled = true;
            textBox1.Visible = true;
            button3.Enabled = true;
            button3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.nextClasterization = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.minEqualVar = Convert.ToDouble(textBox1.Text);
            this.Close();
        }
    }
}
