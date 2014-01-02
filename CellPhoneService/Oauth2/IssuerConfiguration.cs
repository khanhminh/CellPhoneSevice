using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace CellPhoneService
{
    public class IssuerConfiguration
    {
        public IssuerConfiguration()
        {
            TokenLifetime = TimeSpan.FromDays(10);
        }
        public X509Certificate2 SigningCertificate { get; set; }
        public X509Certificate2 EncryptionCertificate { get; set; }

        public TimeSpan TokenLifetime { get; set; }
    }
}