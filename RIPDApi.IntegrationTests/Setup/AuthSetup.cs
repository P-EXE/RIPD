using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace RIPDApi.IntegrationTests;

public static class AuthSetup
{
  public static void SetupAuth(this IServiceCollection services)
  {
    services.AddAuthentication()
      .AddBearerToken();

    services.AddAuthentication(Defaults.AUTH_SCHEME)
      .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(Defaults.AUTH_SCHEME, null);

    services.AddAuthorization();
  }
}

public class TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
  protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    IEnumerable<Claim> claims =  [];
    ClaimsIdentity identity = new(claims, Defaults.AUTH_SCHEME);
    ClaimsPrincipal principal = new(identity);
    AuthenticationTicket ticket = new(principal, Defaults.AUTH_SCHEME);
    AuthenticateResult result = AuthenticateResult.Success(ticket);

    return result;
  }
}