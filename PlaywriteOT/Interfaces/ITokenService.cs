using PlaywriteOT.Models;

namespace PlaywriteOT.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, UserVM user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}