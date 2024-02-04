using System.ComponentModel.DataAnnotations;

namespace TaskLoggerApi.Models.User
{
    public class RegisterUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8, MinimumLength =4)]
        public string Password { get; set; }
    }
}
