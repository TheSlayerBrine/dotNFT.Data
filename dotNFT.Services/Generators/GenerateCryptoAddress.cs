using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Services.Generators
{
    public class GenerateCryptoAddress
    {

        public static string GenerateFakeCryptoAddress()
        {
            // Generate a random byte array of 20 bytes
            byte[] addressBytes = new byte[20];
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(addressBytes);
            }

            // Convert the byte array to a hexadecimal string
            string addressHex = BitConverter.ToString(addressBytes).Replace("-", "");

            // Add the prefix "0x" to the string to make it a valid Ethereum address
            string address = "0x" + addressHex;

            return address;
        }


    }
}
