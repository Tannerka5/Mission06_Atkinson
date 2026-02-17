using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission06_Atkinson.Data;
using Mission06_Atkinson.Models;
using Mission06_Atkinson.Models.ViewModels;

namespace Mission06_Atkinson.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieCollectionContext _context;

        public MoviesController(MovieCollectionContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchTerm, string categoryFilter, string ratingFilter, string sortBy)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Category)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                moviesQuery = moviesQuery.Where(m =>
                    m.Title.Contains(searchTerm) ||
                    (m.Director != null && m.Director.Contains(searchTerm)));
            }

            // Apply category filter
            if (!string.IsNullOrWhiteSpace(categoryFilter))
            {
                moviesQuery = moviesQuery.Where(m => m.Category!.CategoryName == categoryFilter);
            }

            // Apply rating filter
            if (!string.IsNullOrWhiteSpace(ratingFilter))
            {
                moviesQuery = moviesQuery.Where(m => m.Rating == ratingFilter);
            }

            // Apply sorting
            moviesQuery = sortBy switch
            {
                "title_desc" => moviesQuery.OrderByDescending(m => m.Title),
                "year" => moviesQuery.OrderBy(m => m.Year),
                "year_desc" => moviesQuery.OrderByDescending(m => m.Year),
                "category" => moviesQuery.OrderBy(m => m.Category!.CategoryName),
                "rating" => moviesQuery.OrderBy(m => m.Rating),
                _ => moviesQuery.OrderBy(m => m.Title)
            };

            var movies = await moviesQuery.ToListAsync();

            var viewModel = new MovieListViewModel
            {
                Movies = movies.Select(m => new MovieDisplayItem
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    Category = m.Category?.CategoryName ?? "",
                    Year = m.Year.ToString(),
                    Director = m.Director ?? "",
                    Rating = m.Rating ?? "",
                    Edited = m.Edited == 1,
                    CopiedToPlex = m.CopiedToPlex == 1,
                    LentTo = m.LentTo,
                    Notes = m.Notes
                }).ToList(),
                SearchTerm = searchTerm,
                CategoryFilter = categoryFilter,
                RatingFilter = ratingFilter,
                SortBy = sortBy,
                Categories = await _context.Categories.ToListAsync(),
                Ratings = await _context.Movies
                    .Where(m => m.Rating != null)
                    .Select(m => m.Rating!)
                    .Distinct()
                    .OrderBy(r => r)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new MovieFormViewModel
            {
                Categories = await _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    }).ToListAsync(),
                Ratings = MovieFormViewModel.GetAllRatings()
            };

            return View(viewModel);
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(viewModel.Movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns if validation fails
            viewModel.Categories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToListAsync();
            viewModel.Ratings = MovieFormViewModel.GetAllRatings();

            return View(viewModel);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Categories = await _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    }).ToListAsync(),
                Ratings = MovieFormViewModel.GetAllRatings()
            };

            return View(viewModel);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieFormViewModel viewModel)
        {
            if (id != viewModel.Movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(viewModel.Movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns if validation fails
            viewModel.Categories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToListAsync();
            viewModel.Ratings = MovieFormViewModel.GetAllRatings();

            return View(viewModel);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            var displayItem = new MovieDisplayItem
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Category = movie.Category?.CategoryName ?? "",
                Year = movie.Year.ToString(),
                Director = movie.Director ?? "",
                Rating = movie.Rating ?? "",
                Edited = movie.Edited == 1,
                CopiedToPlex = movie.CopiedToPlex == 1,
                LentTo = movie.LentTo,
                Notes = movie.Notes
            };

            return View(displayItem);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
