using FacebookLivePoll.Common.Infrastructure;

namespace FacebookLivePoll.Relay.Helpers
{
    public class ServiceHelper
    {
        public static string GetServiceUrl(ServicesEnum serviceType)
        {
            switch (serviceType)
            {
                case ServicesEnum.Facebook:
                    return "";
                    break;
                case ServicesEnum.Youtube:
                    return "";
                    break;
            }
                    return "";
        }
    }
}
