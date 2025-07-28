using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

ConcurrentDictionary<IPEndPoint, DateTime> ConnectedUsers;
ConnectedUsers = new ConcurrentDictionary<IPEndPoint, DateTime>();

IPEndPoint _IPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
UdpClient _Listener = new UdpClient(_IPEndPoint);

bool isServerRunning = true;

Console.CancelKeyPress += (sender, e) =>
{
    _Listener.Close();
    isServerRunning = false;
};

while (isServerRunning)
{
    try
    {
        UdpReceiveResult _UdpReceiveResult = await _Listener.ReceiveAsync();
        IPEndPoint ReceivedEndPoint = _UdpReceiveResult.RemoteEndPoint;
        ConnectedUsers[ReceivedEndPoint] = DateTime.Now;

        foreach(var User in ConnectedUsers)
        {
            await _Listener.SendAsync(_UdpReceiveResult.Buffer, _UdpReceiveResult.Buffer.Length, User.Key);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }   
}