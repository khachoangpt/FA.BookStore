using FA.BookStore.Data.Infrastructure;
using FA.BookStore.Models.Common;
using FA.BookStore.Services.BaseServices;

namespace FA.BookStore.Services
{
    public class AuthorServices : BaseServices<Author>, IAuthorServices
    {
        public AuthorServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
