namespace App.Application.Feedbacks.Commands
{
    public class AssignFeedbackRequest
    {
        public FeedbackDto Feedback { get; set; }
        public int ProjectId { get; set; }
    }
}