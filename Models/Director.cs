namespace Mission06_Atkinson.Models
{
    public class Director
    {
        public int DirectorId { get; set; }
        public string DirectorName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
    }
}
