using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Threading;
using DotNetOpenAuth.OAuth2;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace CellPhoneService
{
    public class OauthAuthorize : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly ResourceServerConfiguration _configuration;
        public OauthAuthorize()
        {
            _configuration = WebApiApplication.config;
        }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            HttpRequestMessage request = actionContext.Request;
            HttpContextBase httpContext;
            string userName;
            HashSet<string> scope;

            if (!request.TryGetHttpContext(out httpContext))
                throw new InvalidOperationException("HttpContext must not be null.");

            var resourceServer = new ResourceServer(new StandardAccessTokenAnalyzer(
                                                        (RSACryptoServiceProvider)_configuration.IssuerSigningCertificate.PublicKey.Key,
                                                        (RSACryptoServiceProvider)_configuration.EncryptionVerificationCertificate.PrivateKey));

            var error = resourceServer.VerifyAccess(httpContext.Request, out userName, out scope);

            if (error != null)
                return Task<HttpResponseMessage>.Factory.StartNew(error.ToHttpResponseMessage);

            //var identity = new ClaimsIdentity(scope.Select(s => new Claim(s, s)));
            //if (!string.IsNullOrEmpty(userName))
            //    identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            //httpContext.User = new GenericPrincipal(identity, null);
            //Thread.CurrentPrincipal = httpContext.User;

            return continuation();
        }

        public override bool AllowMultiple
        {
            get
            {
                return true;
            }
        }
    }
}