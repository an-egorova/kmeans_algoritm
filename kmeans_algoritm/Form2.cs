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

namespace kmeans_algoritm
{
    public partial class Form2 : Form
    {
        int[,,] clasteringAllInfoGraph;
        double[] bestPartitionGraph;
        int[,,] centroidsClusterAllGraph;
        int iterationGraph;
        int clusterCountGraph;
        int vecCountGraph;
        int[,] vectorCoordGraph;
        int iter = 0;
        SolidBrush[] Colors= new SolidBrush[140];
        Graphics graphics1;
        Bitmap bitmap1;
        double[,] iterationCountGraph;
        public Form2()
        {
            InitializeComponent();
            
        }
        
        
        private void Form2_Load(object sender, EventArgs e)
        {
            clasteringAllInfoGraph = Form1.clasteringAllInfo;
            bestPartitionGraph = Form1.bestPartition;
            centroidsClusterAllGraph = Form1.centroidsClusterAll;
            iterationGraph = Form1.iteration;
            clusterCountGraph = Form1.clusterCount;
            vecCountGraph = Form1.countAllVec;
            vectorCoordGraph = Form1.vectorCoord;
            iterationCountGraph = new double[10, Form1.countIterationVar];
            Brush();
            Colors[10] = new SolidBrush(Color.Black);
            textBox1.Text += "1: ";
            for (int j = 0; j < iterationGraph; j++)
            {
                textBox1.Text += Convert.ToString(bestPartitionGraph[j]) + " ";
                chart1.Series["Series1"].Points.Add(bestPartitionGraph[j]);
                iterationCountGraph[Form1.graphCount, j] = bestPartitionGraph[j];
            }
            textBox1.Text += Environment.NewLine;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            first();
            /*for (int j = 0; j < iterationGraph; j++)
            {
                Graphics g = pictureBox1.CreateGraphics();
                for (int i = 0; i < vecCountGraph; i++)
                {
                    g.FillEllipse(Colors[clasteringAllInfoGraph[j, i, 0]], vectorCoordGraph[i, 0] + 500, vectorCoordGraph[i, 1] + 500, 10, 10);
                }
                Thread.Sleep(2000);
                for (int i = 0; i < clusterCountGraph; i++)
                {
                    g.FillRectangle(Colors[i], centroidsClusterAllGraph[j, i, 0] + 500, centroidsClusterAllGraph[j, i, 1] + 500, 10, 10);
                }
                Thread.Sleep(2000);
                if (j < iterationGraph-1)
                {
                    pictureBox1.Image = null;
                    pictureBox1.Update();
                    Thread.Sleep(1000);
                }
                textBox1.Text += Convert.ToString(bestPartitionGraph[j])+" ";
            }*/
        }

        void Brush()
        {
            Colors[0] = new SolidBrush(Color.Green);
            Colors[1] = new SolidBrush(Color.Blue);
            Colors[2] = new SolidBrush(Color.Red);
            Colors[3] = new SolidBrush(Color.Violet);
            Colors[4] = new SolidBrush(Color.Purple);
            Colors[5] = new SolidBrush(Color.Pink);
            Colors[6] = new SolidBrush(Color.Lavender);
            Colors[7] = new SolidBrush(Color.Azure);
            Colors[8] = new SolidBrush(Color.Ivory);
            Colors[9] = new SolidBrush(Color.DimGray);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            iter++;
            picture(iter);            
            if (iter >= 0)
                button3.Visible = true;
            if (iter == iterationGraph-1)
                button1.Visible = false;
            textBox2.Text = Convert.ToString(iter+1);
        }

        void picture(int k)
        {
            pictureBox1.Image = null;
            pictureBox1.Update();
            Thread.Sleep(100);
            //Graphics g = pictureBox1.CreateGraphics();
            bitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics1 = Graphics.FromImage(bitmap1);
            for (int i = 0; i < vecCountGraph; i++)
            {
                graphics1.FillEllipse(Colors[clasteringAllInfoGraph[k, i, 0]], vectorCoordGraph[i, 0] + 1000, vectorCoordGraph[i, 1] + 1000, 10, 10);
            }
            for (int i = 0; i < clusterCountGraph; i++)
            {
                graphics1.FillRectangle(Colors[i], centroidsClusterAllGraph[k, i, 0] + 1000, centroidsClusterAllGraph[k, i, 1] + 1000, 10, 10);
            }
            pictureBox1.Image = bitmap1;
        }


        void first()
        {
            pictureBox1.Image = null;
            pictureBox1.Update();
            Thread.Sleep(100);
            //Graphics g = pictureBox1.CreateGraphics();
            bitmap1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics1 = Graphics.FromImage(bitmap1);
            
            for (int i = 0; i < vecCountGraph; i++)
            {
                graphics1.FillEllipse(Colors[10], vectorCoordGraph[i, 0] + 1000, vectorCoordGraph[i, 1] + 1000, 10, 10);
            }
            for (int i = 0; i < clusterCountGraph; i++)
            {
                graphics1.FillRectangle(Colors[i], centroidsClusterAllGraph[0, i, 0] + 1000, centroidsClusterAllGraph[0, i, 1] + 1000, 10, 10);
            }
            pictureBox1.Image = bitmap1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            iter--;
            if (iter == -1)
            {
                button3.Visible = false;
                first();
            }
            else
                picture(iter);
            if (iter < iterationGraph)
                button1.Visible = true;
            textBox2.Text = Convert.ToString(iter+1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private int zoom = 5;
        public int Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                pictureBox1.Width = pictureBox1.Image.Width / zoom*2;
                pictureBox1.Height = pictureBox1.Image.Height / zoom*2;
            }
        }

        public Image Image
        {
            get { return pictureBox1.Image; }
            set
            {
                pictureBox1.Size = value.Size;
                pictureBox1.Image = value;
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Zoom = this.trackBar1.Value;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clasteringAllInfoGraph = Form1.clasteringAllInfo;
            bestPartitionGraph = Form1.bestPartition;
            centroidsClusterAllGraph = Form1.centroidsClusterAll;
            iterationGraph = Form1.iteration;
            clusterCountGraph = Form1.clusterCount;
            vecCountGraph = Form1.countAllVec;
            vectorCoordGraph = Form1.vectorCoord;
            //graphCount
            for (int i = 0; i <= Form1.graphCount; i++)
            {
                chart1.Series["Series" + (i+1)].Points.Clear();
            }

            for (int j = 0; j < iterationGraph; j++)
            {
                iterationCountGraph[Form1.graphCount-1, j] = bestPartitionGraph[j];
            }
            for (int i = 0; i < Form1.graphCount; i++)
            {
                textBox1.Text += (i+1)+": ";
                for (int j = 0; j < iterationGraph; j++)
                {
                    textBox1.Text += Convert.ToString(bestPartitionGraph[j]) + " ";
                    chart1.Series["Series"+(i+1)].Points.Add(iterationCountGraph[i, j]); ;
                }
                textBox1.Text += Environment.NewLine;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Form1.graphCount = 0;
            for (int i = 0; i <= Form1.graphCount; i++)
            {
                chart1.Series["Series" + (i + 1)].Points.Clear();
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
