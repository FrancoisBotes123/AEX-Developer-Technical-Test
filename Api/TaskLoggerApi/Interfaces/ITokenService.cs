using TaskLoggerApi.Models.User;

namespace TaskLoggerApi.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
