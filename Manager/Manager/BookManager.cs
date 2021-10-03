// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using global::Manager.Interface;
    using Models;
    using Repository.Interface;

    /// <summary>
    /// Implements interface
    /// </summary>
    public class BookManager : IBookManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IBookRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BookManager(IBookRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Adds the book.
        /// </summary>
        /// <param name="bookData">The book data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool AddBook(AddBookModel bookData)
        {
            try
            {
                return this.repository.AddBook(bookData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="bookData">The book data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public bool UpdateBook(AddBookModel bookData)
        {
            try
            {
                return this.repository.UpdateBook(bookData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <returns>
        /// Returns list of book model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public List<BookModel> GetBooks()
        {
            try
            {
                return this.repository.GetBooks();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
