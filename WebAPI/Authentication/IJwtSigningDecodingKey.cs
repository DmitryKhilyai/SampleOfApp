using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Authentication
{
    // Key for signature checking (public)
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
