using App.Application.Configuration.Commands;

namespace App.Application.Users.Commands
{
    public class UpdateUserCommand : CommandBase
    {
        public UserDto User { get; }

        public UpdateUserCommand(UserDto user)
        {
            User = user;
        }
    }
}
