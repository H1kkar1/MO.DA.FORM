using Microsoft.EntityFrameworkCore;

namespace MO.DA.FORM.Models
{
    class User
    {
            public Guid user_ID { get; set; } = Guid.NewGuid();
            public string name { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string group { get; set; }
    }
    class Post
    {
        public string post_ID { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public DateTime datetime { get; set; }
        public byte[] file { get; set; }

    }

    class Leader
    {
        public int user_ID { get; set; }
    }

    class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=usersdb;Username=postgres;Password=здесь_указывается_пароль_от_postgres");
        }
       

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Psot { get; set; }
        public DbSet<Leader> Leader { get; set; }

        
    }
}
