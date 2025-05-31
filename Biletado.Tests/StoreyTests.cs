using Biletado.Domain.Storey;using Moq;namespace Biletado.Tests{    public class StoreyTests    {        private Mock<IStoreyService> _storeyServiceMock;        public StoreyTests()        {            _storeyServiceMock = new Mock<IStoreyService>();        }        [Fact]        public async Task CreateNewAsync_ShouldReturnStorey_WhenCreationSucceeds() {            // Arrange            var storeyId = Guid.NewGuid();            var buildingId = Guid.NewGuid();            var newStorey = new Storey {                id = storeyId,                name = "Test",                building_id = buildingId            };            _storeyServiceMock.Setup(service => service.CreateStoreyAsync(newStorey)).ReturnsAsync(newStorey);            // Act            var result = await _storeyServiceMock.Object.CreateStoreyAsync(newStorey);            // Assert            Assert.Equal(newStorey, result);        }

        [Fact]
        public async Task GetAllStoreysAsync_ShouldReturnListOfStoreys_ForGivenBuildingId()
        {
            // Arrange
            var buildingId = Guid.NewGuid();
            var storeyId = Guid.NewGuid();

            var expectedStoreys = new List<Storey> {

                new Storey
                {
                    id = storeyId,
                    name = "TestStorey",
                    building_id = buildingId
                }
            };
   

            _storeyServiceMock
                .Setup(service => service.GetAllStoreysAsync(buildingId, false))
                .ReturnsAsync(expectedStoreys);

            // Act
            var result = await _storeyServiceMock.Object.GetAllStoreysAsync(buildingId, false);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expectedStoreys[0].id, result[0].id);
            Assert.Equal(expectedStoreys[0].name, result[0].name);
            Assert.Equal(expectedStoreys[0].building_id, result[0].building_id);

            _storeyServiceMock.Verify(s => s.GetAllStoreysAsync(buildingId, false), Times.Once);
        }
    }}