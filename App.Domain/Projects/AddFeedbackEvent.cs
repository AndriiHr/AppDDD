using App.Domain.SeedWork;

namespace App.Domain.Projects
{
    public class AddFeedbackEvent : DomainEventBase
    {
        public string Feedback { get;}

        public AddFeedbackEvent(string feedback)
        {
            Feedback = feedback;
        }
    }
}