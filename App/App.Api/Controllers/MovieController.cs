using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Response;
using App.Data.Models;
using App.Entity.Filters;
using App.Service;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;
        public MovieController(
            IMovieService movieService
            )
        {
            _movieService = movieService;
        }

        // GET: api/Movie
        [HttpGet]
        public async Task<ServiceResponse<List<Movie>>> Get(MovieFilter request)
        {
            return await _movieService.Get(request);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ServiceResponse<Movie>> Get(int id)
        {
            return await _movieService.Get(id);
        }


    }
}
