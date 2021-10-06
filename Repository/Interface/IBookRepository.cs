// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddressRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Interface
{
    using System.Collections.Generic;
    using Models;

    public interface IBookRepository
    {
        /// <summary>
        /// Adds the book.
        /// </summary>
        /// <param name="bookData">The book data.</param>
        /// <returns>Returns true or false</returns>
        bool AddBook(AddBookModel bookData);

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="bookData">The book data.</param>
        /// <returns>Returns true or false</returns>
        bool UpdateBook(AddBookModel bookData);

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <returns>Returns list of book model</returns>
        List<BookModel> GetBooks();

        bool RemoveBook(int bookId);

    }
}
