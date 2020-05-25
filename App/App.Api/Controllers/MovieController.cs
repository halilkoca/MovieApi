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

        /// <summary>
        /// Filter Paging Movies
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResponse<List<Movie>>> Get([FromQuery]MovieFilter request)
        {
            return await _movieService.Get(request);
        }

        /// <summary>
        /// Get one movie with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ServiceResponse<Movie>> Get(int id)
        {
            return await _movieService.Get(id);
        }
    }
}
