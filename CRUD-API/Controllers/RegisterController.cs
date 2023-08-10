using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD_API.DBContext;
using CRUD_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly PersonContext _context;

        public RegisterController(PersonContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet ("GetAllPeople")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            return await _context.People.ToListAsync();
        }

        // GET: api/People
        [HttpGet("GetById")]
        public async Task<ActionResult<Person>> GetPeopleById(int id)
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdatePerson")] //update entry
        public async Task<IActionResult>UpdatePerson(int id, string Name, string Surname)
        {
            var person = await _context.People.FindAsync(id);

            person.Name = Name;
            person.Surname = Surname;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost ("AddNewPerson")]
        public async Task<IActionResult> AddNewPerson(string Name, string Surname)
        {
          if (Name == null || Surname == null)
          {
              return NoContent();
          }
            _context.People.Add(new Person()
            {
                Name = Name,
                Surname = Surname,

                DateTimeStamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            });
            await _context.SaveChangesAsync();

            return Ok(); 
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (_context.People == null)
            {
                return NotFound();
            }
            var item = await _context.People.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.People.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PersonExists(int id)
        {
            return (_context.People?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
