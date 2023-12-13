using AgendaBot.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaBot.Data
{
    public class DbData: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=tgbotdb;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Todos> Todos { get; set; }
    }
}
