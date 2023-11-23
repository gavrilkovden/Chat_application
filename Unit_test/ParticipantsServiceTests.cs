//using BusinessLogic.Services;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using ExceptionHandling.Exceptions;
//using DataAccessLayer.EntityDB;

//namespace Unit_test
//{
//    public class ParticipantsServiceTests
//    {
//        [Fact]
//        public void GetChatParticipants_WithValidChatId_ReturnsParticipants()
//        {
//            // Arrange
//            var chatId = 1;

//            // Создаем макет контекста базы данных
//            var contextOptions = new DbContextOptions<ChatDbContext>();
//            var contextMock = new Mock<ChatDbContext>(contextOptions);
//            var participantsDbSetMock = new Mock<DbSet<ParticipantsEntity>>();

//            // Настроим DbSet макета для запросов

//            var participantsData = new List<ParticipantsEntity>
//            {
//                new ParticipantsEntity { UserId = 1, ChatId = chatId, IsAdmin = false },
//                new ParticipantsEntity { UserId = 2, ChatId = chatId, IsAdmin = false }
//            };

//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Provider).Returns(participantsData.AsQueryable().Provider);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Expression).Returns(participantsData.AsQueryable().Expression);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.ElementType).Returns(participantsData.AsQueryable().ElementType);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.GetEnumerator()).Returns(participantsData.GetEnumerator());

//            contextMock.Setup(c => c.Participants).Returns(participantsDbSetMock.Object);

//            var participantsService = new ParticipantsService(contextMock.Object);

//            // Act
//            var participants = participantsService.GetChatParticipants(chatId);

//            // Assert
//            Assert.NotNull(participants);
//            Assert.Equal(2, participants.Count());
//        }

//        [Fact]
//        public void GetChatParticipants_WithInvalidChatId_ThrowsInvalidInputException()
//        {
//            // Arrange
//            var chatId = 0;

//            // Создаем макет контекста базы данных
//            var contextOptions = new DbContextOptions<ChatDbContext>();
//            var contextMock = new Mock<ChatDbContext>(contextOptions);

//            var participantsService = new ParticipantsService(contextMock.Object);

//            // Act and Assert
//            Assert.Throws<ChatInvalidInputException>(() => participantsService.GetChatParticipants(chatId));
//        }

//        [Fact]
//        public void ConnectToChat_WithValidChatId_ReturnsParticipants()
//        {
//            // Arrange
//            var chatId = 1;
//            var userId = 1;

//            // Создаем макет контекста базы данных
//            var contextOptions = new DbContextOptions<ChatDbContext>();
//            var contextMock = new Mock<ChatDbContext>(contextOptions);
//            var participantsDbSetMock = new Mock<DbSet<ParticipantsEntity>>();
//            var userDbSetMock = new Mock<DbSet<UserEntity>>();
//            var chatDbSetMock = new Mock<DbSet<ChatEntity>>();
//            // Настроим DbSet макета для запросов
//            var usersData = new List<UserEntity>
//            {
//                new UserEntity { Id = 1, UserName = "Jon"},
//                new UserEntity { Id = 2, UserName = "Jek" }
//            };

//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.Provider).Returns(usersData.AsQueryable().Provider);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.Expression).Returns(usersData.AsQueryable().Expression);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.ElementType).Returns(usersData.AsQueryable().ElementType);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.GetEnumerator()).Returns(usersData.GetEnumerator());

//            var chatData = new List<ChatEntity>
//            {
//                new ChatEntity { Id = 1, ChatName = "test"},
//                new ChatEntity { Id = 1, ChatName = "test" }
//            };

//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.Provider).Returns(chatData.AsQueryable().Provider);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.Expression).Returns(chatData.AsQueryable().Expression);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.ElementType).Returns(chatData.AsQueryable().ElementType);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.GetEnumerator()).Returns(chatData.GetEnumerator());

//            var participantsData = new List<ParticipantsEntity>
//            {
//                new ParticipantsEntity { UserId = userId, ChatId = chatId, IsAdmin = false },
//            };

//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Provider).Returns(participantsData.AsQueryable().Provider);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Expression).Returns(participantsData.AsQueryable().Expression);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.ElementType).Returns(participantsData.AsQueryable().ElementType);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.GetEnumerator()).Returns(participantsData.GetEnumerator());

