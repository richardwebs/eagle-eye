namespace EagleEye.DataAccess.Entities
{
    public class Metadata
    {
        public int MetadataId { get; set; }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Duration { get; set; }
        public int ReleaseYear { get; set; }

        public Metadata() { }

        public Metadata(int metadataId, int movieId, string title, string language, string duration, int releaseYear)
        {
            MetadataId = metadataId;
            MovieId = movieId;
            Title = title;
            Language = language;
            Duration = duration;
            ReleaseYear = releaseYear;
        }
    }
}
