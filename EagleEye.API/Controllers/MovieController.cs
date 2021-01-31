using EagleEye.API.Models;
using EagleEye.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEye.API.Controllers
{
    public interface IMovieController
    {
        Task<ActionResult<MetadataOutput[]>> GetMetadataByMovieId(int movieId);
        Task<ActionResult<StatsSummary[]>> GetStatsSummary();
        Task<ActionResult> AddMetadata(MetadataInput metadata);
    }

    [ApiController]
    public class MovieController : ControllerBase, IMovieController
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMetadataService _metadataService;
        private readonly IStatsService _statsService;

        public MovieController(ILogger<MovieController> logger, IMetadataService metadataService, IStatsService statsService)
        {
            _logger = logger;
            _metadataService = metadataService;
            _statsService = statsService;
        }

        [HttpGet, Route("metadata/{movieId}")]
        public async Task<ActionResult<MetadataOutput[]>> GetMetadataByMovieId(int movieId)
        {
            var output = await _metadataService.GetMetadataByMovieId(movieId);
            if (output.Length == 0) return NotFound();
            var result = output.Select(x => new MetadataOutput(x.MovieId, x.Title, x.Language, x.Duration, x.ReleaseYear)).ToArray();
            return Ok(result);
        }

        [HttpGet, Route("movies/stats")]
        public async Task<ActionResult<StatsSummary[]>> GetStatsSummary()
        {
            var result = await _statsService.GetStatsSummary();
            return Ok(result);
        }

        [HttpPost, Route("metadata")]
        public async Task<ActionResult> AddMetadata(MetadataInput metadata)
        {
            await _metadataService.AddMetadata(metadata);
            return Ok();
        }

    }
}
