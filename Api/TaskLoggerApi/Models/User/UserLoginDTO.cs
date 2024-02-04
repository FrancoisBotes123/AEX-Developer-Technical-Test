using System.ComponentModel.DataAnnotations;

namespace TaskLoggerApi.Models.User
{
    public class UserLoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
