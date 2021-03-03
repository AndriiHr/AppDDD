using App.Application.Configuration.Commands;

namespace App.Application.Users.Commands
{
    public class DeleteUserCommand : CommandBase
    {
        public int Id { get; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
