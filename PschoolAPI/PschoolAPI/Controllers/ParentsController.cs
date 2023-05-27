using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PschoolAPI.Data;
using PschoolAPI.Entities;
using System.Linq;
using System.Numerics;

namespace PschoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentsController : Controller
    {
        private readonly PschoolContext _context;

        public ParentsController(PschoolContext context)
        {
            _context = context;
        }

        // GET: api/parents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parent>>> GetParents()
        {
            return await _context.Parents.ToListAsync();
        }

        //GET: api/parents/firstName
        [HttpGet("searchByQuery")]
        public async Task<ActionResult<List<Parent>>> GetByFirstNameAsync([FromQuery] string firstName)
        {
            if (firstName is not string)
            {
                return BadRequest("First name should be a string!");
            }

            var parent = await _context.Parents.FindAsync(firstName);

            if (parent == null)
            {
                return NotFound("Parent not found!");
            }

            return Ok(parent);
        }

        //GET: api/parents/lastName
        [HttpGet("searchByQuery")]
        public async Task<ActionResult<List<Parent>>> GetByLastNameAsync([FromQuery] string lastName)
        {
            if (lastName is not string)
            {
                return BadRequest("Last name should be a string!");
            }

            var parent = await _context.Parents.FindAsync(lastName);

            if (parent == null)
            {
                return NotFound("Parent not found!");
            }

            return Ok(parent);
        }

        // Post: api/parents
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Parent>>> CreateParents(Parent parent)
        {
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParent", new { id = parent.Id }, parent);
        }

        // PUT: api/parents/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParent(int id, Parent parent)
        {
            if (parent.Id < 0)
            {
                return BadRequest("Id should be positive number!");
            }

            if (id != parent.Id)
            {
                return BadRequest("You have entered the wrong id!");
            }

            if (parent == null)
            {
                return NotFound("Parent is not found!");
            }

            _context.Entry(parent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(parent);
        }

        // DELETE: api/parents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id should be positive number!");
            }

            var parent = await _context.Parents.FindAsync(id);

            if (parent == null)
            {
                return NotFound("Parent is not found!");
            }

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
