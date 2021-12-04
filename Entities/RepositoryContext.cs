using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
              .HasMany<Author>(b => b.Authors)
              .WithMany(a => a.Books)
              .UsingEntity(x => x.ToTable("BookAuthor"));

            modelBuilder.Entity<Book>().HasKey(b => b.Id).HasName("BookId");
            modelBuilder.Entity<Book>().Property(b => b.Id).HasColumnName("BookId");

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.Year)
                .IsRequired();

            modelBuilder.Entity<Author>().HasKey(a => a.AuthorId);
            modelBuilder.Entity<Author>().Property(a => a.AuthorId).HasColumnName("AuthorId");
            modelBuilder.Entity<Author>()
                .Property(a => a.Surname)
                .IsRequired();
        }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
