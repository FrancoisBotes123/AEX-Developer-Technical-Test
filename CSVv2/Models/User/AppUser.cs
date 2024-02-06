namespace CSVv2.Models.User
{
    public class AppUser 
    {
        public string UserName { get; set; }
        public string  Token { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActvie { get; set; } = DateTime.UtcNow;
        public ICollection<UserRole> Roles { get; set; }

    }
}
