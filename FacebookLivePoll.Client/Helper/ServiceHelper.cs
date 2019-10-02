using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using FacebookLivePoll.Client.Services;
using FacebookLivePoll.Common;
using FacebookLivePoll.Common.Infrastructure;

namespace FacebookLivePoll.Client.Helper
{
    public class ServiceHelper
    {
        private static ServiceHelper _instance;
        public static ServiceHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceHelper();
                }
                return _instance;
            }
        }

        readonly Uri baseAddress = new Uri("http://localhost:62236/RelayService");
        private ServiceHost host;
        public void CreateAndStartService()
        {
            host = new ServiceHost(typeof(RelayService));

#if DEBUG
            //host.AddServiceEndpoint(typeof(IRelayService), new BasicHttpBinding(), "");
            //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //smb.HttpGetEnabled = true;
            //smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            //host.Description.Behaviors.Add(smb);
#endif
            host.AddServiceEndpoint(typeof(IRelayService), new NetTcpBinding(), "net.tcp://localhost:62238/RelayService");
            host.Open();
        }
    }
}
