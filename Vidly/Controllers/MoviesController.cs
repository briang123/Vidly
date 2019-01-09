using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // create new movie form
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        // save a movie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        // default movie list
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        // movie details view
        public ActionResult Details(int Id)
        {
            Movie movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            if (movie == null)
            {
                return HttpNotFound($"A movie with an ID of {Id} was not found.");
            }

            return View(movie);
        }

        // edit movie
        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == Id);
            if (movie == null)
            {
                return HttpNotFound($"A movie with the ID of {Id} was not found.");
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);

        }

    }
}