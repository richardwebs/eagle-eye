using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EagleEye.API.Models;
using EagleEye.DataAccess.Entities;
using EagleEye.DataAccess.Repositories;

namespace EagleEye.API.Services
{
    public interface IStatsService
    {
        Task<StatsSummary[]> GetStatsSummary();
    }
    public class StatsService : IStatsService
    {
        private readonly IStatsRepository _statsRepository;
        private readonly IMetadataRepository _metadataRepository;

        public StatsService(IStatsRepository statsRepository, IMetadataRepository metadataRepository)
        {
            _statsRepository = statsRepository;
            _metadataRepository = metadataRepository;
        }

        public async Task<StatsSummary[]> GetStatsSummary()
        {
            var stats = await _statsRepository.GetAllStats();
            var metadata = await _metadataRepository.GetAllMetadata();
            var gouping = stats.GroupBy(x => x.MovieId);
            var result = gouping.Select(x => new Summary(x.Key, x.Count(), Convert.ToInt32(x.Average(y => y.WatchDurationMs)))).ToArray();
            return result.Select(x => GetStatsSummary(x, metadata)).Where(x => x != null).OrderByDescending(x => x.Watches).ToArray();
            
        }

        private static Metadata GetMetadataRecord(int movieId, Metadata[] metadata)
        {
            if (metadata.Count(x => x.MovieId == movieId) == 0) return null;
            var maxMetadataId = metadata.Where(x => x.MovieId == movieId).Max(x => x.MetadataId);

            return metadata.SingleOrDefault(x => x.MetadataId == maxMetadataId);
        }

        private static  StatsSummary GetStatsSummary(Summary summary, Metadata[] metadata)
        {
            var record = GetMetadataRecord(summary.MovieId, metadata);
            if (record == null) return null;
            return new StatsSummary(summary.MovieId, record.Title, summary.Avg / 1000, summary.Count, record.ReleaseYear);
        }

        private class Summary
        {
            public int MovieId { get; private set; }
            public int Count { get; private set; }
            public int Avg { get; private set; }
            public Summary() { }

            public Summary(int id, int count, int avg)
            {
                MovieId = id;
                Count = count;
                Avg = avg;
            }
        }
    }
}
