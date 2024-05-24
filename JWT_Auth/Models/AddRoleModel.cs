using System.ComponentModel.DataAnnotations;

namespace JWT_Auth.Models
{
    public class AddRoleModel
    {
        public required string UserId { get; set; }
        public required string Role { get; set; }
    }
}
