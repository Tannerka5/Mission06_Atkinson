namespace Mission06_Atkinson.Models
{
    public class MovieDirector
    {
        public int MovieDirectorId { get; set; }
        public int MovieId { get; set; }
        public int DirectorId { get; set; }

        // Navigation properties
        public Movie? Movie { get; set; }
        public Director? Director { get; set; }
    }
}
