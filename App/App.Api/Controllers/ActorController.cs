using App.Core.Response;
using App.Data.Models;
using App.Entity.Filters;
using App.Service.Pack;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    public class ActorController : BaseController
    {
        private readonly IActorService _actorService;
        public ActorController(
            IActorService actorService
            )
        {
            _actorService = actorService;
        }

        /// <summary>
        /// Filter Paging actors with Get method
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResponse<List<Actor>>> Get([FromQuery]ActorFilter request)
        {
            return await _actorService.Get(request);
        }

        /// <summary>
        /// Get one actor with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ServiceResponse<Actor>> Get(int id)
        {
            return await _actorService.Get(id);
        }
    }
}