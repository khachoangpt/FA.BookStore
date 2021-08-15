using FA.BookStore.Data.Infrastructure.Repositories;
using FA.BookStore.Models.BaseEntities;
using FA.BookStore.Models.Common;
using System;
using System.Threading.Tasks;

namespace FA.BookStore.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        BookStoreContext DataContext { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;

        IGenericRepository<Book> BookRepository { get; }

        IGenericRepository<Category> CategoryRepository { get; }

        IGenericRepository<Author> AuthorRepository { get; }

        IGenericRepository<Review> ReviewRepository { get; }

        IGenericRepository<Publisher> PublisherRepository { get; }
    }
}