//            contextMock.Setup(c => c.Participants).Returns(participantsDbSetMock.Object);
//            contextMock.Setup(c => c.Users).Returns(userDbSetMock.Object);
//            contextMock.Setup(c => c.Chats).Returns(chatDbSetMock.Object);
//            var participantsService = new ParticipantsService(contextMock.Object);

//            // Act
//            var participants = participantsService.ConnectToChat(chatId, userId);

//            // Assert
//            Assert.NotNull(participants);
//            Assert.Equal(userId, participants.UserId);
//            Assert.Equal(chatId, participants.ChatId);
//        }

//        [Fact]
//        public void ConnectToChat_WithValidChatId_ThrowsNotFoundException()
//        {
//            // Arrange
//            var chatId = 2;
//            var userId = 1;

//            // Создаем макет контекста базы данных
//            var contextOptions = new DbContextOptions<ChatDbContext>();
//            var contextMock = new Mock<ChatDbContext>(contextOptions);
//            var participantsDbSetMock = new Mock<DbSet<ParticipantsEntity>>();
//            var userDbSetMock = new Mock<DbSet<UserEntity>>();
//            var chatDbSetMock = new Mock<DbSet<ChatEntity>>();
//            // Настроим DbSet макета для запросов
//            var usersData = new List<UserEntity>
//            {
//                new UserEntity { Id = 1, UserName = "Jon"},
//                new UserEntity { Id = 2, UserName = "Jek" }
//            };

//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.Provider).Returns(usersData.AsQueryable().Provider);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.Expression).Returns(usersData.AsQueryable().Expression);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.ElementType).Returns(usersData.AsQueryable().ElementType);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.GetEnumerator()).Returns(usersData.GetEnumerator());

//            var chatData = new List<ChatEntity>
//            {
//                new ChatEntity { Id = 1, ChatName = "test"},
//                new ChatEntity { Id = 1, ChatName = "test" }
//            };

//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.Provider).Returns(chatData.AsQueryable().Provider);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.Expression).Returns(chatData.AsQueryable().Expression);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.ElementType).Returns(chatData.AsQueryable().ElementType);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.GetEnumerator()).Returns(chatData.GetEnumerator());

//            var participantsData = new List<ParticipantsEntity>
//            {
//                new ParticipantsEntity { UserId = userId, ChatId = chatId, IsAdmin = false },

//            };

//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Provider).Returns(participantsData.AsQueryable().Provider);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Expression).Returns(participantsData.AsQueryable().Expression);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.ElementType).Returns(participantsData.AsQueryable().ElementType);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.GetEnumerator()).Returns(participantsData.GetEnumerator());

//            contextMock.Setup(c => c.Participants).Returns(participantsDbSetMock.Object);
//            contextMock.Setup(c => c.Users).Returns(userDbSetMock.Object);
//            contextMock.Setup(c => c.Chats).Returns(chatDbSetMock.Object);
//            var participantsService = new ParticipantsService(contextMock.Object);

//            // Act and Assert

//            Assert.Throws<ChatNotFoundException>(() => participantsService.ConnectToChat(chatId, userId));

//        }
//        [Fact]
//        public void LeaveChat_WithValidChatId_ReturnsBool()
//        {
//            // Arrange
//            var chatId = 1;
//            var userId = 1;

//            // Создаем макет контекста базы данных
//            var contextOptions = new DbContextOptions<ChatDbContext>();
//            var contextMock = new Mock<ChatDbContext>(contextOptions);
//            var participantsDbSetMock = new Mock<DbSet<ParticipantsEntity>>();
//            var userDbSetMock = new Mock<DbSet<UserEntity>>();
//            var chatDbSetMock = new Mock<DbSet<ChatEntity>>();
//            // Настроим DbSet макета для запросов
//            var usersData = new List<UserEntity>
//            {
//                new UserEntity { Id = 1, UserName = "Jon"},
//                new UserEntity { Id = 2, UserName = "Jek" }
//            };

//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.Provider).Returns(usersData.AsQueryable().Provider);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.Expression).Returns(usersData.AsQueryable().Expression);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.ElementType).Returns(usersData.AsQueryable().ElementType);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.GetEnumerator()).Returns(usersData.GetEnumerator());

