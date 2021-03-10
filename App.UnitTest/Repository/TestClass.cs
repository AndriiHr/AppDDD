using App.Domain.Enums;
using App.Domain.SeedWork;

namespace App.UnitTest.Repository
{
    public class TestClass: Entity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole UserRole { get; set; }
    }
}