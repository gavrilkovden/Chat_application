using BusinessLogic.DTO;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Chat_application.Controllers;
using Chat_application.Models;
using DataAccessLayer.EntityDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace IntegrationTests
{
    [TestFixture]
    public class ParticipantsServiceIntegrationTests
    {
        private DbContextOptions<Context> _options;

        [OneTimeSetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("InMemoryDatabase")
                .Options;

            using (var context = new Context(_options))
            {

                var chatEntity1 = new ChatEntity { Id = 8, ChatName = "Chat 1" };
                var chatEntity2 = new ChatEntity { Id = 2, ChatName = "Chat 2" };

                var userEntity1 = new UserEntity { Id = 1, UserName = "User 1" };
                var userEntity2 = new UserEntity { Id = 2, UserName = "User 2" };

                context.DALChat.AddRange(chatEntity1, chatEntity2);
                context.DALUser.AddRange(userEntity1, userEntity2);
                context.SaveChanges();
            }
        }

        [Test]
        public void ConnectToChat_WithValidChatId_ReturnsOkResult()
        {
            // Arrange
            var chatId = 8;
            var userId = 1;

            using (var context = new Context(_options))
            {
                var participantsService = new ParticipantsService(context);
                var controller = new ParticipantsController(participantsService);

                // Act
                var result = controller.ConnectToChat(chatId, userId);

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result.Result);
                var okResult = result.Result as OkObjectResult;
                Assert.IsInstanceOf<ParticipantsDTO>(okResult.Value);
                var chatDTO = okResult.Value as ParticipantsDTO;
                Assert.AreEqual(userId, chatDTO.UserId);
                Assert.AreEqual(chatId, chatDTO.ChatId);
            }
        }
        [Test]
        public void GetChatParticipants_WithValidChatId_ReturnsOkResult()
        {
            // Arrange
            var chatId = 8;

            using (var context = new Context(_options))
            {
                var participantsService = new ParticipantsService(context);
                var controller = new ParticipantsController(participantsService);

                // Act
                var result = controller.GetChatParticipants(chatId);

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okResult = result as OkObjectResult;
                Assert.IsInstanceOf<IEnumerable<ParticipantsDTO>>(okResult.Value);
            }
        }

        [Test]
        public void DisconnectingfromChat_WithValidChatIdAndUserId_ReturnsOkResult()
        {
            // Arrange
            var chatId = 8;
            var userId = 1;

            using (var context = new Context(_options))
            {
                var participantsService = new ParticipantsService(context);
                var controller = new ParticipantsController(participantsService);

                // Act
                var result = controller.Disconnectingfrom—hat(chatId, userId);

                // Assert
                Assert.IsInstanceOf<OkResult>(result);
            }
        }
    }
}