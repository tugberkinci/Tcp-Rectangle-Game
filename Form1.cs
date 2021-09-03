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
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private TcpClient client { get; set; }
        public StreamReader STR { get; set; }
        public StreamWriter STW { get; set; }
        public string recieve { get; set; }
        public string TextToSend { get; set; }
        public int horizontal { get; set; }
        public int vertical { get; set; }
        public bool ConnectButtonWasClicked { get; set; }

        public Form1()
        {
            InitializeComponent();
            ConnectButtonWasClicked = false;
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());
            foreach(IPAddress adress in localIP)
            {
                if (adress.AddressFamily == AddressFamily.InterNetwork)
                {
                    ServerIPtextBox.Text = adress.ToString();
                }
            }
        }
       

        /// <summary>
        /// Starter of server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StratButton_Click(object sender, EventArgs e)
        {
             TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(ServerPorttextBox.Text));
            listener.Start();
            client = listener.AcceptTcpClient();
            STR = new StreamReader(client.GetStream());
            STW = new StreamWriter(client.GetStream());
            STW.AutoFlush = true;

            backgroundWorker1.RunWorkerAsync();

            this.label10.Visible = true;
            this.label10.Text = "Im Server!";

            VisibleOptions();
            
        }

        /// <summary>
        /// Connect for client side.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            
            client = new TcpClient();
            IPEndPoint IpEnd = new IPEndPoint(IPAddress.Parse(ClientIPtextBox.Text), int.Parse(ClientPorttextBox.Text));
            try
            {
                client.Connect(IpEnd);
                if(client.Connected)
                {
                    
                    this.label7.Visible = true;
                    this.label7.Text = "Connected to Server";

                    STR = new StreamReader(client.GetStream());
                    STW = new StreamWriter(client.GetStream());
                    STW.AutoFlush = true;
                    
                    backgroundWorker1.RunWorkerAsync();
                    this.label9.Visible = true;
                    this.label9.Text = "Im Client!";
                    ConnectButtonWasClicked = true;
                    this.label11.Visible = true;
                    

                    VisibleOptions();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Set compenents are non visible after connection.
        /// </summary>
        private void VisibleOptions()
        {
            this.Size = new Size(730, 450);
            this.BackColor = Color.Black;
            this.ClientIPtextBox.Visible = false;
            this.ClientPorttextBox.Visible = false;
            this.ServerIPtextBox.Visible = false;
            this.ServerPorttextBox.Visible = false;
            this.label1.Visible = false;
            this.label2.Visible = false;
            this.label3.Visible = false;
            this.label4.Visible = false;
            this.label5.Visible = false;
            this.label6.Visible = false;
            this.StratButton.Visible = false;
            this.ConnectButton.Visible = false;
            

        }

     
        /// <summary>
        /// Listener method.Also this method draws rectangel on client screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            while (client.Connected)
            {
                Graphics dc = this.CreateGraphics();
                Pen SalmonPen = new Pen(Color.DarkSalmon, 3);
                Pen BlackPen = new Pen(Color.Black, 3);
                dc.DrawRectangle(SalmonPen, horizontal, vertical, 100, 75);

                try
                {
                    recieve = STR.ReadLine();
                    this.label11.Invoke(new MethodInvoker(delegate ()
                    {
                        label11.Text = recieve;
                    }));


                    if (String.Equals(recieve, "Down"))
                    {
                        
                        dc.DrawRectangle(BlackPen, horizontal, vertical, 100, 75);
                        recieve = recieve + vertical++;
                        
                    }
                    if (String.Equals(recieve, "Up"))
                    {
                       
                        dc.DrawRectangle(BlackPen, horizontal, vertical, 100, 75);   
                        recieve = recieve + vertical--;
                        
                    }                        
                    if (String.Equals(recieve, "Left"))
                    {
                        
                        dc.DrawRectangle(BlackPen, horizontal, vertical, 100, 75);
                        recieve = recieve + horizontal--;
                       
                    }
                        
                    if (String.Equals(recieve, "Right"))
                    {
                        
                        dc.DrawRectangle(BlackPen, horizontal, vertical, 100, 75);
                        recieve = recieve + horizontal++;
                        
                    }
                 


                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }

        /// <summary>
        /// This method tells to client which arrow key pressed. Also it draws rectangle on server screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            
            if (client.Connected)
            {

                STW.WriteLine(TextToSend);
                //RectangleDrawer(horizontal, vertical, TextToSend);

                Graphics dc = this.CreateGraphics();
                Pen SalmonPen = new Pen(Color.DarkSalmon, 3);
                Pen BlackPen = new Pen(Color.Black, 3);
                dc.DrawRectangle(BlackPen, horizontal, vertical, 100, 75);

                if (String.Equals(TextToSend, "Down"))
                {

                    dc.DrawRectangle(BlackPen, horizontal, vertical++, 100, 75);
                    dc.DrawRectangle(SalmonPen, horizontal, vertical, 100, 75);


                }
                if (String.Equals(TextToSend, "Up"))
                {
                    dc.DrawRectangle(BlackPen, horizontal, vertical--, 100, 75);
                    dc.DrawRectangle(SalmonPen, horizontal, vertical, 100, 75);


                }
                if (String.Equals(TextToSend, "Left"))
                {
                    dc.DrawRectangle(BlackPen, horizontal--, vertical, 100, 75);
                    dc.DrawRectangle(SalmonPen, horizontal, vertical, 100, 75);


                }

                if (String.Equals(TextToSend, "Right"))
                {
                    dc.DrawRectangle(BlackPen, horizontal++, vertical, 100, 75);
                    dc.DrawRectangle(SalmonPen, horizontal, vertical, 100, 75);


                }

                this.label8.Invoke(new MethodInvoker(delegate ()
                {
                   label8.Text = TextToSend;
                }));
            }
            else
            {
                MessageBox.Show("Sending failed");
            }
            //backgroundWorker2.CancelAsync();

        }


        /// <summary>
        /// Gets information of which key pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(!ConnectButtonWasClicked)
            { 
            this.label8.Visible = true;
            if (e.KeyCode == Keys.Down)
            {
                TextToSend = "Down";
                    //20 ms delay for every key request
                Task.Delay(20).ContinueWith(t => backgroundWorker2.RunWorkerAsync());
            }
            if (e.KeyCode == Keys.Up)
            {
                TextToSend = "Up";
                Task.Delay(20).ContinueWith(t => backgroundWorker2.RunWorkerAsync());
            }
            if (e.KeyCode == Keys.Left)
            {
                TextToSend = "Left";
                Task.Delay(20).ContinueWith(t => backgroundWorker2.RunWorkerAsync());
            }
            if (e.KeyCode == Keys.Right)
            {
                TextToSend = "Right";
                Task.Delay(20).ContinueWith(t => backgroundWorker2.RunWorkerAsync());
            }
            }
        }

      
    }
}