//            var chatData = new List<ChatEntity>
//            {
//                new ChatEntity { Id = 1, ChatName = "test"},
//                new ChatEntity { Id = 1, ChatName = "test" }
//            };

//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.Provider).Returns(chatData.AsQueryable().Provider);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.Expression).Returns(chatData.AsQueryable().Expression);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.ElementType).Returns(chatData.AsQueryable().ElementType);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.GetEnumerator()).Returns(chatData.GetEnumerator());

//            var participantsData = new List<ParticipantsEntity>
//            {
//                new ParticipantsEntity { UserId = userId, ChatId = chatId, IsAdmin = false },

//            };

//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Provider).Returns(participantsData.AsQueryable().Provider);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Expression).Returns(participantsData.AsQueryable().Expression);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.ElementType).Returns(participantsData.AsQueryable().ElementType);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.GetEnumerator()).Returns(participantsData.GetEnumerator());

//            contextMock.Setup(c => c.Participants).Returns(participantsDbSetMock.Object);
//            contextMock.Setup(c => c.Users).Returns(userDbSetMock.Object);
//            contextMock.Setup(c => c.Chats).Returns(chatDbSetMock.Object);
//            var participantsService = new ParticipantsService(contextMock.Object);

//            // Act and Assert
//            Assert.True(participantsService.LeaveChat(chatId, userId));
//        }

//        [Fact]
//        public void LeaveChat_ThrowsNotFoundException()
//        {
//            // Arrange
//            var chatId = 1;
//            var userId = 8;

//            // Создаем макет контекста базы данных
//            var contextOptions = new DbContextOptions<ChatDbContext>();
//            var contextMock = new Mock<ChatDbContext>(contextOptions);
//            var participantsDbSetMock = new Mock<DbSet<ParticipantsEntity>>();
//            var userDbSetMock = new Mock<DbSet<UserEntity>>();
//            var chatDbSetMock = new Mock<DbSet<ChatEntity>>();
//            // Настроим DbSet макета для запросов
//            var usersData = new List<UserEntity>
//            {
//                new UserEntity { Id = 1, UserName = "Jon"},
//                new UserEntity { Id = 2, UserName = "Jek" }
//            };

//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.Provider).Returns(usersData.AsQueryable().Provider);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.Expression).Returns(usersData.AsQueryable().Expression);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.ElementType).Returns(usersData.AsQueryable().ElementType);
//            userDbSetMock.As<IQueryable<UserEntity>>().Setup(m => m.GetEnumerator()).Returns(usersData.GetEnumerator());

//            var chatData = new List<ChatEntity>
//            {
//                new ChatEntity { Id = 1, ChatName = "test"},
//                new ChatEntity { Id = 1, ChatName = "test" }
//            };

//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.Provider).Returns(chatData.AsQueryable().Provider);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.Expression).Returns(chatData.AsQueryable().Expression);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.ElementType).Returns(chatData.AsQueryable().ElementType);
//            chatDbSetMock.As<IQueryable<ChatEntity>>().Setup(m => m.GetEnumerator()).Returns(chatData.GetEnumerator());

//            var participantsData = new List<ParticipantsEntity>
//            {
//                new ParticipantsEntity { UserId = userId, ChatId = chatId, IsAdmin = false },

//            };

//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Provider).Returns(participantsData.AsQueryable().Provider);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.Expression).Returns(participantsData.AsQueryable().Expression);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.ElementType).Returns(participantsData.AsQueryable().ElementType);
//            participantsDbSetMock.As<IQueryable<ParticipantsEntity>>().Setup(m => m.GetEnumerator()).Returns(participantsData.GetEnumerator());

//            contextMock.Setup(c => c.Participants).Returns(participantsDbSetMock.Object);
//            contextMock.Setup(c => c.Users).Returns(userDbSetMock.Object);
//            contextMock.Setup(c => c.Chats).Returns(chatDbSetMock.Object);
//            var participantsService = new ParticipantsService(contextMock.Object);

//            // Act and Assert
//            Assert.Throws<ChatNotFoundException>(() => participantsService.LeaveChat(chatId, userId));
//        }
//    }
//}
