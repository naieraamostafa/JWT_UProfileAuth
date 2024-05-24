using System.ComponentModel.DataAnnotations;

namespace JWT_Auth.Models
{
    public class RegisterModel
    {

        [StringLength(100)]
        public required string FirstName { get; set; }

        [StringLength(100)]
        public required string LastName { get; set; }

        [StringLength(100)]
        public required string UserName { get; set; }

        [StringLength(128)]
        public required string Email { get; set; }

        [StringLength(256)]
        public required string Password { get; set; }

        
    }
}
