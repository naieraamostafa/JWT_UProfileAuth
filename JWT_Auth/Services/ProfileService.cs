using JWT_Auth.Models;
using Microsoft.AspNetCore.Identity;
using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using NuGet.Packaging;
using Microsoft.EntityFrameworkCore;

namespace JWT_Auth.Services
{
    public class ProfileService: IProfileService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProfileService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<bool> AddProfilePictureAsync(string userId, IFormFile profilePicture)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            {
                throw new ArgumentException("Profile picture is required.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} not found.");
            }

            if (string.IsNullOrEmpty(_hostingEnvironment.WebRootPath))
            {
                throw new Exception("WebRootPath is not configured.");
            }

            if (string.IsNullOrEmpty(profilePicture.FileName))
            {
                throw new Exception("Profile picture file name is invalid.");
            }

            string uploadDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "profile-pictures");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            try
            {
                string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(profilePicture.FileName)}";
                string filePath = Path.Combine(uploadDirectory, uniqueFileName);

                // Log the paths to ensure they are not null or empty
                Console.WriteLine($"WebRootPath: {_hostingEnvironment.WebRootPath}");
                Console.WriteLine($"Upload Directory: {uploadDirectory}");
                Console.WriteLine($"Unique File Name: {uniqueFileName}");
                Console.WriteLine($"File Path: {filePath}");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }

                // Construct the URL path
                string baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                string urlPath = $"{baseUrl}/profile-pictures/{uniqueFileName}";

                user.ProfilePictureUrl = urlPath;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to update user with profile picture URL.");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading profile picture: {ex.Message}");
            }
        }


        public async Task<bool> AddJobDescriptionAsync(string userId, string jobDescription)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.JobDescription = jobDescription;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> AddAddressAsync(string userId, string address)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.Address = address;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> AddCoursesAsync(string userId, List<CourseDto> courses)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            if (user.Courses == null)
            {
                user.Courses = new List<Course>();
            }
            user.Courses.AddRange(courses.Select(c => new Course { Title = c.Title, Description = c.Description, UserId = userId }));
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> AddProjectsAsync(string userId, List<ProjectDto> projects)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;
            if(user.Projects == null)
            {
                user.Projects = new List<Project>();
            }
            user.Projects.AddRange(projects.Select(p => new Project { Title = p.Title, Description = p.Description, UserId = userId }));
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> AddSkillsAsync(string userId, List<SkillDto> skills)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;
            if(user.Skills == null)
            {
                user.Skills = new List<Skill>();
            }
            user.Skills.AddRange(skills.Select(s => new Skill { Name = s.Name, Level = s.Level, UserId = userId }));
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateJobDescriptionAsync(string userId, string jobDescription)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.JobDescription = jobDescription;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateAddressAsync(string userId, string address)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.Address = address;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateCourseAsync(string userId, int courseId, CourseDto updatedCourse)
        {
            var user = await _userManager.Users
                            .Include(u => u.Courses) // Eagerly load the Courses collection
                            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            // Find the course by ID
            var courseToUpdate = user.Courses.FirstOrDefault(c => c.Id == courseId);
            if (courseToUpdate == null)
                return false;

            // Update the course properties
            courseToUpdate.Title = updatedCourse.Title;
            courseToUpdate.Description = updatedCourse.Description;
            // Update other properties as needed

            // Update the user
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateProjectAsync(string userId, int projectId, ProjectDto updatedProject)
        {
            var user = await _userManager.Users
                            .Include(u => u.Projects) // Eagerly load the Projects collection
                            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            // Find the project by ID
            var projectToUpdate = user.Projects.FirstOrDefault(p => p.Id == projectId);
            if (projectToUpdate == null)
                return false;

            // Update the project properties
            projectToUpdate.Title = updatedProject.Title;
            projectToUpdate.Description = updatedProject.Description;
            // Update other properties as needed

            // Update the user
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }


        public async Task<bool> UpdateSkillAsync(string userId, int skillId, SkillDto updatedSkill)
        {
            var user = await _userManager.Users
                .Include(u => u.Skills) // Eagerly load the Skills collection
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            // Find the skill by ID
            var skillToUpdate = user.Skills.FirstOrDefault(s => s.Id == skillId);
            if (skillToUpdate == null)
                return false;

            // Update the skill properties
            skillToUpdate.Name = updatedSkill.Name;
            skillToUpdate.Level = updatedSkill.Level;

            // Update the user
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }




        public async Task<ProfileDto> GetProfileAsync(string userId)
        {
            var user = await _userManager.Users
                            .Include(u => u.Courses)
                            .Include(u => u.Projects)
                            .Include(u => u.Skills)
                            .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            // Create the profile DTO
            var profileDto = new ProfileDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                JobDescription = user.JobDescription,
                Address = user.Address,
                Courses = user.Courses?.Select(c => new CourseDto { Title = c.Title, Description = c.Description }).ToList(),
                Projects = user.Projects?.Select(p => new ProjectDto { Title = p.Title, Description = p.Description }).ToList(),
                Skills = user.Skills?.Select(s => new SkillDto { Name = s.Name, Level = s.Level }).ToList()
            };

            return profileDto;
        }

        public async Task<bool> DeleteCourseAsync(string userId, int courseId)
        {
            var user = await _userManager.Users
                                .Include(u => u.Courses)
                                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            var courseToRemove = user.Courses.FirstOrDefault(c => c.Id == courseId);
            if (courseToRemove == null)
                return false;

            user.Courses.Remove(courseToRemove);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteSkillAsync(string userId, int skillId)
        {
            var user = await _userManager.Users
                                .Include(u => u.Skills)
                                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            var skillToRemove = user.Skills.FirstOrDefault(s => s.Id == skillId);
            if (skillToRemove == null)
                return false;

            user.Skills.Remove(skillToRemove);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteProjectAsync(string userId, int projectId)
        {
            var user = await _userManager.Users
                                .Include(u => u.Projects)
                                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            var projectToRemove = user.Projects.FirstOrDefault(p => p.Id == projectId);
            if (projectToRemove == null)
                return false;

            user.Projects.Remove(projectToRemove);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }





    }
}
