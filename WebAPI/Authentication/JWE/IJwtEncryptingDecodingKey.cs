using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Authentication.JWE
{
    //Key for data decryption (private)
    public interface IJwtEncryptingDecodingKey
    {
        SecurityKey GetKey();
    }
}
