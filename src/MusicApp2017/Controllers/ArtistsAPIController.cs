using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp2017.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApp2017.Controllers
{
    [Route("api/ArtistsAPI")]
    public class ArtistsAPIController : Controller
    {
        private readonly MusicDbContext _context;

        public ArtistsAPIController(MusicDbContext context)
        {
            _context = context;
        }

        // GET: api/ArtistsAPI
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Artists.ToListAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
