using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    public class MovieController : BaseController
    {
        // GET: api/Movie
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Movie/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }


    }
}
