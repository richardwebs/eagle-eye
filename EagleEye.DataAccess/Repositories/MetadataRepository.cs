using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EagleEye.DataAccess.Entities;

namespace EagleEye.DataAccess.Repositories
{
    public interface IMetadataRepository
    {
        Task<Metadata[]> GetAllMetadata();
        Task<Metadata[]> GetMetadataByMovieId(int movieId);
        Task AddMetadata(int movieId, string title, string language, string duration, int releaseYear);
    }

    public class MetadataRepository : IMetadataRepository
    {
        private const string Filename = @"Data\metadata.csv";
        public async Task<Metadata[]> GetAllMetadata()
        {
            var result = new List<Metadata>();
            using (var reader = new StreamReader(Filename, Encoding.UTF8))
            {
                await reader.ReadLineAsync();
                while (reader.EndOfStream == false)
                {
                    var lineStr = await reader.ReadLineAsync();
                    if (lineStr.Length == 0) throw new Exception("Blank line in data file");
                    var pieces = lineStr.Split(',');
                    if (pieces.Length == 7 && lineStr.Contains('"'))
                    {
                        pieces[2] = pieces[2] + pieces[3];
                        pieces[2] = pieces[2].Replace("\"", "");
                        pieces[3] = pieces[4];
                        pieces[4] = pieces[5];
                        pieces[5] = pieces[6];
                    }
                    if (pieces[2].Trim().Length == 0) throw new Exception("Blank title in data file: " + lineStr);
                    if (pieces[3].Trim().Length == 0) throw new Exception("Blank language in data file: " + lineStr);
                    if (pieces[4].Trim().Length == 0) throw new Exception("Blank duration in data file: " + lineStr);
                    var metadata = new Metadata
                    {
                        MetadataId = int.Parse(pieces[0]),
                        MovieId = int.Parse(pieces[1]),
                        Title = pieces[2].Trim(),
                        Language = pieces[3].Trim(),
                        Duration = pieces[4].Trim(),
                        ReleaseYear = int.Parse(pieces[5])
                    };
                    result.Add(metadata);
                }
            }

            return result.ToArray();
        }

        public async Task<Metadata[]> GetMetadataByMovieId(int movieId)
        {
            var metadata = await GetAllMetadata();
            return metadata.Where(x => x.MovieId == movieId).ToArray();
        }

        public async Task AddMetadata(int movieId, string title, string language, string duration, int releaseYear)
        {
            await Task.Run(() =>
            {
                return;
            });
        }
    }
}
