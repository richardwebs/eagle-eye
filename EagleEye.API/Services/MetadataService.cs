using EagleEye.DataAccess.Entities;
using EagleEye.DataAccess.Repositories;
using System.Linq;
using System.Threading.Tasks;
using EagleEye.API.Models;

namespace EagleEye.API.Services
{

    public interface IMetadataService
    {
        Task<Metadata[]> GetMetadataByMovieId(int movieId);
        Task AddMetadata(MetadataInput metadata);
    }

    public class MetadataService : IMetadataService
    {
        private readonly IMetadataRepository _repository;

        public MetadataService(IMetadataRepository repository)
        {
            _repository = repository;
        }
        public async Task<Metadata[]> GetMetadataByMovieId(int movieId)
        {
            var metadata = await _repository.GetMetadataByMovieId(movieId);
            var grouping = metadata.GroupBy(x => x.Language);
            var result = grouping.Select(x => x.Single(y => y.MetadataId == x.Max(z => z.MetadataId))).OrderBy(x => x.Language); 
            return result.ToArray();
        }

        public async Task AddMetadata(MetadataInput metadata)
        {
            await _repository.AddMetadata(metadata.MovieId, metadata.Title, metadata.Language, metadata.Duration, metadata.ReleaseYear);
        }
    }
}
