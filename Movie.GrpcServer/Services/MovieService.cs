using Grpc.Core;
using Movie.GrpcServer.Database.Entities;
using Movie.GrpcServer.Database.Repositories.Abstractions;
using MovieGrpc.Crosscutting.Commom;
using ProtoDefinitions;

namespace Movie.GrpcServer.Services
{
    public class MovieService : MoviesApi.MoviesApiBase
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IShowtimesRepository showtimesRepository, ILogger<MovieService> logger)
        {
            _showtimesRepository = showtimesRepository;
            _logger = logger;
        }

        public override async Task<responseModel> GetById(IdRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, Message.TRACE_METHOD + "GetById");

            responseModel responseModel = new responseModel();

            try
            {
                var showTimeEntity = await _showtimesRepository.GetWithMoviesByIdAsync(Convert.ToInt32(request.Id), context.CancellationToken);
                showResponse showTimeResponse = new showResponse() { 
                    Id = showTimeEntity.Id.ToString(),
                    Rank = "",
                    Title = showTimeEntity.Movie.Title,
                    FullTitle = showTimeEntity.Movie.FullTitle,
                    Year = showTimeEntity.SessionDate.Year.ToString(),
                    Image = "",
                    Crew = "",
                    ImDbRating = showTimeEntity.Movie.ImdbId,
                    ImDbRatingCount = ""
                };

                responseModel.Success = true;
                responseModel.Data = Google.Protobuf.WellKnownTypes.Any.Pack(showTimeResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, Message.ERRO_METHOD + $"GetById: {ex.Message}");
                responseModel.Success = false;
                responseModel.Exceptions.Add(new moviesApiException() { StatusCode = (int)StatusCode.DataLoss, Message = Message.ERRO_METHOD_EXPECTED_FAILURE });
            }

            return Task.FromResult(responseModel).Result;
        }
        
        public override async Task<responseModel> Search(SearchRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, Message.TRACE_METHOD + "Search");

            showListResponse showTimeListResponse = new showListResponse();
            responseModel responseModel = new responseModel();

            try
            {
                IEnumerable<ShowtimeEntity> showTimes = await _showtimesRepository.GetAllAsync(s => s.Movie.Title == request.Text || s.Movie.FullTitle == request.Text, context.CancellationToken);

                foreach (ShowtimeEntity showTime in showTimes)
                    showTimeListResponse.Shows.Add(new showResponse()
                    {
                        Id = showTime.Id.ToString(),
                        Rank = "",
                        Title = showTime.Movie.Title,
                        FullTitle = showTime.Movie.FullTitle,
                        Year = showTime.SessionDate.Year.ToString(),
                        Image = "",
                        Crew = "",
                        ImDbRating = showTime.Movie.ImdbId,
                        ImDbRatingCount = ""
                    });

                responseModel.Success = true;
                responseModel.Data = Google.Protobuf.WellKnownTypes.Any.Pack(showTimeListResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, Message.ERRO_METHOD + $"Search: {ex.Message}");
                responseModel.Success = false;
                responseModel.Exceptions.Add(new moviesApiException() { StatusCode = (int)StatusCode.DataLoss, Message = Message.ERRO_METHOD_EXPECTED_FAILURE });
            }

            return Task.FromResult(responseModel).Result;
        }

        public override async Task<responseModel> GetAll(Empty request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, Message.TRACE_METHOD + "GetAll");

            showListResponse showTimeListResponse = new showListResponse();
            responseModel responseModel = new responseModel();

            try
            {
                IEnumerable<ShowtimeEntity> showTimes = await _showtimesRepository.GetAllAsync(filter: null, context.CancellationToken);

                foreach (ShowtimeEntity showTime in showTimes)
                    showTimeListResponse.Shows.Add(new showResponse()
                    {
                        Id = showTime.Id.ToString(),
                        Rank = "",
                        Title = showTime.Movie.Title,
                        FullTitle = showTime.Movie.FullTitle,
                        Year = showTime.SessionDate.Year.ToString(),
                        Image = "",
                        Crew = "",
                        ImDbRating = showTime.Movie.ImdbId,
                        ImDbRatingCount = ""
                    });

                responseModel.Success = true;
                responseModel.Data = Google.Protobuf.WellKnownTypes.Any.Pack(showTimeListResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, Message.ERRO_METHOD + $"GetAll: {ex.Message}");
                responseModel.Success = false;
                responseModel.Exceptions.Add(new moviesApiException() { StatusCode = (int)StatusCode.DataLoss, Message = Message.ERRO_METHOD_EXPECTED_FAILURE });
            }

            return Task.FromResult(responseModel).Result;
        }

        public override Task<responseModel> CreateShowtimes(ShowTimeRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Information, Message.TRACE_METHOD + "CreateShowtimes");

            ShowtimeEntity showTimeEntity = new ShowtimeEntity
            {
                Id = request.Id,
                SessionDate = new DateTime(2023, 04, 20),
                Movie = new MovieEntity
                {
                    Id = 1,
                    Title = "Inception 2",
                    FullTitle = "Inception Full Movie 2",
                    ImdbId = "tt1375666",
                    ReleaseDate = new DateTime(2010, 01, 14),
                    Stars = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe"
                },
                AuditoriumId = 1,
            };


            return Task.FromResult(new responseModel
            {
                Success = true,
                Data = Google.Protobuf.WellKnownTypes.Any.Pack(new Empty())
                //Exceptions = null

            });
        }

        public override Task<responseModel> ReserveSeats(SeatNumberRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Debug, "Request Received for MovieService::GetAll");


            return Task.FromResult(new responseModel
            {
                Success = true,
                Data = Google.Protobuf.WellKnownTypes.Any.Pack(new Empty())
                //Exceptions = null

            });
        }

        public override Task<responseModel> BuySeats(SeatNumberRequest request, ServerCallContext context)
        {
            _logger.Log(LogLevel.Debug, "Request Received for MovieService::GetAll");


            return Task.FromResult(new responseModel
            {
                Success = true,
                Data = Google.Protobuf.WellKnownTypes.Any.Pack(new Empty())
                //Exceptions = null

            });
        }

    }
}
