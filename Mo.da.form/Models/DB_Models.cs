using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MO.DA.FORM.Models
{
    public class User
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string group { get; set; }
        public bool leader { get; set; }
    }
    public class Post
    {
        [Key]
        public int post_id { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public DateTime datetime { get; set; }
        public byte[] file { get; set; }
    }

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MODAFORM;Username=postgres;Password=pi314159");
        }


        public DbSet<User> User { get; set; }
        public DbSet<Post> Psot { get; set; }

    }
}

