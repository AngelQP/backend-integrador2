using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Comunications.Infrastructure.Identity
{
    public class ProtectedApiBearerTokenHandler : DelegatingHandler
    {

        private readonly IIdentityServerClient _identityServerClient;
        public ProtectedApiBearerTokenHandler(
           IIdentityServerClient identityServerClient)
        {
            _identityServerClient = identityServerClient
                                    ?? throw new ArgumentNullException(nameof(identityServerClient));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // request the access token
            var accessToken = await _identityServerClient.RequestClientCredentialsTokenAsync();

            // set the bearer token to the outgoing request
            request.SetBearerToken(accessToken);

            // Proceed calling the inner handler, that will actually send the request
            // to our protected api
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
