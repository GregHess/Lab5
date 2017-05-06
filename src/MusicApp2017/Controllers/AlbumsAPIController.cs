using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp2017.Models;
using Microsoft.AspNetCore.Identity;

namespace MusicApp2017.Controllers
{
    [Produces("application/json")]
    [Route("api/AlbumsAPI")]
    public class AlbumsAPIController : Controller
    {
        private readonly MusicDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AlbumsAPIController(MusicDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/AlbumsAPI
        [HttpGet]
        public async Task<IActionResult> GetAlbumsAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var musicDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).OrderByDescending(a => a.GenreID == currentUser.GenreID);

                return Ok(await musicDbContext.ToListAsync());
            }
            else
            {
                var musicDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
                return Ok(await musicDbContext.ToListAsync());
            }
        }

        // GET: api/AlbumsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var albumContext = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre);

            var album = await albumContext.Where(m => m.AlbumID == id).SingleOrDefaultAsync();

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        // PUT: api/AlbumsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum([FromRoute] int id, [FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != album.AlbumID)
            {
                return BadRequest();
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AlbumsAPI
        [HttpPost]
        public async Task<IActionResult> PostAlbum([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlbum", new { id = album.AlbumID }, album);
        }

        // DELETE: api/AlbumsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var album = await _context.Albums.SingleOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return Ok(album);
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.AlbumID == id);
        }
    }
}