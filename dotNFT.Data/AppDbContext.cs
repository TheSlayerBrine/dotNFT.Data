using Microsoft.EntityFrameworkCore;
using dotNFT.Data.Entities;

namespace dotNFT.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-NFKHDAA\\SQLEXPRESS01;Initial Catalog=dotNFT;Integrated Security=True;TrustServerCertificate=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Wallet>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.GasFee)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.PricePaidForNFT)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<User>()
               .HasOne(w => w.Wallet)
               .WithOne(u => u.User)
               .HasForeignKey<Wallet>(w => w.UserId);

            modelBuilder.Entity<Wallet>()
            .HasOne(w => w.User)
            .WithOne(u => u.Wallet)
            .HasForeignKey<User>(w => w.WalletId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.UserBuyer)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserBuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NFT>()
                .HasOne(n => n.Wallet)
                 .WithMany(w => w.NFTs)
                .HasForeignKey(n => n.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NFT>()
                .HasOne(n => n.Artist)
                .WithMany(a => a.NFTs)
                .HasForeignKey(n => n.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NFT>()
                .HasOne(n => n.Collection)
                .WithMany(c => c.NFTs)
                .HasForeignKey(n => n.CollectionId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<NFT> NFTs { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}