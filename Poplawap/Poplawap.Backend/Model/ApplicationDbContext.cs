using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Poplawap.Backend.Model;

namespace Poplawap.Backend.Model
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sales>(s =>
            {
                s.HasMany(e => e.Comments)
                .WithOne(c => c.Sale)
                .HasForeignKey(e => e.SaleId);
            
                s.HasOne(u => u.User)
                .WithMany(c => c.Sales)
                .HasForeignKey(c => c.UserId);

                s.HasOne(s => s.Status)
                .WithMany(ss => ss.Sales)
                .HasForeignKey(s => s.StatusId);

                s.HasOne(s => s.Product)
                .WithOne(p => p.Sale)
                .HasForeignKey<Sales>(s => s.ProductId);

            });

            modelBuilder.Entity<SalesCategories>()
                .HasKey(ca => new { ca.SaleId, ca.CategoryId});

            modelBuilder.Entity<Products>(p =>
            {
                p.HasMany(p => p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);
            });
                
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<ProductImages> ProductImages { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
    }
}
