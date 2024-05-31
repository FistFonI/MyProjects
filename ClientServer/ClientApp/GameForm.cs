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
using System.Threading;
using System.IO;

namespace ClientApp
{
    public partial class GameForm : Form
    {
        int tcpPort = 27016;

        TcpClient tcp_client;
        byte[] _buffer = new byte[4096];
        bool isClosed = false;

        public GameForm(string ip)
        {
            InitializeComponent();

            this.Text = $"Сервер: {ip}";

            
            TimerImage.Start();

            try
            {
                tcp_client = new TcpClient();

                tcp_client.Connect(ip, tcpPort);

                tcp_client.GetStream().BeginRead(_buffer,
                                                0,
                                                _buffer.Length,
                                                Server_MessageReceived,
                                                null);
            }
            catch
            {
                MessageBox.Show("Сервер недоступен.");
                Close();
            }
        }
 
        private void Server_MessageReceived(IAsyncResult ar)
        {
            if (!isClosed)
            {
                try
                {
                    if (ar.IsCompleted)
                    {
                        var bytesIn = tcp_client.GetStream().EndRead(ar);
                        if (bytesIn > 0)
                        {
                            var tmp = new byte[bytesIn];
                            Array.Copy(_buffer, 0, tmp, 0, bytesIn);
                            var str = Encoding.UTF8.GetString(tmp);
                            BeginInvoke((Action)(() =>
                            {
                                ChatBox.Items.Add(str);
                                ChatBox.SelectedIndex = ChatBox.Items.Count - 1;
                            }));
                        }

                        Array.Clear(_buffer, 0, _buffer.Length);
                        if (!isClosed)
                            tcp_client.GetStream().BeginRead(_buffer,
                                                        0,
                                                        _buffer.Length,
                                                        Server_MessageReceived,
                                                        null);
                    }
                }
                catch
                {
                    isClosed = true;
                }
            }
        }


        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                var msg = Encoding.UTF8.GetBytes(ChatInput.Text);

                tcp_client.GetStream().Write(msg, 0, msg.Length);

                ChatInput.Text = "";
                ChatInput.Focus();
            }
            catch
            {
                MessageBox.Show("Сервер недоступен.");
                Close();
            }
        }

        private void chatInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendButton_Click(sender, e);
            }
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosed)
            {
                isClosed = true;
                tcp_client.GetStream().Close();
                tcp_client.Close();
            }
        }

        private void TimerImage_Tick(object sender, EventArgs e)
        {
            List<Bitmap> lisimage = GetImages();
            Random rnd = new Random();
            int rnd1 = rnd.Next(lisimage.Count);
            int rnd2 = rnd.Next(lisimage.Count);
            if (rnd1 != rnd2)
            {
                pictureBox1.Image = lisimage[rnd1];
                pictureBox2.Image = lisimage[rnd2];
            }
        }

        private List<Bitmap> GetImages()
        {
            List<Bitmap> lisimage = new List<Bitmap>();
            lisimage.Add(Properties.Resources._1);
            lisimage.Add(Properties.Resources._2);
            lisimage.Add(Properties.Resources._3);
            lisimage.Add(Properties.Resources._4);
            lisimage.Add(Properties.Resources._5);
            lisimage.Add(Properties.Resources._6);
            lisimage.Add(Properties.Resources._7);
            lisimage.Add(Properties.Resources._8);
            lisimage.Add(Properties.Resources._9);
            lisimage.Add(Properties.Resources._10);
            return lisimage;
        }
    }
}
