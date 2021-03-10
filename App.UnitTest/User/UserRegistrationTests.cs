using System;
using System.Threading.Tasks;
using App.Domain.Enums;
using App.Domain.Users.Rules;
using Moq;
using Xunit;

namespace App.UnitTest.User
{
    public class UserRegistrationTests
    {
        [Fact]
        public async Task UserIsUnique_WhenRegistering()
        {
            var mock = new Mock<IUserUniquenessChecker>();
            var user = new Domain.Users.User("testgmail.com", "Name", "Surname", UserRole.Developer);

            //Arrange
            mock.Setup(x => x.IsUnique(user.Email)).ReturnsAsync(true);

            //Act
            var result = user.CheckRule(new UserEmailUniqueRule(mock.Object).SetEmail(user.Email));
            
            //Assert
            Assert.Equal(true, result.IsCompleted);
        }
        
        
        [Fact]
        public async Task UserIsNotUnique_WhenRegistering()
        {
            var mock = new Mock<IUserUniquenessChecker>();
            var user = new Domain.Users.User("testgmail.com", "Name", "Surname", UserRole.Developer);
        
            //Arrange
            mock.Setup(x => x.IsUnique(user.Email)).ReturnsAsync(false);
        
            //Act
            Exception result = user.CheckRule(new UserEmailUniqueRule(mock.Object).SetEmail(user.Email)).Exception;
        
            //Assert
            Assert.Equal("Email already taken", result.InnerException.Message);
        }
    }
}