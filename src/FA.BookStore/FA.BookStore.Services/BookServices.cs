using FA.BookStore.Data.Infrastructure;
using FA.BookStore.Models.Common;
using FA.BookStore.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FA.BookStore.Services
{
    public class BookServices : BaseServices<Book>, IBookServices
    {
        public BookServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public int CountBooksForCategory(string category)
        {
            return _unitOfWork.BookRepository.GetQuery().Count(b => b.Category.Name.Equals(category));
        }

        public int CountBooksForPublisher(string publisher)
        {
            return _unitOfWork.BookRepository.GetQuery().Count(b => b.Publisher.Name.Contains(publisher));
        }

        public List<Book> FindBookBySummary(string summary)
        {
            return _unitOfWork.BookRepository.GetQuery().Where(b => b.Summary.Contains(summary)).ToList();
        }

        public List<Book> FindBookByTitle(string title)
        {
            return _unitOfWork.BookRepository.GetQuery().Where(b => b.Title.Contains(title)).ToList();
        }

        public List<Book> GetBooksByCategory(string category)
        {
            return _unitOfWork.BookRepository.GetQuery().Where(b => b.Category.Name.Contains(category)).ToList();
        }

        public List<Book> GetBooksByMonth(DateTime monthYear)
        {
            return _unitOfWork.BookRepository.GetQuery().Where(b => b.CreatedDate.Month == monthYear.Month
                                        && b.CreatedDate.Year == monthYear.Year).ToList();
        }

        public List<Book> GetBooksByPublisher(string publisher)
        {
            return _unitOfWork.BookRepository.GetQuery().Where(b => b.Publisher.Name.Contains(publisher)).ToList();
        }

        public List<Book> GetLatestBook(int size)
        {
            return _unitOfWork.BookRepository.GetQuery().OrderByDescending(b => b.CreatedDate).Take(size).ToList();
        }
    }
}
