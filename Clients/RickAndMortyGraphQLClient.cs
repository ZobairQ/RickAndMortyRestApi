// Infrastructure/RickAndMortyGraphQLClient.cs
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Threading.Tasks;
using System.Net.Http;
using GraphQL;
using GraphQL.Client.Abstractions;
namespace RickAndMorty
{


    public interface IRickAndMortyGraphQLClient
    {
        Task<List<Character>> GetCharactersAsync(int page);
    }

    public class RickAndMortyGraphQLClient : IRickAndMortyGraphQLClient
    {
        private readonly GraphQLHttpClient _client;
        private readonly ILogger<RickAndMortyGraphQLClient> _logger;

        public RickAndMortyGraphQLClient(ILogger<RickAndMortyGraphQLClient> logger)
        {
            _client = new GraphQLHttpClient("https://rickandmortyapi.com/graphql", new NewtonsoftJsonSerializer());
            this._logger = logger;
        }

        public async Task<List<Character>> GetCharactersAsync(int page)
        {
            _logger.LogInformation("Fetching characters for page: {Page} from GraphQL API", page);
            var request = new GraphQLRequest
            {
                Query = """
                query($page: Int) {
                    characters(page: $page) {
                     results {
                        id
                        name
                        status
                        species
                        gender
                        origin {
                            name
                        }
                        location {
                            name
                        }
                      }
                    }
                }
                """,
                Variables = new { page }
            };
            try
            {
                var response = await _client.SendQueryAsync<CharactersResponse>(request);
                this._logger.LogInformation("Successfully fetched {Count} characters for page: {Page} from GraphQL API",
                                   response.Data.Characters.Results.Count, page);
                return response.Data.Characters.Results;
            }
            catch (System.Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching characters for page: {Page} from GraphQL API", page);
                throw;
            }


        }
    }


    public class CharactersResponse
    {
        public required CharactersData Characters { get; set; }
    }

    public class CharactersData
    {
        public required List<Character> Results { get; set; }
    }

}