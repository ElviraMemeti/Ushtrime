using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using SOA2024.MovieReview.API.Dtos;
using UshtrimeKOL2.Interfaces;

namespace UshtrimeKOL2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController: ControllerBase
    {
        private IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        protected readonly TelemetryClient _telemetry;
        private readonly ILogger<MovieController> _logger;


        public MovieController(IMovieRepository movieRepository, IMapper mapper, TelemetryClient telemtry, ILogger<MovieController> logger)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _telemetry = telemtry;
            _logger = logger;
        }


        [HttpGet]
        //[Authorize]
        public ActionResult<List<MovieDto>> GetAllMovies()
        {
            try
            {
                var movies = _movieRepository.GetAll();
                var moviesDto = _mapper.Map<List<MovieDto>>(movies);

                return Ok(moviesDto);
            }
            catch (Exception e)
            {
                _telemetry.TrackException(e);
                return BadRequest();
            }
        }


    }
}
