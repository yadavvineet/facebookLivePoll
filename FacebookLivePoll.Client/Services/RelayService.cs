using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FacebookLivePoll.Common;
using FacebookLivePoll.Common.Infrastructure;
using FacebookLivePoll.Common.Model;
using CStream = FacebookLivePoll.Common.Model.CStream;

namespace FacebookLivePoll.Client.Services
{
    public class RelayService : IRelayService
    {
        public CheckStreamModel CheckStream()
        {
            return new CheckStreamModel() {StreamAvailable = true, StreamId = Guid.NewGuid()};
        }

        public CStream GetStream(Guid streamId)
        {
            var file = Directory.GetFiles(@"D:\vineet\pics\slide", "*.jpg");
            Random rd = new Random();
            var selFile = file[rd.Next(0, file.Count() - 1)];

            var image = Image.FromFile(selFile);
            var bMap = new Bitmap(image, 1024, 720);
            var mStream = new MemoryStream();
            bMap.Save(mStream, ImageFormat.Jpeg);
            return new CStream() {StreamStatus = StreamStatus.Started, ContentStream = mStream.ToArray(), StreamAddress = "rtmp://rtmp-api.facebook.com:80/rtmp/1549996645042463?ds=1&s_l=1&a=ATimzy4pTZFAz2P8" };
        }
    }
}
