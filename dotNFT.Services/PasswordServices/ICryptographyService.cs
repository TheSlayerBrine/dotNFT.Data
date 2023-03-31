using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Services.PasswordServices
{
    public interface ICryptographyService
    {
        HashedPassword HashPasswordWithSaltGeneration(string password);
        HashedPassword HashPassword(string password, string salt);
    }
}
