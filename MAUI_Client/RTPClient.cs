using System.Net.Sockets;
using System.Net;
using SIPSorcery.Net;
using NAudio.Wave;
using System.Text;

namespace MAUI_Client
{
    internal class RTPClient
    {
        private UdpClient _UdpClient;
        private IPEndPoint _IPEndPoint;

        private AudioInputManager _AudioManager;
        private AudioOutputManager _AudioOutputManager;

        private ushort SequenceNumber = 0;
        private uint TimeStamp = 0;
        private const int SampleRate = (int)(44100 * 0.020);
        private const int PayloadType = 11;
        private int UniqueID = new Random().Next();

        public RTPClient(string IP, int Port, AudioInputManager audioInputManager, AudioOutputManager audioOutputManager)
        {
            _UdpClient = new UdpClient();
            _UdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0));
            _IPEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);

            _AudioManager = audioInputManager;
            _AudioManager.AudioDataCaptured += data => SendData(data);

            _AudioOutputManager = audioOutputManager;

            Task.Run(() =>
            {
                ReceiveData();
            });
        }

        public void ConnectToServer()
        {
            _UdpClient.Connect(_IPEndPoint);
        }

        public void SendData(byte[] data)
        {
            RTPPacket RTP_Packet = new RTPPacket();
            RTP_Packet.Payload = data;
            
            RTPHeader RTP_Header = new RTPHeader()
            {
                SyncSource = (uint)UniqueID,
                Timestamp = TimeStamp += SampleRate,
                PayloadType = PayloadType,
                SequenceNumber = SequenceNumber++,
            };
            RTP_Packet.Header = RTP_Header;

            _UdpClient.SendAsync(RTP_Packet.GetBytes(), RTP_Packet.GetBytes().Length, _IPEndPoint);
        }

        public async void ReceiveData()
        {
            while (true)
            {
                try
                {
                    UdpReceiveResult ReceivedData = await _UdpClient.ReceiveAsync();
                    _AudioOutputManager.PlayAudioOnClient(ReceivedData.Buffer.Skip(12).ToArray());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error message: {ex.Message}");
                }
            }
        }
    }
}
