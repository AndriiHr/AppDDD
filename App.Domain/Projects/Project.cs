using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.Feedbacks;
using App.Domain.SeedWork;
using App.Domain.Users;

namespace App.Domain.Projects
{
    public class Project : Entity, IAggregateRoot
    {
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public string Description { get; protected set; }
        public string Name { get; protected set; }
        public string UsedTechnologies { get; protected set; }


        private List<User> _users;
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();

        private List<Feedback> _feedbacks;
        public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks.AsReadOnly();

        public Project()
        {
            _users = new List<User>();
            _feedbacks = new List<Feedback>();
        }

        public Project(string name, DateTime startDate, DateTime endDate, string description)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            _users = new List<User>();
            _feedbacks = new List<Feedback>();

            this.AddDomainEvent(new ProjectCreatedEvent(name));
        }

        public void AddTechnology(string technology)
        {
            //_usedTechnologies.Add(technology);
        }

        public void RemoveTechnology(string technology)
        {
            //_usedTechnologies.Remove(technology);
        }

        public void AssignUserToProject(User user)
        {
            _users.Add(user);
            this.AddDomainEvent(new AssignUserEvent(user.Id, Id));
        }

        public void UnassignUserFromProject(User user)
        {
            _users.Remove(user);
            this.AddDomainEvent(new UnassignUserEvent(user.Id, Id));
        }

        public void AddFeedbackToProject(string feedback)
        {
            _feedbacks.Add(new Feedback(feedback));
            this.AddDomainEvent(new AddFeedbackEvent(feedback));
        }

        public void RemoveFeedbackFromProject(string feedback)
        {
            var result = _feedbacks.Where(x => x.Text == feedback).FirstOrDefault();
            if (result != null)
            {
                _feedbacks.Remove(result);
                this.AddDomainEvent(new RemoveFedbackEvent(result.Id));
            }
        }
    }
}