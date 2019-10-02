using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace FacebookLivePoll.Relay
{
    class Broadcaster
    {
        private Process proc;
        private Image _image = null;
        private byte[] _imageStream = null;
        private Guid streamId;
        private bool streamEnded = false;
        public Guid StreamId => streamId;

        private static Broadcaster _instance;

        public static Broadcaster Instance => _instance ?? (_instance = new Broadcaster());


        private Broadcaster()
        {

        }

        public void UpdateImage(byte[] imageArray)
        {
            using (var stream = new MemoryStream(imageArray))
            {
                _image = Image.FromStream(stream);
            }
        }

        public void UpdateImageStream(byte[] imageArray)
        {
            _imageStream = imageArray;
        }
        public void EndStream()
        {
            streamEnded = true;
        }

        public void KillProcess()
        {
            if (!proc.HasExited)
            {
                proc.Kill();
            }
        }

        public void Start(Guid streamId, string fps, string rtmp, string preset, bool streamMode = true)
        {
            this.streamId = streamId;
            proc = new Process();
            proc.StartInfo.FileName = @"D:\temp\ffmpeg\bin\ffmpeg.exe";
            proc.StartInfo.Arguments = "" +
                                       "-f lavfi -i anullsrc=channel_layout=stereo:sample_rate=44100  " +
                                       "-loop 1 -f image2pipe -i pipe:.jpg " +
                                       "-vcodec libx264 -c:a aac -preset " + preset + " -r " + fps +
                                       " -pix_fmt yuvj420p  " +
                                       //"-f flv \"rtmp://rtmp-api.facebook.com:80/rtmp/1543161622392632?ds=1&s_l=1&a=ATjtzCH3VSLmbGUg\"";
                                       "-f flv \"" + rtmp + "\"";
            //"-y \"d:\\temp\\out.mp4\""; //+ rtmp;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = false;

            proc.Start();

            BinaryWriter writer = new BinaryWriter(proc.StandardInput.BaseStream);

            while (!streamEnded)
            {
                Thread.Sleep(10);
                if (_imageStream != null)
                    writer.Write(_imageStream);
            }
            proc.Kill();
        }
    }
}
