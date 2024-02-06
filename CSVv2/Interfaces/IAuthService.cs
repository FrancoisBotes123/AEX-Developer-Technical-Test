using CSVv2.Models.User;

namespace CSVv2.Interfaces
{
    public interface IAuthService
    {
        Task Login(UserLoginDto userLoginDTO);

        Task Logout();
    }
}
