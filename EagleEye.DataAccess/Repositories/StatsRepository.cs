using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EagleEye.DataAccess.Entities;

namespace EagleEye.DataAccess.Repositories
{
    public interface IStatsRepository
    {
        Task<Stats[]> GetAllStats();
    }
    public class StatsRepository : IStatsRepository
    {
        private const string Filename = @"Data\stats.csv";
        public async Task<Stats[]> GetAllStats()
        {
            var result = new List<Stats>();
            using (var reader = new StreamReader(Filename, Encoding.UTF8))
            {
                await reader.ReadLineAsync();
                while (reader.EndOfStream == false)
                {
                    var lineStr = await reader.ReadLineAsync();
                    if (lineStr.Length == 0) throw new Exception("Blank line in data file");
                    var pieces = lineStr.Split(',');
                    if (pieces.Length != 2) throw new Exception("Corrupt line in data file: " + lineStr);
                    var stats = new Stats
                    {
                        MovieId = int.Parse(pieces[0]),
                        WatchDurationMs = int.Parse(pieces[1])
                    };
                    result.Add(stats);
                }
            }

            return result.ToArray();
        }
    }
}
