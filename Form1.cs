using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ZedGraph;




namespace Supernova
{
    public partial class Form1 : Form
    {
        string[] portlar = SerialPort.GetPortNames(); //portları goruntulemek icin 
        public Form1()
        {
            InitializeComponent();
            /*
            solidGauge1.From = 0;
            solidGauge1.To = 3500;
           // solidGauge1.Value = Convert.ToInt16(label4.Text);

            solidGauge2.From = 0;
            solidGauge2.To = 100;
           // solidGauge2.Value = Convert.ToInt16(label60.Text);
           

            plotgraph();
           */

        }

        private void plotgraph()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.Title.Text = "Rocket Altitude and Payload Altitude";
            myPane.XAxis.Title.Text = "Time(s)";
            myPane.YAxis.Title.Text = "Altitude(m)";

            /*
            myPane.XAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 300;
            myPane.YAxis.Scale.Max = 3500;

            
                myCurvelabel4 = myPanelabel4.AddCurve(null, listPointslabel4, Color.Red, SymbolType.None);
                myCurvelabel18 = myPanelabel18.AddCurve(null, listPointslabel4, Color.Blue, SymbolType.None);
                myCurvelabel4.Line.Width = 3;
                myCurvelabel18.Line.Width = 3;
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0; //0, birinci port demektir.
            }
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.SelectedIndex = 1;
            label10.Text = "CONNECTION CLOSED";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            { 
                String cikti = serialPort1.ReadLine(); //gelen veriler okunur ciktinin icine aktarilir.
                String[] yenicikti = cikti.Split('*');
                System.Windows.Forms.Label[] label = { label5, label6, label22, label9, label7, label4};
               
                /* 
                  label5.Text = yenicikti[0];
                  label6.Text = yenicikti[1];
                  label22.Text = yenicikti[2];
                  label27.Text = yenicikti[3];
                  label28.Text = yenicikti[4];
                  label30.Text = yenicikti[5];
                  label34.Text = yenicikti[6];
                  label35.Text = yenicikti[7];
                  label37.Text = yenicikti[8];
                  label39.Text = yenicikti[9];
                  label9.Text = yenicikti[10];
              */
                for (int i = 0; i < yenicikti.Length; i++)
                {
                    label[i].Text = yenicikti[i];

                }



                serialPort1.DiscardInBuffer();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                timer1.Stop();
                throw;
            }
        }

        

        private void button2_Click(object sender, EventArgs e) //baglantı kurmak icin
        {
            timer1.Start();
            if (serialPort1.IsOpen == false)
            {
                if (comboBox1.Text == "")
                    return;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt16(comboBox2.Text);//string tanimlamistik,int yaptik
                try
                {
                    serialPort1.Open();
                    label10.Text = "CONNECTION OPEN";
                    label10.ForeColor = Color.OliveDrab;
                }
                catch (Exception hata)
                {
                    MessageBox.Show("hata" + hata.Message);
                    throw;
                }
            }
            else
            {
                label10.Text = "CONNECTION ESTABLISHED";
                label10.ForeColor = Color.OliveDrab;
            }
        }

        

        private void button3_Click(object sender, EventArgs e) //baglantiyi kesmek icin
        {
            timer1.Stop();
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label10.Text = "CONNECTION CLOSED";
                label10.ForeColor = Color.Firebrick;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) //form kapanırken seri port acik kalirsa
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit(); //uygulamanin kapanmasi icin
        }

        bool move;
        int mouse_x;
        int mouse_y;

        public object ChartValueType { get; }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Instagram: @supernovarocketteam");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Linkedin: Supernova Rocket Team");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Youtube: Supernova Rocketteam");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("E-mail: supernovarocketteam@gmail.com");
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //bu olmazsa veriler grafiğe gelmez
        }
    }
}

