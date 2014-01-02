using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace CellPhoneService
{
    public class OAuth2Issuer : IAuthorizationServer
    {
        private readonly IssuerConfiguration _configuration;

        public OAuth2Issuer(IssuerConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            _configuration = configuration;
        }

        public RSACryptoServiceProvider AccessTokenSigningKey
        {
            get
            {
                return (RSACryptoServiceProvider)_configuration.SigningCertificate.PrivateKey;
            }
        }

        public DotNetOpenAuth.Messaging.Bindings.ICryptoKeyStore CryptoKeyStore
        {
            get { throw new NotImplementedException(); }
        }

        public TimeSpan GetAccessTokenLifetime(DotNetOpenAuth.OAuth2.Messages.IAccessTokenRequest accessTokenRequestMessage)
        {
            return _configuration.TokenLifetime;
        }

        public IClientDescription GetClient(string clientIdentifier)
        {
            ApplicationDAO data = new ApplicationDAO();
            Application app = data.GetAppSecret(clientIdentifier);

            return new ClientDescription(app.AppSecret, new Uri(app.Link), ClientType.Confidential);
        }

        public RSACryptoServiceProvider GetResourceServerEncryptionKey(DotNetOpenAuth.OAuth2.Messages.IAccessTokenRequest accessTokenRequestMessage)
        {
            return (RSACryptoServiceProvider)_configuration.EncryptionCertificate.PublicKey.Key;

        }

        public bool IsAuthorizationValid(DotNetOpenAuth.OAuth2.ChannelElements.IAuthorizationDescription authorization)
        {
            return true;
        }

        public bool IsResourceOwnerCredentialValid(string userName, string password)
        {
            return true;
        }

        public DotNetOpenAuth.Messaging.Bindings.INonceStore VerificationCodeNonceStore
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}