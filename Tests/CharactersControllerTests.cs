using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using RickAndMorty;

namespace RickAndMortyRestApi.Tests
{
    public class CharactersControllerTests
    {
        private readonly Mock<IRickAndMortyGraphQLClient> _mockGraphQLClient;
        private readonly CharactersController _controller;

        public CharactersControllerTests()
        {
            _mockGraphQLClient = new Mock<IRickAndMortyGraphQLClient>();
            _controller = new CharactersController(_mockGraphQLClient.Object, new Mock<ILogger<CharactersController>>().Object);
        }

        [Fact]
        public async Task GetCharacters_ReturnsListOfCharacters_WhenCharactersExist()
        {
            // Arrange
            var page = 1;
            var expectedCharacters = new List<Character>
            {
                new Character
                {
                    Id = 1,
                    Name = "Rick Sanchez",
                    Status = "Alive",
                    image = "test",
                    Species = "Human",
                    Gender = "Male",
                    Origin = new Origin { Name = "Earth (C-137)" },
                    Location = new Location { Name = "Earth (Replacement Dimension)" }
                },
                new Character
                {
                    Id = 2,
                    Name = "Morty Smith",
                    Status = "Alive",
                    image = "test2",
                    Species = "Human",
                    Gender = "Male",
                    Origin = new Origin { Name = "Earth (C-137)" },
                    Location = new Location { Name = "Earth (Replacement Dimension)" }
                }
            };

            _mockGraphQLClient.Setup(client => client.GetCharactersAsync(page))
                .ReturnsAsync(expectedCharacters);

            // Act
            var result = await _controller.GetCharacters(page) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedCharacters, result.Value);
        }

        [Fact]
        public async Task GetCharacters_ReturnsEmptyList_WhenNoCharactersExist()
        {
            // Arrange
            var page = 2;
            var expectedCharacters = new List<Character>();

            _mockGraphQLClient.Setup(client => client.GetCharactersAsync(page))
                .ReturnsAsync(expectedCharacters);

            // Act
            var result = await _controller.GetCharacters(page) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.Empty((List<Character>)result.Value);
        }
    }
}
