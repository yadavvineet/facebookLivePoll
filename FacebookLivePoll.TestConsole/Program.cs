using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
namespace FacebookLivePoll.TestConsole
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;


        //        var handle = GetConsoleWindow();
        //// Hide
        //ShowWindow(handle, SW_HIDE);

        //// Show
        //ShowWindow(handle, SW_SHOW);

        static void Main(string[] args)
        {
            proc = new Process();
            var handle = GetConsoleWindow();
            Console.ReadLine();
            //ShowWindow(handle, SW_HIDE);
            Start("30", "", "", "veryfast");
            var procc = Process.GetProcessesByName("ffmpeg");
            if (procc.Any())
            {
                foreach (var process in procc)
                {
                    process.Kill();
                }
            }
            //ShowWindow(handle, SW_SHOW);
            Console.ReadLine();
        }

        private static Process proc;
        public static void Start(string fps, string rtmp, string resolution, string preset)
        {
            proc.StartInfo.FileName = @"D:\temp\ffmpeg\bin\ffmpeg.exe";
            proc.StartInfo.Arguments = "" +
                                       "-f lavfi -i anullsrc=channel_layout=stereo:sample_rate=44100  " +
                                       "-loop 1 -f image2pipe -i pipe:.jpg " +
                                       "-vcodec libx264 -c:a aac -preset " + preset + " -r " + fps + " -pix_fmt yuvj420p  " +
            //"-f flv \"rtmp://rtmp-api.facebook.com:80/rtmp/1543161622392632?ds=1&s_l=1&a=ATjtzCH3VSLmbGUg\"";
            "-f flv \"rtmp://a.rtmp.youtube.com/live2/es7k-019e-y0c7-2q98\"";
            //"-y \"d:\\temp\\out.mp4\""; //+ rtmp;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = false;

            proc.Start();

            Stopwatch st = new Stopwatch();
            BinaryWriter writer = new BinaryWriter(proc.StandardInput.BaseStream);

            st.Reset();
            st.Start();

            var files = Directory.GetFiles("D:\\temp_pics\\familyZell", "*.jpg");
            Stopwatch stWatch = new Stopwatch();
            foreach (var file in files)
            {
                Image img = Image.FromFile(file);
                //var a = File.ReadAllBytes(file);
                Bitmap bMpap = new Bitmap(img, new Size(1024, 720));


                stWatch.Start();
                for (int i = 0; i < 30; i++)
                {
                    RectangleF rectf = new RectangleF(70, 90, 400, 50);

                    Graphics g = Graphics.FromImage(bMpap);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawString(DateTime.Now.ToString("HH:mm:ss"), new Font("Tahoma", 32), Brushes.Black, rectf);

                    g.Flush();

                    Thread.Sleep(10);
                    MemoryStream ms = new MemoryStream();
                    bMpap.Save(ms, ImageFormat.Bmp);
                    writer.Write(ms.ToArray());
                }

                stWatch.Stop();
                stWatch.Reset();
            }

            st.Stop();
        }
    }

}

