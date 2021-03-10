using App.Domain.Enums;

namespace App.Application.Users.Commands
{
    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
    }
}