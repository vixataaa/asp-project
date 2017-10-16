using Moq;
using NUnit.Framework;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Data.UnitTests
{
    [TestFixture]
    public class ChatsServiceTests
    {
        [Test]
        public void Constructor_Should_Throw_WhenGivenNullChatsRepo()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ChatsService(null, null, null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullAdvertsRepo()
        {
            // Arrange
            var chatRepo = new Mock<IChatsRepository>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ChatsService(chatRepo.Object, null, null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullUserRepo()
        {
            // Arrange
            var chatRepo = new Mock<IChatsRepository>();
            var advertRepo = new Mock<IAdvertisementsRepository>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ChatsService(chatRepo.Object, advertRepo.Object, null));
        }

        [Test]
        public void Constructor_Should_NotThrow_WhenGivenValidDependencies()
        {
            // Arrange
            var chatRepo = new Mock<IChatsRepository>();
            var advertRepo = new Mock<IAdvertisementsRepository>();
            var userRepo = new Mock<IUsersRepository>();

            // Act & Assert
            Assert.DoesNotThrow(() => new ChatsService(chatRepo.Object, advertRepo.Object, userRepo.Object));
        }


        [Test]
        public void GetChat_Should_CallChatsRepoFindChatWithTheGivenParticipantsAndAdvIdTwice()
        {
            // Arrange
            var chatRepo = new Mock<IChatsRepository>();
            var advertRepo = new Mock<IAdvertisementsRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var advId = new Guid();
            var participants = new[] { "pesho", "gosho" };

            chatRepo.Setup(x => x.FindChat(advId, participants)).Returns(new Chat { });

            var sut = new ChatsService(chatRepo.Object, advertRepo.Object, userRepo.Object);

            // Act
            sut.GetChat(advId, participants);

            // Assert
            chatRepo.Verify(x => x.FindChat(advId, participants), Times.Exactly(2));
        }

        [Test]
        public void GetChat_Should_ReturnTheFoundChat()
        {
            // Arrange
            var chatRepo = new Mock<IChatsRepository>();
            var advertRepo = new Mock<IAdvertisementsRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var advId = new Guid();
            var participants = new[] { "pesho", "gosho" };
            var expectedChat = new Chat { };

            chatRepo.Setup(x => x.FindChat(advId, participants)).Returns(expectedChat);

            var sut = new ChatsService(chatRepo.Object, advertRepo.Object, userRepo.Object);

            // Act
            var result = sut.GetChat(advId, participants);

            // Assert
            Assert.AreEqual(expectedChat, result);
        }

        [Test]
        public void GetChatById_Should_CallAndReturnChatRepoGetById()
        {
            // Arrange
            var chatRepo = new Mock<IChatsRepository>();
            var advertRepo = new Mock<IAdvertisementsRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var id = new Guid();
            var expectedChat = new Chat { };

            chatRepo.Setup(x => x.GetById(id)).Returns(expectedChat);

            var sut = new ChatsService(chatRepo.Object, advertRepo.Object, userRepo.Object);

            // Act
            var result = sut.GetChatById(id);

            // Assert
            Assert.AreEqual(expectedChat, result);
        }


        [Test]
        public void CreateMessage_Should_AddTheMessageToTheChatAndReturnIt()
        {
            // Arrange
            var chatRepo = new Mock<IChatsRepository>();
            var advertRepo = new Mock<IAdvertisementsRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var chat = new Chat
            {
                Messages = new List<Message>()
            };

            var user = new ApplicationUser
            {
                UserName = "Pesho"
            };

            string message = "Hello VisualBasic";

            var sut = new ChatsService(chatRepo.Object, advertRepo.Object, userRepo.Object);

            // Act
            var result = sut.CreateMessage(chat, user, message);

            // Assert
            Assert.IsInstanceOf<Message>(result);
            Assert.AreEqual(1, chat.Messages.Count);
            Assert.AreEqual(user.UserName, result.Author.UserName);
            Assert.AreEqual(message, result.Text);
        }

        [Test]
        public void GetUserChats_Should_CallChatsRepoGetUserChatsWithTheGivenUsernameAndReturnIt()
        {
            // Arrange
            var chatRepo = new Mock<IChatsRepository>();
            var advertRepo = new Mock<IAdvertisementsRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var username = "username";
            var dbModel = new List<Chat>()
            {

            }.AsQueryable();

            chatRepo.Setup(x => x.GetUserChats(username)).Returns(dbModel);

            var sut = new ChatsService(chatRepo.Object, advertRepo.Object, userRepo.Object);

            // Act
            var result = sut.GetUserChats(username);

            // Assert
            chatRepo.Verify(x => x.GetUserChats(username), Times.Once);
            Assert.AreEqual(dbModel, result);
        }
    }
}
