namespace EagleEye.DataAccess.Entities
{
    public class Stats
    {
        public int MovieId { get; set; }
        public int WatchDurationMs { get; set; }

        public Stats() { }

        public Stats(int id, int duration)
        {
            MovieId = id;
            WatchDurationMs = duration;
        }
    }
}
