using CSV.Models.User;

namespace CSV.Models
{
    public class AppRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
