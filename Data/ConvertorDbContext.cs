using CurrencyConverter2023.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter2023.Data
{
    public class ConvertorDbContext : DbContext
    {

        

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
       
        public DbSet<UserCurrency> UserCurrencies { get; set; }

        public ConvertorDbContext(DbContextOptions<ConvertorDbContext> options) : base(options) { }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCurrency>()
            .HasKey(uc => new { uc.UserId, uc.CurrencyId });

            modelBuilder.Entity<UserCurrency>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCurrencies)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCurrency>()
                .HasOne(uc => uc.Currency)
                .WithMany(c => c.UserCurrencies)
                .HasForeignKey(uc => uc.CurrencyId);

           

            base.OnModelCreating(modelBuilder);
        }
    }


}
