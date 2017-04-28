using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicApp2017.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MusicApp2017.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AlbumsController(MusicDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;    
        }

    
        // GET: Albums
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var musicDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre).OrderByDescending(a => a.GenreID == currentUser.GenreID);

                return View(await musicDbContext.ToListAsync());
            }
            else
            {
                var musicDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
                return View(await musicDbContext.ToListAsync());
            }

            //var musicDbContext = _context.Albums.Include(a => a.Artist).Include(a => a.Genre);
            //return View(await musicDbContext.ToListAsync());
        }

        // GET: Albums/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var albumContext = _context.Albums
        //        .Include(a => a.Artist)
        //        .Include(a => a.Genre);
        //    var album = await albumContext
        //        .SingleOrDefaultAsync(m => m.AlbumID == id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(album);
        //}

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return NotFound();
            }

            var albumContext = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre);
            //var album = await albumContext
            //    .SingleOrDefaultAsync(m => m.AlbumID == id);
            var album = await albumContext.Where(m => m.AlbumID == id).ToListAsync();
            if (album == null)
            {
                Response.StatusCode = 404;
                return NotFound();
            }

            return View(album);
        }


        [HttpPost]
        public async Task<IActionResult> Rate(int? id, int albumView)
        {
            var albumContext = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre);
            var album = await albumContext
                .SingleOrDefaultAsync(m => m.AlbumID == id);

            var rating = new Rating { AlbumID = id.Value, Score = albumView.Score };
            _context.Add(rating);


            await _context.SaveChangesAsync();
            albumView = new AlbumViewModel(album);
            albumView.Score = GetAverageAlbumRating(album.AlbumID);
            albumView.Count = GetRatingCount(album.AlbumID);
            album.AvgRating = GetAverageAlbumRating(album.AlbumID);
            _context.Update(album);
            await _context.SaveChangesAsync();

            return View("Details", albumView);
        }

        // GET: Albums/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name");
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("AlbumID,Title,ArtistID,GenreID")] Album album)
        { 
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.SingleOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumID,Title,ArtistID,GenreID,Likes")] Album album)
        {
            if (id != album.AlbumID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "Name", album.ArtistID);
            ViewData["GenreID"] = new SelectList(_context.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.SingleOrDefaultAsync(m => m.AlbumID == id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.AlbumID == id);
        }

        private bool AlbumExists(string title)
        {
            return _context.Albums.Any(e => e.Title == title);
        }
    }
}
