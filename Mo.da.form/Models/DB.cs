using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MO.DA.FORM.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MODAFORM;Username=postgres;Password=pi314159");
        }


        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }

    }
}





