using App.Application.Configuration.Commands;

namespace App.Application.Users.Commands
{
    public class UpdateUserProfileImageCommand : CommandBase
    {
        public int Id { get; }
        public string ImageProfile { get; }

        public UpdateUserProfileImageCommand(int id, string imageProfile)
        {
            Id = id;
            ImageProfile = imageProfile;
        }
    }
}
