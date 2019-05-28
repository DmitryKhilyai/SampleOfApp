using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Authentication.JWS
{
    // Key for signature checking (public)
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
