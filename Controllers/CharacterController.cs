// Controllers/CharacterController.cs
using Microsoft.AspNetCore.Mvc;

namespace RickAndMorty
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IRickAndMortyGraphQLClient _graphqlClient;

        public CharactersController(IRickAndMortyGraphQLClient graphqlClient)
        {
            _graphqlClient = graphqlClient;
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
            var characters = await _graphqlClient.GetCharactersAsync(page);
            return Ok(characters);
        }
    }

}