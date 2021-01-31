namespace EagleEye.API.Models
{
    public class StatsSummary
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int AverageWatchDurationS { get; set; }
        public int Watches { get; set; }
        public int ReleaseYear { get; set; }

        public StatsSummary() {}

        public StatsSummary(int id, string title, int avg, int count, int year)
        {
            MovieId = id;
            Title = title;
            AverageWatchDurationS = avg;
            Watches = count;
            ReleaseYear = year;
        }
    }
}
