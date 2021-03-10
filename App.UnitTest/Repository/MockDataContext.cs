using System.Collections.Generic;

namespace App.UnitTest.Repository
{
    public class MockDataContext
    {
        public MockDataContext()
        {
            Users.Add(new Domain.Users.User() {Id = 1});
            Users.Add(new Domain.Users.User() {Id = 2});
            Users.Add(new Domain.Users.User() {Id = 3});
        }

        public List<Domain.Users.User> Users = new List<Domain.Users.User>();
    }
}