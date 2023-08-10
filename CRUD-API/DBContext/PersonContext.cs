using CRUD_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.DBContext
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

        public DbSet<Person> People { get; set; } = null!;
    }
}
