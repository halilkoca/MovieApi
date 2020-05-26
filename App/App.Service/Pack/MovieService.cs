using App.Core.Aspects.Caching;
using App.Core.DbTrackers;
using App.Core.Response;
using App.Data.Models;
using App.Entity.Enum;
using App.Entity.Filters;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Service.Pack
{
    public interface IMovieService
    {

        Task<ServiceResponse<Movie>> Get(int id);
        Task<DateTime> Saat();
        Task<ServiceResponse<List<Movie>>> Get(MovieFilter request);
        Task<ServiceResponse<Movie>> Insert(Movie movie);
        ServiceResponse<Movie> Update(Movie Movie);
        ServiceResponse<bool> Delete(int id);
    }

    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepo;
        private readonly IRepository<MovieActor> _movieActorRepo;
        private readonly IRepository<MovieGenre> _movieGenreRepo;
        public MovieService(
            IRepository<Movie> movieRepo, IRepository<MovieActor> movieActorRepo,
        IRepository<MovieGenre> movieGenreRepo
            )
        {
            _movieRepo = movieRepo;
            _movieActorRepo = movieActorRepo;
            _movieGenreRepo = movieGenreRepo;
        }

        [CacheAspect(60)]
        public async Task<ServiceResponse<Movie>> Get(int id)
        {
            var movie = await _movieRepo.Table
                .Include(a => a.Actors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(x => x.Id == id);
            return new ServiceResponse<Movie>(movie, true);
        }


        public async Task<ServiceResponse<List<Movie>>> Get(MovieFilter model)
        {
            ExpressionStarter<Movie> expression = PredicateBuilder.New<Movie>(true);
            if (model.Actors?.Count() > 0)
                expression = expression.And(prop => prop.Actors.Any(a => model.Actors.Contains(a.ActorId)));
            if (model.Genres?.Count() > 0)
                expression = expression.And(prop => prop.Genres.Any(a => model.Genres.Contains(a.GenreId)));
            if (model.PriceRange != null && !model.PriceRange.Equals(""))
                expression = expression.And(prop => prop.Price >= model.PriceMin && prop.Price <= model.PriceMax);

            var responseD = _movieRepo.Pagining(expression, ((model.PageNumber - 1) * model.PageSize), model.PageSize, model.OrderColumn, (OrderDirection)model.OrderBy, true);
            var response = await responseD.ToListAsync();
            return new ServiceResponse<List<Movie>>(response, true);
        }

        [CacheRemoveAspect("GetMovie")]
        public ServiceResponse<Movie> Update(Movie movie)
        {
            var nMovie = _movieRepo.Get(x => x.Id == movie.Id);
            if (nMovie == null)
            {
                return new ServiceResponse<Movie>(false, "Movie doesnt exist");
            }
            _movieRepo.Update(movie);
            return new ServiceResponse<Movie>(movie, true);
        }

        [CacheRemoveAspect("GetMovie")]
        public async Task<ServiceResponse<Movie>> Insert(Movie movie)
        {
            var nMovie = _movieRepo.Table.FirstOrDefault(x => x.Title == movie.Title);
            if (nMovie != null) return new ServiceResponse<Movie>(false, "MovieExist");
            var newId = _movieRepo.Table.Select(x => x.Id).Max() + 1;
            movie.Id = newId;
            await _movieRepo.Insert(movie);

            // add movie actors
            var newIdd = _movieActorRepo.Table.Select(x => x.Id).Max() + 1;
            List<MovieActor> actors = new List<MovieActor>();
            foreach (var item in movie.Actors)
            {
                actors.Add(new MovieActor { Id = newIdd, ActorId = item.ActorId, MovieId = newId });
                ++newIdd;
            }
            await _movieActorRepo.Insert(actors);

            // add movie genres
            var mgId = _movieGenreRepo.Table.Select(x => x.Id).Max() + 1;
            List<MovieGenre> genres = new List<MovieGenre>();
            foreach (var item in movie.Genres)
            {
                genres.Add(new MovieGenre { Id = mgId, GenreId = item.GenreId, MovieId = newId });
                ++mgId;
            }
            await _movieGenreRepo.Insert(genres);

            return new ServiceResponse<Movie>(movie, true);
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var nMovie = _movieRepo.Get(x => x.Id == id);
            if (nMovie == null)
                return new ServiceResponse<bool>(false, "MovieDoesntExist");
            _movieRepo.Delete(nMovie);
            return new ServiceResponse<bool>(true, "Deleted");
        }

        [CacheAspect]
        public async Task<DateTime> Saat()
        {
            return DateTime.Now;
        }
    }
}
