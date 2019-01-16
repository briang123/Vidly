using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        public readonly ApplicationDbContext Context;

        public MoviesController()
        {
            Context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            Context.Dispose();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            var dtos = Context.Movies
                .Include(m => m.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(dtos);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            dto.DateAdded = DateTime.Now;
            var movie = Mapper.Map<MovieDto, Movie>(dto);

            Context.Movies.Add(movie);
            Context.SaveChanges();

            dto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), dto);
        }

        // PUT /api/movies/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            dto.DateAdded = movie.DateAdded;
            Mapper.Map(dto, movie);
            Context.SaveChanges();

            return Ok(dto);
        }

        // DELETE /api/movies/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return BadRequest();

            Context.Movies.Remove(movie);
            Context.SaveChanges();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
    }
}
