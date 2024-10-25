// Controllers/CharacterController.cs
using Microsoft.AspNetCore.Mvc;

namespace RickAndMorty
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IRickAndMortyGraphQLClient _graphqlClient;
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(IRickAndMortyGraphQLClient graphqlClient, ILogger<CharactersController> logger)
        {
            this._graphqlClient = graphqlClient;
            this._logger = logger;
        }

        /// <summary>
        /// Retrieves a list of characters from the Rick and Morty GraphQL API with pagination.
        /// </summary>
        /// <param name="page">The page number for pagination</param>
        /// <returns>A list of characters</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Character>), 200)]
        public async Task<IActionResult> GetCharacters([FromQuery] int page = 1)
        {
            this._logger.LogInformation("Received request for character list on page: {Page}", page);
            try
            {
                var characters = await _graphqlClient.GetCharactersAsync(page);
                return Ok(characters);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching characters for page: {Page}", page);
                return StatusCode(500, "Internal server error");
            }

        }
    }

}