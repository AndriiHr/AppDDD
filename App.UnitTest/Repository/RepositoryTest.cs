using System.Threading.Tasks;
using App.Domain.IRepositories;
using App.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using Xunit;

// 

namespace App.UnitTest.Repository
{
    public class RepositoryTest
    {
        [Fact]
        public async void AddOTestClassObject()
        {
            var dbContextMock = new Mock<Infrastructure.Database.AppContext>();
            var dbSetMock = new Mock<DbSet<Domain.Users.User>>();
            
            var user = new Domain.Users.User()
            {
                Id = 1
            };
            
            // dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>()))
            //     .ReturnsAsync(new Domain.Users.User() {Id = 1});
            dbContextMock.Setup(s => s.Set<Domain.Users.User>()).Returns(dbSetMock.Object);


            var productRepository = new Repository<Domain.Users.User>(dbContextMock.Object);
            var product = productRepository.GetSingle(x => x.Id == 1).Result;
            
            
            Assert.NotNull(product);
            Assert.IsAssignableFrom<TestClass>(product);
        }
    }
}