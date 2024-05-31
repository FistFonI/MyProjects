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

namespace ClientApp
{
    public partial class ServersListForm : Form
    {
        int udpPort = 27015;
        int tcpPort = 27016;
        Dictionary<IPEndPoint, int> clients = new Dictionary<IPEndPoint, int>();
        TcpClient tcp_client;
        byte[] _buffer = new byte[4096];

        public ServersListForm()
        {
            InitializeComponent();
        }

        public Dictionary<IPEndPoint, int> FindUdpClients()
        {
            Dictionary<IPEndPoint, int> dictionaryResult = new Dictionary<IPEndPoint, int>();

            using (Socket udpsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                udpsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontRoute, true);
                udpsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                udpsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                udpsocket.ReceiveTimeout = 1000;

                string testMessage = "9game";
                udpsocket.SendTo(Encoding.UTF8.GetBytes(testMessage), new IPEndPoint(IPAddress.Broadcast, udpPort));

                byte[] buffer = new byte[udpsocket.ReceiveBufferSize];
                EndPoint remEp = new IPEndPoint(IPAddress.Any, 0);
                int count = 0;
                while (true)
                {
                    try
                    {
                        count = udpsocket.ReceiveFrom(buffer, ref remEp);
                        byte[] msg = buffer.Clone() as byte[];
                        Array.Resize(ref msg, count);
                        dictionaryResult.Add(remEp as IPEndPoint, int.Parse(Encoding.UTF8.GetString(msg)));
                    }
                    catch
                    {
                        break;
                    }
                }

                udpsocket.Close();
            }

            return dictionaryResult;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            clients = FindUdpClients();
            ServersGrid.Rows.Clear();
            foreach (var id in clients.Keys)
                ServersGrid.Rows.Add(id.Address.ToString(), clients[id]);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            var selectedServer = ServersGrid.SelectedRows;
            if (selectedServer == null)
                MessageBox.Show($"Нужно выделить сервер, к которыму вы хотите подключиться.");
            else
            {
                InitializeChatWindow(selectedServer[0].Cells[0].Value.ToString());
            }
        }

        private void InitializeChatWindow(string serverip)
        {
            GameForm chatWindow = new GameForm(serverip);

            try
            {
                chatWindow.Owner = this;
                chatWindow.Show();
                chatWindow.Left = Left + Width + 20;
                chatWindow.Top = Top;
            }
            catch
            {

            }

        }

        private void ServersListForm_Activated(object sender, EventArgs e)
        {
            clients = FindUdpClients();
            ServersGrid.Rows.Clear();
            foreach (var id in clients.Keys)
                ServersGrid.Rows.Add(id.Address.ToString(), clients[id]);
        }
    }
}
