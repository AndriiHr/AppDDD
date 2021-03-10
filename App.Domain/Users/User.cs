using System.Collections.Generic;
using App.Domain.Enums;
using App.Domain.Projects;
using App.Domain.SeedWork;
using App.Domain.Users.Rules;

namespace App.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public UserRole UserRole { get; private set; }
        public string ImageProfile { get; set; }

        private List<Project> _projects;
        public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();

        public User()
        {
            _projects = new List<Project>();
        }

        public User(string email, string firstName, string lastName, UserRole role)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            UserRole = role;

            _projects = new List<Project>();
            this.AddDomainEvent(new UserCreatedEvent(email));
        }

        public void SetUserRole(UserRole userRole)
        {
            UserRole = userRole;
        }

        public void AssignProjectToUser(Project project)
        {
            _projects.Add(project);
            this.AddDomainEvent(new AssignProjectEvent(Id, project.Id));
        }

        public void UnassignProjectFromUser(Project project)
        {
            _projects.Remove(project);
            this.AddDomainEvent(new UnassignProjectEvent(Id, project.Id));
        }
    }
}