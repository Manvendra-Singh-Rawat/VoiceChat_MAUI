using NAudio.Wave;
using System.Collections.Concurrent;

namespace MAUI_Client
{
    internal class AudioInputManager
    {
        public WaveInEvent AudioInput;

        public event Action<byte[]> AudioDataCaptured;

        public AudioInputManager()
        {
            AudioInput = new WaveInEvent()
            {
                WaveFormat = new WaveFormat(44100, 16, 1),
            };

            AudioInput.DataAvailable += (s, a) =>
            {
                if (a.BytesRecorded > 0)
                    AudioDataCaptured?.Invoke(a.Buffer.Take(a.BytesRecorded).ToArray());
            };

            AudioInput.StartRecording();

            AudioInput.RecordingStopped += (s, a) =>
            {
                System.Diagnostics.Debug.WriteLine("Recording stopped: " + a.Exception?.Message);
            };
        }
    }
}
