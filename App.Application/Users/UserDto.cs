using App.Application.Projects;
using App.Domain.Enums;
using System.Collections.Generic;

namespace App.Application.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string ImageProfile { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}