using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Authentication.JWS
{
    // Key for signature creating (private)
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
