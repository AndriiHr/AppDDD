using App.Application.Configuration.Commands;

namespace App.Application.Feedbacks.Commands
{
    public class AssignFeedbackToProjectCommand : CommandBase
    {
        public FeedbackDto Feedback { get; }
        public int ProjectId { get; }

        public AssignFeedbackToProjectCommand(FeedbackDto feedback, int projectId)
        {
            Feedback = feedback;
            ProjectId = projectId;
        }
    }
}