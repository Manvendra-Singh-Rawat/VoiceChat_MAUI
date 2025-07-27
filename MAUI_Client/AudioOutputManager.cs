using NAudio.Wave;
using System.Net.Sockets;

namespace MAUI_Client
{
    internal class AudioOutputManager
    {
        WaveOutEvent AudioOutput;
        BufferedWaveProvider WaveProvider;

        public AudioOutputManager()
        {
            AudioOutput = new WaveOutEvent();
            WaveProvider = new BufferedWaveProvider(new WaveFormat(44100, 16, 1));

            AudioOutput.Init(WaveProvider);
            AudioOutput.Play();
        }

        public void PlayAudioOnClient(byte[] ReceivedAudio)
        {
            if(ReceivedAudio.Length > 0)
            {
                WaveProvider.AddSamples(ReceivedAudio, 0, ReceivedAudio.Length);
                
            }
            
        }
    }
}
