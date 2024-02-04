using System.Text.Json.Serialization;

namespace CSV.Models.User
{
    public class UserLoggedInDto
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("lastActvie")] // It seems there is a typo in your JSON, it should be "lastActive"
        public DateTime LastActive { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
