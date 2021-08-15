using FA.BookStore.Models.BaseEntities;
using FA.BookStore.Models.Common;
using FA.BookStore.Models.Securiry;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FA.BookStore.Data
{
    public class BookStoreContext : IdentityDbContext<User>
    {
        public BookStoreContext() : base("BookStoreConn")
        {
        }

        static BookStoreContext()
        {
            // Set the database initializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer(new BookStoreInitializer());
        }

        public static BookStoreContext Create()
        {
            return new BookStoreContext();
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasMany(p => p.Authors)
                .WithMany(t => t.Books)
                .Map(pt =>
                {
                    pt.MapLeftKey("BookId");
                    pt.MapRightKey("AuthorId");
                    pt.ToTable("BookAuthorMap", "common");
                });
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            BeforeSaveChanges();
            return await base.SaveChangesAsync();
        }

        private void BeforeSaveChanges()
        {
            var entities = this.ChangeTracker.Entries();
            foreach (var entry in entities)
            {
                if (entry.Entity is IBaseEntity entityBase)
                {
                    var now = DateTime.Now;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entityBase.UpdatedAt = now;
                            break;

                        case EntityState.Added:
                            entityBase.InsertedAt = now;
                            entityBase.UpdatedAt = now;
                            break;
                    }
                }

            }
        }
    }
}
