using System.Threading.Tasks;
using Xpenser.Models;

namespace Xpenser.UI.Services
{
    public interface IAuthService
    {
        Task<AppUser> LoginAsync(SvcData user);
        Task<AppUser> RegisterUserAsync(SvcData user);
        Task<AppUser> GetUserByAccessTokenAsync(string accessToken);
        Task<AppUser> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}
