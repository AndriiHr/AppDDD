using App.Domain.SeedWork;

namespace App.Domain.Projects
{
    public class RemoveFedbackEvent : DomainEventBase
    {
        public int FeedbackId { get; }

        public RemoveFedbackEvent(int feedbackId)
        {
            FeedbackId = feedbackId;
        }
    }
}