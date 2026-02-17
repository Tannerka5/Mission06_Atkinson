using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mission06_Atkinson.Models.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; } = new Movie();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Ratings { get; set; } = new List<SelectListItem>();

        public static List<SelectListItem> GetAllRatings()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "G", Text = "G" },
                new SelectListItem { Value = "PG", Text = "PG" },
                new SelectListItem { Value = "PG-13", Text = "PG-13" },
                new SelectListItem { Value = "R", Text = "R" },
                new SelectListItem { Value = "NR", Text = "NR" },
                new SelectListItem { Value = "UR", Text = "UR" },
                new SelectListItem { Value = "TV-G", Text = "TV-G" },
                new SelectListItem { Value = "TV-PG", Text = "TV-PG" },
                new SelectListItem { Value = "TV-14", Text = "TV-14" },
                new SelectListItem { Value = "TV-Y7", Text = "TV-Y7" }
            };
        }
    }
}
