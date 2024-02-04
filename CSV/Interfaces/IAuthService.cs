using CSV.Models.User;

namespace CSV.Interfaces
{
    public interface IAuthService
    {
        Task Login(UserLoginDto userLoginDTO);

        Task Logout();
    }
}
