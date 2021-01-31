using EagleEye.API.Models;
using EagleEye.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEye.API.Controllers
{
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMetadataService _metadataService;
        private readonly IStatsService _statsService;

        public MovieController(ILogger<MovieController> logger, IMetadataService metadataService,
            IStatsService statsService)
        {
            _logger = logger;
            _metadataService = metadataService;
            _statsService = statsService;
        }

        [HttpGet, Route("metadata/{movieId}")]
        public async Task<MetadataOutput[]> GetMetadataByMovieId(int movieId)
        {
            var result = await _metadataService.GetMetadataByMovieId(movieId);
            return result.Select(x => new MetadataOutput(x.MovieId, x.Title, x.Language, x.Duration, x.ReleaseYear)).ToArray();
        }

        [HttpGet, Route("movies/stats")]
        public async Task<StatsSummary[]> GetStatsSummary()
        {
            return await _statsService.GetStatsSummary();
        }

    }
}
