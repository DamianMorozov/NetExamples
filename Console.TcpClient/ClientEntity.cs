using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientApp
{
    public class ClientEntity
    {
        #region Public and private fields and properties

        public bool IsWork { get; set; }
        public short BufferSize { get; set; }

        #endregion

        #region Constructor and destructor

        public ClientEntity()
        {
            IsWork = true;
            BufferSize = 256;
        }

        #endregion

        #region Public and private methods

        public void Work(TcpClient tcpClient)
        {
            if (tcpClient == null)
                return;
            
            Task.Run(async () =>
            {
                await WorkAsync(tcpClient).ConfigureAwait(false);
            });
        }

        public async Task WorkAsync(TcpClient tcpClient)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            try
            {
                await using var netStream = tcpClient.GetStream();
                
                GetData(netStream);
                SendData(netStream);

                netStream.Close();
                tcpClient.Close();
            }
            finally
            {
                tcpClient?.Dispose();
                Console.WriteLine("--------------------------------------------------");
            }
        }

        public void GetData(NetworkStream netStream)
        {
            var data = new byte[BufferSize];
            var messageBuilder = new StringBuilder();
            do
            {
                var bytesCount = netStream.Read(data, 0, data.Length);
                messageBuilder.Append(Encoding.UTF8.GetString(data, 0, bytesCount));
            } while (netStream.DataAvailable);
            Console.WriteLine("Get data: ");
            Console.WriteLine($"{messageBuilder.ToString().TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t')}");
        }

        public void SendData(NetworkStream netStream)
        {
            var response = "Response";
            var data = Encoding.UTF8.GetBytes(response);

            netStream. Write(data, 0, data.Length);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Send data: ");
            Console.WriteLine($"{response}");
        }

        #endregion
    }
}