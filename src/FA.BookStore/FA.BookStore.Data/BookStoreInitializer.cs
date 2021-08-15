using FA.BookStore.Models.Common;
using FA.BookStore.Models.Securiry;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FA.BookStore.Data
{
    class BookStoreInitializer : DropCreateDatabaseIfModelChanges<BookStoreContext>
    {
        protected override void Seed(BookStoreContext context)
        {
            InitializeIdentity(context);

            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category 01",
                    Description = "Description 01"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category 02",
                    Description = "Description 02"
                },
                new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Category 03",
                    Description = "Description 03"
                },
            };

            var publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    Id = Guid.NewGuid(),
                    Name = "Publisher 01",
                    Description = "Description 01"
                },
                new Publisher()
                {
                    Id = Guid.NewGuid(),
                    Name = "Publisher 02",
                    Description = "Description 02"
                },
                new Publisher()
                {
                    Id = Guid.NewGuid(),
                    Name = "Publisher 03",
                    Description = "Description 03"
                },
            };

            var author1 = new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Author 01",
                Description = "Description 01"
            };
            var author2 = new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Author 02",
                Description = "Description 02"
            };
            var author3 = new Author()
            {
                Id = Guid.NewGuid(),
                Name = "Author 03",
                Description = "Description 03"
            };

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Title 01",
                    Summary = "Summary 01",
                    ImgUrl = "Img Url 01",
                    UnitPrice = 100M,
                    Quantity = 12,
                    CreatedDate = new DateTime(2020,02,03),
                    UpdatedDate = new DateTime(2020,05,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[0].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[0].Name),
                    Authors = new List<Author>{ author1, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Title 02",
                    Summary = "Summary 02",
                    ImgUrl = "Img Url 02",
                    UnitPrice = 115M,
                    Quantity = 25,
                    CreatedDate = new DateTime(2020,04,15),
                    UpdatedDate = new DateTime(2020,05,22),
                    Published = false,
                    Category = categories.Single(x => x.Name == categories[1].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[1].Name),
                    Authors = new List<Author>{ author2, author3 }
                },
                new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = "Title 03",
                    Summary = "Summary 03",
                    ImgUrl = "Img Url 03",
                    UnitPrice = 111M,
                    Quantity = 44,
                    CreatedDate = new DateTime(2020,10,20),
                    UpdatedDate = new DateTime(2021,01,12),
                    Published = true,
                    Category = categories.Single(x => x.Name == categories[2].Name),
                    Publisher = publishers.Single(x => x.Name == publishers[2].Name),
                    Authors = new List<Author>{ author1, author2, author3 }
                },
            };

            var Reviews = new List<Review>()
            {
                new Review()
                {
                    Id = Guid.NewGuid(),
                    Book = books.Single(x => x.Title == books[0].Title),
                    Content = "Content 01",
                    CreatedDate = new DateTime(2021,05,10)
                },
                new Review()
                {
                    Id = Guid.NewGuid(),
                    Book = books.Single(x => x.Title == books[1].Title),
                    Content = "Content 02",
                    CreatedDate = new DateTime(2021,05,10)
                },
                new Review()
                {
                    Id = Guid.NewGuid(),
                    Book = books.Single(x => x.Title == books[2].Title),
                    Content = "Content 03",
                    CreatedDate = new DateTime(2021,05,10)
                },
                new Review()
                {
                    Id = Guid.NewGuid(),
                    Book = books.Single(x => x.Title == books[0].Title),
                    Content = "Content 04",
                    CreatedDate = new DateTime(2021,05,10)
                }
            };

            context.Categories.AddRange(categories);
            context.Publishers.AddRange(publishers);
            context.Books.AddRange(books);
            context.Reviews.AddRange(Reviews);
            context.SaveChanges();
        }

        public static void InitializeIdentity(BookStoreContext db)
        {
            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleResult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new User { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}
