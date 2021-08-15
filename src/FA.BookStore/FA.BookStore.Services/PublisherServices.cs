using FA.BookStore.Data.Infrastructure;
using FA.BookStore.Models.Common;
using FA.BookStore.Services.BaseServices;

namespace FA.BookStore.Services
{
    public class PublisherServices : BaseServices<Publisher>, IPublisherServices
    {
        public PublisherServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
