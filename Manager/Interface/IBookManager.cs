// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBookManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Manager.Interface
{
    using System.Collections.Generic;
    using Models;

    /// <summary>
    /// Interface for manager function
    /// </summary>
    public interface IBookManager
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

        /// <summary>
        /// Remove Book 
        /// </summary>
        /// <param name="bookId">passing a bookId</param>
        /// <returns>Returns true or false</returns>
        bool RemoveBook(int bookId);
    }
}
