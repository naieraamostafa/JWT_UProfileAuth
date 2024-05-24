using JWT_Auth.Models;

namespace JWT_Auth.Services
{
    public interface IProfileService
    {
        Task<bool> AddProfilePictureAsync(string userId, IFormFile profilePicture);
        Task<bool> AddJobDescriptionAsync(string userId, string jobDescription);
        Task<bool> AddAddressAsync(string userId, string address);
        Task<bool> AddCoursesAsync(string userId, List<CourseDto> courses);
        Task<bool> AddProjectsAsync(string userId, List<ProjectDto> projects);
        Task<bool> AddSkillsAsync(string userId, List<SkillDto> skills);
        Task<bool> UpdateJobDescriptionAsync(string userId, string jobDescription);
        Task<bool> UpdateAddressAsync(string userId, string address);
        Task<bool> UpdateCourseAsync(string userId, int courseId, CourseDto updatedCourse);
        Task<bool> UpdateProjectAsync(string userId, int projectId, ProjectDto updatedProject);
        Task<bool> UpdateSkillAsync(string userId, int skillId, SkillDto updatedSkill);
        Task<ProfileDto> GetProfileAsync(string userId);
        Task<bool> DeleteCourseAsync(string userId, int courseId);
        Task<bool> DeleteSkillAsync(string userId, int skillId);
        Task<bool> DeleteProjectAsync(string userId, int projectId);

    }
}
