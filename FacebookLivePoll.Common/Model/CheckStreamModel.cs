using System;
using System.Runtime.Serialization;

namespace FacebookLivePoll.Common.Model
{
    [DataContract]
    public class CheckStreamModel
    {
        [DataMember]
        public bool StreamAvailable { get; set; }
        [DataMember]
        public Guid StreamId { get; set; }
    }
}
