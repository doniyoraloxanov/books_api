using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Repository.Entities;
//using Data_Acess_Layer;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Repository
{
 
        public partial class BookDbContext : DbContext
        {
            public BookDbContext()
            {
            }

            public BookDbContext(DbContextOptions<BookDbContext> options)
                : base(options)
            {
            }

            public virtual DbSet<Book> Books { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("Data Source=LAPTOP-83VDM6D4;Initial Catalog=BooksDatabase;Integrated Security=True;Trust Server Certificate=True");
                }
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
    
            modelBuilder.Entity<Book>().HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Book>(entity =>
                {
                    entity.Property(e => e.Title).IsUnicode(false);

                    entity.Property(e => e.PublicationYear).IsUnicode(false);

                    entity.Property(e => e.AuthorName).IsUnicode(false);

                    entity.Property(e => e.ViewCount).IsUnicode(false);
                });

                OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }
    
}
 