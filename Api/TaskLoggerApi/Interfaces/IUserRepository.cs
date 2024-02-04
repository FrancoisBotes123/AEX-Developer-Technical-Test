using TaskLoggerApi.Models.User;

namespace TaskLoggerApi.Interfaces
{
    public interface IUserRepository
    {
        void UpdateUser(AppUser user);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUserNameAsync(string username);


    }
}
