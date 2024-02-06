using CSVv2.Models.User;

namespace CSVv2.Models
{
    public class AppRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
