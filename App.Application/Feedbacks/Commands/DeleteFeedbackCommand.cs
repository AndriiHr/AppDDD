using App.Application.Configuration.Commands;

namespace App.Application.Feedbacks.Commands
{
    public class DeleteFeedbackCommand : CommandBase
    {
        public int Id { get; }

        public DeleteFeedbackCommand(int id)
        {
            Id = id;
        }
    }
}