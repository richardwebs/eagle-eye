using System.ComponentModel.DataAnnotations;

namespace EagleEye.API.Models
{
    public class MetadataInput
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public int ReleaseYear { get; set; }

        public MetadataInput() { }
        public MetadataInput(int movieId, string title, string language, string duration, int releaseYear)
        {
            MovieId = movieId;
            Title = title;
            Language = language;
            Duration = duration;
            ReleaseYear = releaseYear;
        }

    }
}
