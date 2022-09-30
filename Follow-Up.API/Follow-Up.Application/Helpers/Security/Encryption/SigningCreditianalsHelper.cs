using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Helpers.Security.Encryption
{
    public class SigningCreditianalsHelper
    {
        public static SigningCredentials CreateSigningCreditianals(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
