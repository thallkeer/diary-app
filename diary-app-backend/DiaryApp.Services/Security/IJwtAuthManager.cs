using System.Collections.Generic;
using System.Security.Claims;

namespace DiaryApp.Services.Security
{
    public interface IJwtAuthManager
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}