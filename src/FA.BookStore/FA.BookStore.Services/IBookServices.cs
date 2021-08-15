using FA.BookStore.Models.Common;
using FA.BookStore.Services.BaseServices;
using System;
using System.Collections.Generic;

namespace FA.BookStore.Services
{
    public interface IBookServices : IBaseService<Book>
    {
        /// <summary>
        /// Find Book By Title
        /// </summary>
        /// <param name="title">title of book</param>
        /// <returns>List of Book</returns>
        List<Book> FindBookByTitle(string title);

        /// <summary>
        /// Find Book By Summary
        /// </summary>
        /// <param name="summary">summary of Book</param>
        /// <returns>List of Book</returns>
        List<Book> FindBookBySummary(string summary);

        /// <summary>
        /// Get Lastest Book
        /// </summary>
        /// <param name="size">number of book want to get</param>
        /// <returns>List of Book</returns>
        List<Book> GetLatestBook(int size);

        /// <summary>
        /// Get Book By Month Create
        /// </summary>
        /// <param name="monthYear">Date</param>
        /// <returns>List of Book</returns>
        List<Book> GetBooksByMonth(DateTime monthYear);

        /// <summary>
        /// Count Book By Category
        /// </summary>
        /// <param name="category">Category Name</param>
        /// <returns>int</returns>
        int CountBooksForCategory(string category);

        /// <summary>
        /// Get Book By Category
        /// </summary>
        /// <param name="category">Category Name</param>
        /// <returns>List of Book</returns>
        List<Book> GetBooksByCategory(string category);

        /// <summary>
        /// Count Book By Publisher
        /// </summary>
        /// <param name="publisher">Publisher Name</param>
        /// <returns>int</returns>
        int CountBooksForPublisher(string publisher);

        /// <summary>
        /// Get Book by Publisher
        /// </summary>
        /// <param name="publisher">Publisher Name</param>
        /// <returns>List of Book</returns>
        List<Book> GetBooksByPublisher(string publisher);
    }
}
