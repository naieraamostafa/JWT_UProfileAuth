using Microsoft.AspNetCore.Identity;
using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;

namespace JWT_Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public required string LastName { get; set; }

        public string? ProfilePictureUrl { get; set; }
        public string? JobDescription { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
        public virtual ICollection<Skill>? Skills { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
    }
}
