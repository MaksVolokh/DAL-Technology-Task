using Microsoft.EntityFrameworkCore;
using PschoolAPI.Entities;

namespace PschoolAPI.Data
{
    public class PschoolContext : DbContext
    {
        public PschoolContext(DbContextOptions<PschoolContext> options) : base(options) { }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ParentId);
        }
    }
}
