namespace JWT_Auth.Models
{
    public class ProfileDto
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? JobDescription { get; set; }
        public string? Address { get; set; }
        public List<CourseDto>? Courses { get; set; }
        public List<ProjectDto>? Projects { get; set; }
        public List<SkillDto>? Skills { get; set; }
    }
}
