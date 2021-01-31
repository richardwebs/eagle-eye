namespace EagleEye.API.Models
{
    public class MetadataOutput
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }

        public MetadataOutput() { }

        public MetadataOutput(int movieId, string title, string language, string duration, int releaseYear)
        {
            MovieId = movieId;
            Title = title;
            Language = language;
            Duration = duration;
            ReleaseYear = releaseYear;
        }
    }
}
