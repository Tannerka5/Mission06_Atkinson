namespace Mission06_Atkinson.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
