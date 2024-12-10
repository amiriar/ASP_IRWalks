using Microsoft.AspNetCore.Identity;

namespace ASP_CORE_API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
