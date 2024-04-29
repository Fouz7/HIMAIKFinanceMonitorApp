using HIMAIKFinanceMonitorApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace HIMAIKFinanceMonitorApp.Server.Data
{
    public class DataContext(IConfiguration config) : DbContext
    {

        private readonly IConfiguration _config = config;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<IncomeData> IncomeData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }
    }
}
