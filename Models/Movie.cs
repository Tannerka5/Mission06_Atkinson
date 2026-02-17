using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission06_Atkinson.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required")]
        [Range(1888, 2100, ErrorMessage = "Year must be between 1888 and 2100")]
        public int Year { get; set; }

        public string? Director { get; set; }

        public string? Rating { get; set; }

        [Required(ErrorMessage = "Edited field is required")]
        public int Edited { get; set; }

        public string? LentTo { get; set; }

        [Required(ErrorMessage = "Copied to Plex field is required")]
        [Display(Name = "Copied to Plex")]
        public int CopiedToPlex { get; set; }

        public string? Notes { get; set; }

        // Navigation property
        public Category? Category { get; set; }

        // Helper properties for checkboxes (not mapped to database)
        [NotMapped]
        public bool EditedCheckbox
        {
            get => Edited == 1;
            set => Edited = value ? 1 : 0;
        }

        [NotMapped]
        [Display(Name = "Copied to Plex")]
        public bool CopiedToPlexCheckbox
        {
            get => CopiedToPlex == 1;
            set => CopiedToPlex = value ? 1 : 0;
        }
    }
}
