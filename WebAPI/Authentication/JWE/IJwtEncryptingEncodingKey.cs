using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Authentication.JWE
{
    //Key for data encryption (public)
    public interface IJwtEncryptingEncodingKey
    {
        string SigningAlgorithm { get; }

        string EncryptingAlgorithm { get; }

        SecurityKey GetKey();
    }
}
