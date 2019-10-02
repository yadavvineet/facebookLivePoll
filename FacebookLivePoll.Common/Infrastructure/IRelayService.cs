using System;
using System.ServiceModel;
using FacebookLivePoll.Common.Model;

namespace FacebookLivePoll.Common.Infrastructure
{
    [ServiceContract]
    public interface IRelayService
    {
        [OperationContract]
        CheckStreamModel CheckStream();

        [OperationContract]
        CStream GetStream(Guid streamId);
    }
}
