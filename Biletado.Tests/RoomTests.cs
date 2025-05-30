using Biletado.Domain.Room;
using Biletado.Domain.Storey;
using Moq;

namespace Biletado.Tests
{
    public class RoomTests
    {
        private Mock<IRoomService> _roomServiceMock;

        public RoomTests()
        {
            _roomServiceMock = new Mock<IRoomService>();
        }

        [Fact]
        public async Task CreateNewAsync_ShouldReturnRoom_WhenCreationSucceeds()
        {

            // Arrange
            var Id = Guid.NewGuid();
            var storeyId = Guid.NewGuid();
            string name = "testroom";

            var newRoom = new Room
            {
                id = Id,
                storey_id = storeyId,
                name = name,
            };

            _roomServiceMock.Setup(service => service.CreateRoomAsync(newRoom)).ReturnsAsync(newRoom);

            // Act
            var result = await _roomServiceMock.Object.CreateRoomAsync(newRoom);

            // Assert
            Assert.Equal(newRoom, result);
        }
        [Fact]
        public async Task GetAllRoomsAsync_ShouldReturnListOfRooms_ForGivenStoreyId()
        {
            // Arrange
            var storeyId = Guid.NewGuid();
            var roomId = Guid.NewGuid();

            var expectedRooms = new List<Room> {


        new Room
        {
            id = roomId,
            name = "TestRoom",
            storey_id = storeyId
        }
                };


            _roomServiceMock
                .Setup(service => service.GetAllRoomsAsync(storeyId, false))
                .ReturnsAsync(expectedRooms);

            // Act
            var result = await _roomServiceMock.Object.GetAllRoomsAsync(storeyId, false);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expectedRooms[0].id, result[0].id);
            Assert.Equal(expectedRooms[0].name, result[0].name);
            Assert.Equal(expectedRooms[0].storey_id, result[0].storey_id);

            _roomServiceMock.Verify(s => s.GetAllRoomsAsync(storeyId, false), Times.Once);
        }


    }
}