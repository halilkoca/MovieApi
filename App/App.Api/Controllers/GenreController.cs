using App.Core.Response;
using App.Data.Models;
using App.Entity.Filters;
using App.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;
        public GenreController(
            IGenreService genreService
            )
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Filter Paging Genres with Get method
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResponse<List<Genre>>> Get([FromQuery]GenreFilter request)
        {
            return await _genreService.Get(request);
        }

        /// <summary>
        /// Get one genre with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ServiceResponse<Genre>> Get(int id)
        {
            return await _genreService.Get(id);
        }
    }
}