using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySql.Data.MySqlClient;


namespace ServerSide.DataBaseORM
{
    public class Context : DbContext
    {
        public DbSet<Accounts> Accounts { get; set; }
        public Context() => Database.EnsureCreated();
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                Database = "SooliksRP",
                Port = 3306,
                UserID = "SooliksRP",
                Password = "-",
            };
            optionsBuilder.UseMySQL(connectionString.ConnectionString)
                .LogTo(str => Debug.WriteLine(str), new[] { RelationalEventId.CommandExecuted })
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountsConfig());
        }
    }
}