using FA.BookStore.Data.Infrastructure.Repositories;
using FA.BookStore.Models.BaseEntities;
using FA.BookStore.Models.Common;
using System.Threading.Tasks;

namespace FA.BookStore.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreContext _dbContext;

        public BookStoreContext DataContext => _dbContext;

        public UnitOfWork(BookStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IGenericRepository<Category> _categoryRepository;

        public IGenericRepository<Category> CategoryRepository => _categoryRepository ?? new GenericRepository<Category>(_dbContext);

        private IGenericRepository<Book> _bookRepository;

        public IGenericRepository<Book> BookRepository => _bookRepository ?? new GenericRepository<Book>(_dbContext);

        private IGenericRepository<Author> _authorRepository;

        public IGenericRepository<Author> AuthorRepository => _authorRepository ?? new GenericRepository<Author>(_dbContext);

        private IGenericRepository<Review> _reviewRepository;

        public IGenericRepository<Review> ReviewRepository => _reviewRepository ?? new GenericRepository<Review>(_dbContext);

        private IGenericRepository<Publisher> _publisherRepository;

        public IGenericRepository<Publisher> PublisherRepository => _publisherRepository ?? new GenericRepository<Publisher>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(_dbContext);
        }
    }
}
