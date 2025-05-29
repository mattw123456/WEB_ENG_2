using Biletado.Domain.Status;
using Moq;

namespace Biletado.Tests
{
    public class StatusTests
    {
        private Mock<IStatusService> _statusServiceMock;

        public StatusTests()
        {

            _statusServiceMock = new Mock<IStatusService>();
        }

        [Fact]
        public void GetStatusInformation_ShouldReturnStatusInformation_WhenRepositoryReturnsData()
        {
            //Arrange
            var authoren = new List<string> { "Finn Marin", "Matt Wagner" };
            var supportedApis = new List<string> { "jwt-v2", "assets-v3", "reservations-v2" };

            var expectedStatusInformation = new StatusInformation
            {
                authors = authoren,
                supportedApis = supportedApis
            };

            _statusServiceMock.Setup(service => service.GebeStatusInformationenZurueck()).Returns(expectedStatusInformation);


            //Act
            var result = _statusServiceMock.Object.GebeStatusInformationenZurueck();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedStatusInformation.authors, result.authors);
            Assert.Equal(expectedStatusInformation.supportedApis, result.supportedApis);
        }
    }
}
