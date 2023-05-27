using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PschoolAPI.Data;
using PschoolAPI.Entities;

namespace PschoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly PschoolContext _context;

        public StudentsController(PschoolContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        //GET: api/students/firstName
        [HttpGet("searchByQuery")]
        public async Task<ActionResult<List<Student>>> GetByFirstNameAsync([FromQuery] string firstName)
        {
            if (firstName is not string)
            {
                return BadRequest("First name should be a string!");
            }

            var student = await _context.Students.FindAsync(firstName);

            if (student == null)
            {
                return NotFound("Student not found!");
            }

            return Ok(student);
        }

        //GET: api/students/lastName
        [HttpGet("searchByQuery")]
        public async Task<ActionResult<List<Student>>> GetByLastNameAsync([FromQuery] string lastName)
        {
            if (lastName is not string)
            {
                return BadRequest("Last name should be a string!");
            }

            var student = await _context.Students.FindAsync(lastName);

            if (student == null)
            {
                return NotFound("Student not found!");
            }

            return Ok(student);
        }

        // Post: api/students
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Student>>> CreateStudents(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (student.Id < 0)
            {
                return BadRequest("Id should be positive number!");
            }

            if (id != student.Id)
            {
                return BadRequest("You have entered the wrong id!");
            }

            if (student == null)
            {
                return NotFound("Student is not found!");
            }

            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        // DELETE: api/students/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id should be positive number!");
            }

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound("Student is not found!");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
