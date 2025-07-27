using NAudio.Wave;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

ConcurrentDictionary<IPEndPoint, int> ConnectedUsers;
ConnectedUsers = new ConcurrentDictionary<IPEndPoint, int>();

IPEndPoint _IPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
UdpClient _Listener = new UdpClient(_IPEndPoint);

//WaveFileWriter _WaveFileWriter = new WaveFileWriter("D://Projects/MAUI_Client/Test.wav", new WaveFormat(44100, 16, 1));

bool isServerRunning = true;

Console.CancelKeyPress += (sender, e) =>
{
    _Listener.Close();
    isServerRunning = false;
    //_WaveFileWriter?.Flush();
    //_WaveFileWriter?.Dispose();
};

AppDomain.CurrentDomain.ProcessExit += (s, e) =>
{
    //_WaveFileWriter?.Flush();
    //_WaveFileWriter?.Dispose();
};

//int Count = 0;
while (isServerRunning)
{
    try
    {
        UdpReceiveResult _UdpReceiveResult = await _Listener.ReceiveAsync();
        IPEndPoint ReceivedEndPoint = _UdpReceiveResult.RemoteEndPoint;

        await _Listener.SendAsync(_UdpReceiveResult.Buffer, _UdpReceiveResult.Buffer.Length, ReceivedEndPoint);

        //_WaveFileWriter.Write(_UdpReceiveResult.Buffer.Skip(12).ToArray());
        //Console.WriteLine($"Data received {Count++}");
        //System.Diagnostics.Debug.WriteLine("Data received");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }   
}