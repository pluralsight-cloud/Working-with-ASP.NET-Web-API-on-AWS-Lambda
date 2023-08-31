using Microsoft.EntityFrameworkCore;

namespace WebApiRdsApp.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Student>? Students { get; set; }
    }
}