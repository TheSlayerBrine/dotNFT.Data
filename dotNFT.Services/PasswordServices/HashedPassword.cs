using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNFT.Services.PasswordServices
{
    public record HashedPassword(string Hash, string Salt);
}

