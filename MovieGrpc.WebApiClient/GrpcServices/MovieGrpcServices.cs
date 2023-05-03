using ProtoDefinitions;

namespace MovieGrpc.WebApiClient.GrpcServices
{
    public class MovieGrpcServices
    {
        public readonly MoviesApi.MoviesApiClient _moviesApiClient;

        public MovieGrpcServices(MoviesApi.MoviesApiClient moviesApiClient)
        {
            _moviesApiClient = moviesApiClient;
        }

        public async Task<responseModel> GetByIdAsync(string id)
        {
            return await _moviesApiClient.GetByIdAsync(new IdRequest() { Id = id });

        }
        public async Task<responseModel> GetAllAsync()
        {
            return await _moviesApiClient.GetAllAsync(new Empty());
        }

        public async Task<responseModel> SearchAsync(string searchText)
        {
            return await _moviesApiClient.SearchAsync(new SearchRequest() { Text = searchText });
        }

    }
}
