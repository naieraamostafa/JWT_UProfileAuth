using JWT_Auth.Models;
using JWT_Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Auth.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("upload-profile-picture/{userId}")]
        public async Task<IActionResult> UploadProfilePicture(string userId, IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                try
                {
                    var result = await _profileService.AddProfilePictureAsync(userId, profilePicture);

                    if (result)
                    {
                        return Ok("Profile picture uploaded successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to upload profile picture");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return BadRequest("No file uploaded");
        }

        [HttpPost("add-job-description/{userId}")]
        public async Task<IActionResult> AddJobDescription(string userId, [FromBody] JobDescriptionDto jobDescriptionDto)
        {

            var result = await _profileService.AddJobDescriptionAsync(userId, jobDescriptionDto.JobDescription);

            if (result)
            {
                return Ok("Job description added successfully");
            }
            else
            {
                return BadRequest("Failed to add job description");
            }
        }
        [HttpPost("add-address/{userId}")]
        public async Task<IActionResult> AddAddress(string userId, [FromBody] AddressDto addressDto)
        {
            var result = await _profileService.AddAddressAsync(userId, addressDto.Address);

            if (result)
            {
                return Ok("Address added successfully");
            }
            else
            {
                return BadRequest("Failed to add address");
            }
        }

        [HttpPost("add-courses/{userId}")]
        public async Task<IActionResult> AddCourses(string userId, [FromBody] List<CourseDto> courses)
        {
            var result = await _profileService.AddCoursesAsync(userId, courses);

            if (result)
            {
                return Ok("Courses added successfully");
            }
            else
            {
                return BadRequest("Failed to add courses");
            }
        }

        [HttpPost("add-projects/{userId}")]
        public async Task<IActionResult> AddProjects(string userId, [FromBody] List<ProjectDto> projects)
        {
            var result = await _profileService.AddProjectsAsync(userId, projects);

            if (result)
            {
                return Ok("Projects added successfully");
            }
            else
            {
                return BadRequest("Failed to add projects");
            }
        }

        [HttpPost("add-skills/{userId}")]
        public async Task<IActionResult> AddSkills(string userId, [FromBody] List<SkillDto> skills)
        {
            var result = await _profileService.AddSkillsAsync(userId, skills);

            if (result)
            {
                return Ok("Skills added successfully");
            }
            else
            {
                return BadRequest("Failed to add skills");
            }
        }

        [HttpPut("update-job-description/{userId}")]
        public async Task<IActionResult> UpdateJobDescription(string userId, [FromBody] JobDescriptionDto jobDescriptionDto)
        {
            var result = await _profileService.UpdateJobDescriptionAsync(userId, jobDescriptionDto.JobDescription);

            if (result)
            {
                return Ok("Job description updated successfully");
            }
            else
            {
                return BadRequest("Failed to update job description");
            }
        }

        [HttpPut("update-address/{userId}")]
        public async Task<IActionResult> UpdateAddress(string userId, [FromBody] AddressDto addressDto)
        {
            var result = await _profileService.UpdateAddressAsync(userId, addressDto.Address);

            if (result)
            {
                return Ok("Address updated successfully");
            }
            else
            {
                return BadRequest("Failed to update address");
            }
        }

        [HttpPut("update-course/{userId}/{courseId}")]
        public async Task<IActionResult> UpdateCourse(string userId, int courseId, [FromBody] CourseDto updatedCourse)
        {
            var result = await _profileService.UpdateCourseAsync(userId, courseId, updatedCourse);

            if (result)
            {
                return Ok("Course updated successfully");
            }
            else
            {
                return BadRequest("Failed to update course");
            }
        }


        [HttpPut("update-project/{userId}/{projectId}")]
        public async Task<IActionResult> UpdateProject(string userId, int projectId, [FromBody] ProjectDto updatedProject)
        {
            var result = await _profileService.UpdateProjectAsync(userId, projectId, updatedProject);

            if (result)
            {
                return Ok("Project updated successfully");
            }
            else
            {
                return BadRequest("Failed to update project");
            }
        }


        [HttpPut("update-skill/{userId}/{skillId}")]
        public async Task<IActionResult> UpdateSkill(string userId, int skillId, [FromBody] SkillDto updatedSkill)
        {
            var result = await _profileService.UpdateSkillAsync(userId, skillId, updatedSkill);

            if (result)
            {
                return Ok("Skill updated successfully");
            }
            else
            {
                return BadRequest("Failed to update skill");
            }
        }


        [HttpGet("get-profile/{userId}")]
        public async Task<IActionResult> GetProfile(string userId)
        {
            var profile = await _profileService.GetProfileAsync(userId);

            if (profile != null)
            {
                return Ok(profile);
            }
            else
            {
                return NotFound("Profile not found");
            }
        }

        [HttpDelete("delete-course/{userId}/{courseId}")]
        public async Task<IActionResult> DeleteCourse(string userId, int courseId)
        {
            var result = await _profileService.DeleteCourseAsync(userId, courseId);

            if (result)
            {
                return Ok("Course deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete course");
            }
        }

        [HttpDelete("delete-skill/{userId}/{skillId}")]
        public async Task<IActionResult> DeleteSkill(string userId, int skillId)
        {
            var result = await _profileService.DeleteSkillAsync(userId, skillId);

            if (result)
            {
                return Ok("Skill deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete skill");
            }
        }

        [HttpDelete("delete-project/{userId}/{projectId}")]
        public async Task<IActionResult> DeleteProject(string userId, int projectId)
        {
            var result = await _profileService.DeleteProjectAsync(userId, projectId);

            if (result)
            {
                return Ok("Project deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete project");
            }
        }

    }
}

