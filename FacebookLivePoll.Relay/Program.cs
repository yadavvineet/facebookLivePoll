using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FacebookLivePoll.Common;
using FacebookLivePoll.Common.Infrastructure;

namespace FacebookLivePoll.Relay
{
    class Program
    {
        #region DllImports

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        #region Constants
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        #endregion

        #region Fields
        static IntPtr handle;
        #endregion

        //var handle = GetConsoleWindow();
        //// Hide
        //ShowWindow(handle, SW_HIDE);

        //// Show
        //ShowWindow(handle, SW_SHOW);

        static bool isStreamEnded = false;
        private static CfService service;
        static void Main(string[] args)
        {
            handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            service = new CfService();
            Broadcaster broadcaster = Broadcaster.Instance;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();


            while (!isStreamEnded)
            {
                Thread.Sleep(100);
            }
            broadcaster.KillProcess();
            Environment.Exit(0);
        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var streamAvailablity = service.GetServiceInstance().CheckStream();
            if (!streamAvailablity.StreamAvailable)
            {
                isStreamEnded = true;
                return;
            }
            Guid availableStreamId = streamAvailablity.StreamId;
            var stream = service.GetServiceInstance().GetStream(availableStreamId);
            Broadcaster.Instance.UpdateImageStream(stream.ContentStream);

            BackgroundWorker streamWorker = new BackgroundWorker();
            streamWorker.DoWork += StreamWorker_DoWork;
            streamWorker.RunWorkerAsync();

            Broadcaster.Instance.Start(streamAvailablity.StreamId, "30", stream.StreamAddress, "veryfast", true);
        }

        private static void StreamWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var stream = service.GetServiceInstance().GetStream(Broadcaster.Instance.StreamId);
                Broadcaster.Instance.UpdateImageStream(stream.ContentStream);

                var streamCompleted = stream.StreamStatus == StreamStatus.Ended || stream.StreamStatus == StreamStatus.None;
                if (streamCompleted)
                    break;
                Thread.Sleep(50);
            }
        }
    }
}
