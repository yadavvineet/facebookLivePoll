using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FacebookLivePoll.Common.Contracts
{
    [DataContract]
    class CommonContract
    {
        [DataMember]
        public string PublishKey { get; set; }

        [DataMember]
        public Guid StreamId { get; set; }

        [DataMember]
        public byte[] Content { get; set; }


    }
}
