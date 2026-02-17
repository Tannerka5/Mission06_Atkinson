namespace Mission06_Atkinson.Models.ViewModels
{
    public class MovieListViewModel
    {
        public List<MovieDisplayItem> Movies { get; set; } = new List<MovieDisplayItem>();
        public string? SearchTerm { get; set; }
        public string? CategoryFilter { get; set; }
        public string? RatingFilter { get; set; }
        public string? SortBy { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<string> Ratings { get; set; } = new List<string>();
    }

    public class MovieDisplayItem
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public bool Edited { get; set; }
        public bool CopiedToPlex { get; set; }
        public string? LentTo { get; set; }
        public string? Notes { get; set; }
    }
}
