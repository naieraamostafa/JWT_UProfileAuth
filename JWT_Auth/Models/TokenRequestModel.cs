namespace JWT_Auth.Models
{
    public class TokenRequestModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
