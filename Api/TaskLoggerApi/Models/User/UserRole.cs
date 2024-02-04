
using Microsoft.AspNetCore.Identity;

namespace TaskLoggerApi.Models.User
{
    public class UserRole: IdentityUserRole<int>
    {
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
    }
}
