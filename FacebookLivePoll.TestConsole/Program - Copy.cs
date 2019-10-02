using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FacebookLivePoll.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Process pipeClient = new Process();

            pipeClient.StartInfo.FileName = @"D:\temp\ffmpeg\bin\ffmpeg.exe";
            pipeClient.StartInfo.Arguments =
                @" -y -loglevel warning -loop 1 -f image2pipe -i pipe:.jpg -f lavfi -i anullsrc=channel_layout=stereo:sample_rate=44100  -c:v libx264 -t 3000 -r 30 -pix_fmt yuvj420p  -g 60 -c:a aac d:\temp\out.mp4";

            using (AnonymousPipeServerStream pipeServer =
                new AnonymousPipeServerStream(PipeDirection.Out,
                HandleInheritability.Inheritable))
            {
                // Show that anonymous pipes do not support Message mode.
                try
                {
                    Console.WriteLine("[SERVER] Setting ReadMode to \"Message\".");
                    pipeServer.ReadMode = PipeTransmissionMode.Message;
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine("[SERVER] Exception:\n    {0}", e.Message);
                }

                Console.WriteLine("[SERVER] Current TransmissionMode: {0}.",
                    pipeServer.TransmissionMode);

                // Pass the client process a handle to the server.
                //pipeClient.StartInfo.Arguments =
                //    pipeServer.GetClientHandleAsString();
                pipeClient.StartInfo.UseShellExecute = false;
                pipeClient.Start();
                pipeServer.DisposeLocalCopyOfClientHandle();

                try
                {
                    // Read user input and send that to the client process.
                    using (StreamWriter sw = new StreamWriter(pipeServer))
                    {
                        sw.AutoFlush = true;
                        // Send a 'sync message' and wait for client to receive it.
                        var a = File.ReadAllBytes(@"D:\temp\cover.jpg");
                        Stream stream = new MemoryStream(a);
                        sw.Write(stream);
                        pipeServer.WaitForPipeDrain();

                        // Send the console input to the client process.
                        //Console.Write("[SERVER] Enter text: ");
                        //sw.WriteLine(Console.ReadLine());
                    }
                }

                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    Console.WriteLine("[SERVER] Error: {0}", e.Message);
                }
            }
            Console.ReadLine();

            pipeClient.WaitForExit();
            pipeClient.Close();
            Console.WriteLine("[SERVER] Client quit. Server terminating.");
        }
    }

}

