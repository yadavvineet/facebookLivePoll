using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FacebookLivePoll.Common.Infrastructure;

namespace FacebookLivePoll.Relay
{
    internal class CfService
    {
        public IRelayService GetServiceInstance()
        {
            EndpointAddress myEndpoint = new EndpointAddress("net.tcp://localhost:62238/RelayService");
            var binding = new NetTcpBinding();
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            ChannelFactory<IRelayService> myChannelFactory =
                new ChannelFactory<IRelayService>(binding, myEndpoint);
            return myChannelFactory.CreateChannel();
        }
    }
}
