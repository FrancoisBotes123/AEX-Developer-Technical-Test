using Microsoft.AspNetCore.Identity;
using TaskLoggerApi.Models.User;

namespace TaskLoggerApi.Models
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
