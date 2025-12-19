using SigortaApp.Models;

namespace SigortaApp.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}