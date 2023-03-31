using Microsoft.EntityFrameworkCore;
using dotNFT.Data.Entities;

namespace dotNFT.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnectionString");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistNFT>().HasKey(an => new
            {
                an.ArtistId,
                an.NFTId
            });

            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.GasFee)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.PricePaidForNFT)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<NFT>()
                .HasOne(n => n.Wallet)
                 .WithMany()
                .HasForeignKey(n => n.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NFT>()
                .HasOne(n => n.Artist)
                .WithMany()
                .HasForeignKey(n => n.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NFT>()
                .HasOne(n => n.Collection)
                .WithMany()
                .HasForeignKey(n => n.CollectionId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<NFT> NFTs { get; set; }
        public DbSet<ArtistNFT> ArtistsNFTs { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}