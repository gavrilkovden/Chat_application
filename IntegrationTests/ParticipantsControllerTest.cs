//using BusinessLogic.DTO;
//using BusinessLogic.Services;
//using Chat_application.Controllers;
//using DataAccessLayer.EntityDB;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace IntegrationTests
//{
//    [TestFixture]
//    public class ParticipantsServiceIntegrationTests
//    {
//        private DbContextOptions<ChatDbContext> _options;

//        [OneTimeSetUp]
//        public void Setup()
//        {
//            _options = new DbContextOptionsBuilder<ChatDbContext>()
//                .UseInMemoryDatabase("InMemoryDatabase")
//                .Options;

//            using (var context = new ChatDbContext(_options))
//            {

//                var chatEntity1 = new ChatEntity { Id = 8, ChatName = "Chats 1" };
//                var chatEntity2 = new ChatEntity { Id = 2, ChatName = "Chats 2" };

//                var userEntity1 = new UserEntity { Id = 1, UserName = "Users 1" };
//                var userEntity2 = new UserEntity { Id = 2, UserName = "Users 2" };

//                context.Chats.AddRange(chatEntity1, chatEntity2);
//                context.Users.AddRange(userEntity1, userEntity2);
//                context.SaveChanges();
//            }
//        }

//        [Test]
//        public void ConnectToChat_WithValidChatId_ReturnsOkResult()
//        {
//            // Arrange
//            var chatId = 8;
//            var userId = 1;

//            using (var context = new ChatDbContext(_options))
//            {
//                var participantsService = new ParticipantsService(context);
//                var controller = new ParticipantsController(participantsService);

//                // Act
//                var result = controller.Create(chatId, userId);

//                // Assert
//                Assert.IsInstanceOf<OkObjectResult>(result.Result);
//                var okResult = result.Result as OkObjectResult;
//                Assert.IsInstanceOf<ParticipantsDTO>(okResult.Value);
//                var chatDTO = okResult.Value as ParticipantsDTO;
//                Assert.AreEqual(userId, chatDTO.UserId);
//                Assert.AreEqual(chatId, chatDTO.ChatId);
//            }
//        }
//        [Test]
//        public void GetChatParticipants_WithValidChatId_ReturnsOkResult()
//        {
//            // Arrange
//            var chatId = 8;

//            using (var context = new ChatDbContext(_options))
//            {
//                var participantsService = new ParticipantsService(context);
//                var controller = new ParticipantsController(participantsService);

//                // Act
//                var result = controller.GetChatParticipants(chatId);

//                // Assert
//                Assert.IsInstanceOf<OkObjectResult>(result);
//                var okResult = result as OkObjectResult;
//                Assert.IsInstanceOf<IEnumerable<ParticipantsDTO>>(okResult.Value);
//            }
//        }

//        [Test]
//        public void DisconnectingfromChat_WithValidChatIdAndUserId_ReturnsOkResult()
//        {
//            // Arrange
//            var chatId = 8;
//            var userId = 1;

//            using (var context = new ChatDbContext(_options))
//            {
//                var participantsService = new ParticipantsService(context);
//                var controller = new ParticipantsController(participantsService);

//                // Act
//                var result = controller.Disconnectingfrom—hat(chatId, userId);

//                // Assert
//                Assert.IsInstanceOf<OkResult>(result);
//            }
//        }
//    }
//}