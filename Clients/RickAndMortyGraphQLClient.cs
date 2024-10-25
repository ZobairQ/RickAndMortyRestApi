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

        public RickAndMortyGraphQLClient()
        {
            _client = new GraphQLHttpClient("https://rickandmortyapi.com/graphql", new NewtonsoftJsonSerializer());
        }

        public async Task<List<Character>> GetCharactersAsync(int page)
        {
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
            var response = await _client.SendQueryAsync<CharactersResponse>(request);
            return response.Data.Characters.Results;

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