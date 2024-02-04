namespace CSV.Models.User
{
    public class UserReturnDTO
    {
        public string UserName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActvie { get; set; } = DateTime.UtcNow;
        public string Token { get; set; }
    }
}
