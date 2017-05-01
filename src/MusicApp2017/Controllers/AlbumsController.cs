using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicApp2017.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MusicApp2017.Models.AlbumViewModels;

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
        [HttpGet]
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

        }

        // GET: Albums/Details/{AlbumID}
        [HttpGet]
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

            var album = await albumContext.Where(m => m.AlbumID == id).SingleOrDefaultAsync();

            if (album == null)
            {
                Response.StatusCode = 404;
                return NotFound();
            }

            AlbumViewModel AVM = new AlbumViewModel();

            AVM.Album = album;

            return View(AVM);
        }

        // GET: Albums/Create
        [HttpGet]
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

        // GET: Albums/Edit/{AlbumID}
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

        // POST: Albums/Edit/{AlbumID}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumID,Title,ArtistID,GenreID")] Album album)
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

        /*
         * Rate must update the album averageRating.
         * 
         * 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Rate(AlbumViewModel AVM)
        {

            //If the AlbumViewModel is null or the rating input values are out of range return not found.
            if (AVM.Album == null || AVM.RatingValue <= 0 || AVM.RatingValue >= 6)
            {
                return NotFound();
            }

            //TODO Attach the user ID to the rating & stop users from rating an album twice.

            //Create the new rating, add it to the database, and save the changes.
            var rating = new Rating { AlbumID = AVM.Album.AlbumID, RatingValue = AVM.RatingValue };
            _context.Add(rating);
            await _context.SaveChangesAsync();

            /**
             * Get the list of ratings for the selected album.
             * Find the sum of the ratings.
             * Divide the sum by the amount of ratings to get the average rating.
             * Update the albums average rating.
             * 
             */
            var ratingList = await _context.Ratings.Where(m => m.AlbumID == AVM.Album.AlbumID).ToListAsync();

            var ratingArray = ratingList.ToArray();

            decimal ratingSum = 0;

            for (int i = 0; i < ratingArray.Length; i++)
            {
                ratingSum = ratingSum + ratingArray[i].RatingValue;
            }

            decimal calculateAverageRating = ratingSum / ratingList.Count;

            //Save changes made to the album to the database.
            var album = await _context.Albums.Where(m => m.AlbumID == AVM.Album.AlbumID).SingleOrDefaultAsync();
            album.AverageRating = calculateAverageRating;
            _context.Update(album);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
            //return View("Details", AVM.Album.AlbumID);
        }

        // GET: Albums/Delete/{AlbumID}
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

        // POST: Albums/Delete/{AlbumID}
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
