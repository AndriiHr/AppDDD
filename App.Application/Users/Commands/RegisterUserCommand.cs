using App.Application.Configuration.Commands;

namespace App.Application.Users.Commands
{
    public class RegisterUserCommand : CommandBase
    {
        public UserDto User { get; }

        public RegisterUserCommand(UserDto user)
        {
            User = user;
        }
    }
}