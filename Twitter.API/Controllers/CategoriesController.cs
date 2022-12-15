using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twitter.Core.Entities;
using Twitter.Core.Interfaces;
using Twitter.Infrastructure.Data;

namespace Twitter.API.Controllers
{

    public class CategoriesController : Controller
    {
        private readonly TwitterAPIContext _context;
        private ILoggerManager _logger;

        public CategoriesController(TwitterAPIContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Categories
        [HttpGet("Categories")]
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            //throw new Exception("Exception while fetching all Categories");
            return Ok(categories);
        }

        // GET: Categories/Details/5
        [HttpGet("Categories/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return Ok();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Categories/Create")]
        public async Task<IActionResult> Create(string Name, [Bind("Id,Name")] Category category)
        {
            if (NameExists(Name))
            {
                return BadRequest("Category with the same name already exists!");
            }
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Categories/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: Categories/Delete/5
        [HttpPost("Categories/DeleteById/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return BadRequest("Entity set 'TwitterAPIContext.Category'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPost("Categories/DeleteByName/{Name}")]
        public async Task<IActionResult> DeleteByName(string Name)
        {
            if (_context.Categories == null)
            {
                return BadRequest("Entity set 'TwitterAPIContext.Category'  is null.");
            }
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Name == Name);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        private bool NameExists(string Name)
        {
            return _context.Categories.Any(e => e.Name == Name);
        }
    }
}
