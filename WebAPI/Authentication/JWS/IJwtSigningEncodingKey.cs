using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Authentication
{
    // Key for signature creating (private)
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
