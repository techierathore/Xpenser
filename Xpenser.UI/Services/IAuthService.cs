using System.Threading.Tasks;
using Xpenser.Models;

namespace Xpenser.UI.Services
{
    public interface IAuthService
    {
        Task<AppUser> LoginAsync(SvcData user);
        Task<bool> RegisterUserAsync(SvcData user);
        Task<AppUser> GetUserByAccessTokenAsync(string accessToken);
        Task<AppUser> RefreshTokenAsync(RefreshRequest refreshRequest);

        Task<bool> SendPasswordResetEmailAsync(SvcData user);
        Task<bool> ResetPasswordAsync(SvcData user);
        Task<AppUser> VerifyEmailAsync(SvcData aVerifyEmailData);

        Task<bool> ResendVerifiEmailAsync(SvcData aVerifiEmailData);
        Task<bool> UpdateNSendVerifiEmailAsync(SvcData aVerifiEmailData);
    }
}
