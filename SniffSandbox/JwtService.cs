using System.Security.Claims;

namespace SniffSandbox;

public class JwtService
{
    public async Task<JwtToken> GetToken(IList<Claim> claims, CancellationToken cancellationToken)
    {
        return new JwtToken("", "");
    }
}