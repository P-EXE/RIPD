using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace RIPDApi.IntegrationTests;

public class TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
  protected override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    ClaimsIdentity identity = new([], "Test");
    ClaimsPrincipal principal = new(identity);
    AuthenticationTicket ticket = new(principal, "TestScheme");
    AuthenticateResult result = AuthenticateResult.Success(ticket);

    return Task.FromResult(result);
  }
}