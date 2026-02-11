using Mission06_Atkinson.Models;
using System.ComponentModel.DataAnnotations;

namespace Mission06_Atkinson.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Movie Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start Year is required")]
        [Display(Name = "Year (Start)")]
        [Range(1800, 2100, ErrorMessage = "Please enter a valid year")]
        public int StartYear { get; set; }

        [Display(Name = "Year (End)")]
        [Range(1800, 2100, ErrorMessage = "Please enter a valid year")]
        public int? EndYear { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public string Rating { get; set; } = string.Empty;

        [Display(Name = "Edited Version")]
        public bool Edited { get; set; } = false;

        [Display(Name = "Lent To")]
        public string? LentTo { get; set; }

        [StringLength(25, ErrorMessage = "Notes cannot exceed 25 characters")]
        public string? Notes { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
    }
}
