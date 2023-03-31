using dotNFT.Data.Enums;

namespace dotNFT.Utilities
{
    public class NetorkConveter
    {
        public static string ConvertFromNetworkIdToNetworkName(Network networkId)
        {
            IDictionary<Network, string> networkNameDictionary = new Dictionary<Network, string>
            {
                { Network.Polygon, "Polygon" }
            };
            return networkNameDictionary[networkId];
        }
    }
}
