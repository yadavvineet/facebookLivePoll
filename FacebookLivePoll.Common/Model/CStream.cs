using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using FacebookLivePoll.Common.Infrastructure;

namespace FacebookLivePoll.Common.Model
{
    [DataContract]
    public class CStream
    {
        [DataMember]
        public StreamStatus StreamStatus { get; set; }

        [DataMember]
        public byte[] ContentStream { get; set; }

        [DataMember]
        public string StreamAddress { get; set; }
    }
}
