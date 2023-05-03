using Microsoft.AspNetCore.Mvc;
using MovieGrpc.WebApiClient.GrpcServices;
using ProtoDefinitions;

namespace MovieGrpc.WebApiClient.Controllers
{
    [ApiController]
    //[Route("/v1/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IConfiguration _configuration;
        private readonly MovieGrpcServices _movieGrpcServices;
        public MoviesController(ILogger<MoviesController> logger, IConfiguration configuration, MovieGrpcServices movieGrpcServices)
        {
            _logger = logger;
            _configuration = configuration;
            _movieGrpcServices = movieGrpcServices;
        }


        [HttpGet]
        [Route("/v1/movies/{id}")]
        public async Task<ActionResult<responseModel>> GetById(string id)
        {
            _logger.Log(LogLevel.Information, "Request Received for MoviesController::GetById");
            responseModel responseModel = new responseModel();

            responseModel = await _movieGrpcServices.GetByIdAsync(id);

            _logger.Log(LogLevel.Information, "Sending Response from MoviesController::GetById");

            if (responseModel.Success)
            {
                responseModel.Data.TryUnpack<showResponse>(out var dataAll);
                return Ok(dataAll);
            }
            else
                return BadRequest(responseModel.Exceptions);
        }

        [HttpGet]
        [Route("/v1/movies")]
        public async Task<ActionResult<responseModel>> Get(string? search)
        {
            _logger.Log(LogLevel.Information, "Request Received for MoviesController::GetAll");
            responseModel responseModel = new responseModel();

            if (string.IsNullOrEmpty(search))
                responseModel = await _movieGrpcServices.GetAllAsync();
            else
                responseModel = await _movieGrpcServices.SearchAsync(search);


            _logger.Log(LogLevel.Information, "Sending Response from MoviesController::GetAll");

            if (responseModel.Success)
            {
                responseModel.Data.TryUnpack<showListResponse>(out var dataAll);
                return Ok(dataAll.Shows);
            }
            else
                return BadRequest(responseModel.Exceptions);

        }
    }
}
