using Microsoft.AspNetCore.SignalR;
using MapUserMVC.Controllers;
using MapUserMVC.Hubs;
using MapUserMVC.Models;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Testing
{
    public class HomeControllerTest
    {
        [Fact]
        public void TestIndex()
        {
            // Arrange
            var mockRepo = new Mock<IHubContext<NotificationUserHub>>();
            var mockUser = new Mock<IUserConnectionManager>();
            var controller = new HomeController(mockRepo.Object, mockUser.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void TestUserBoard()
        {
            // Arrange
            var mockRepo = new Mock<IHubContext<NotificationUserHub>>();
            var mockUser = new Mock<IUserConnectionManager>();
            var controller = new HomeController(mockRepo.Object, mockUser.Object);

            // Act
            var result = controller.UserBoard();

            // Assert
            Assert.IsType<ViewResult>(result);

        }
    }
}
