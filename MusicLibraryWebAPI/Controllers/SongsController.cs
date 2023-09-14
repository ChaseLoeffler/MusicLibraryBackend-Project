using Microsoft.AspNetCore.Mvc;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicLibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public IActionResult Get()
        {
            var songs = _context.Songs.ToList();
            return Ok(songs);
        }

        // GET api/Songs/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var song = _context.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }

        // POST api/Songs
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {
            _context.Songs.Add(song);
            _context.SaveChanges();
            return StatusCode(201, song);
        }

        // PUT api/Songs/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
           var songU = _context.Songs.FirstOrDefault(s=>s.Id==id);
            if (songU == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                songU.Artist = song.Artist;
                songU.Album = song.Album;
                songU.Genre = song.Genre;
                songU.Title = song.Title;
                songU.ReleaseDate = song.ReleaseDate;
                
                _context.SaveChanges();

                return StatusCode(200,songU);

            }
            return BadRequest(ModelState);
        }

        // DELETE api/Songs/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var song = _context.Songs.Find(id);
            if (song == null)
            {
                return NotFound();
            }
            _context.Songs.Remove(song);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
