using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;

namespace Reservea.Common.Helpers
{
    public class JwtTokenHelper
    {
        public static RsaSecurityKey BuildRsaSigningKey(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) throw new ArgumentNullException($"{nameof(xml)}", "Null or empty parameter");

            var rsaProvider = new RSACryptoServiceProvider(2048);
            rsaProvider.FromXmlString(xml);

            return new RsaSecurityKey(rsaProvider);
        }
    }
}
