using App.Domain.Projects;
using App.Domain.SeedWork;

namespace App.Domain.Feedbacks
{
    public class Feedback : Entity, IAggregateRoot
    {
        public string Text { get; }

        public Project Project { get; }

        public Feedback()
        {
            
        }

        public Feedback(string text)
        {
            Text = text;
        }
    }
}