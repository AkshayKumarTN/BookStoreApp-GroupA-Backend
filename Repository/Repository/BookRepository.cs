// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Models;
    using global::Repository.Interface;

    /// <summary>
    /// Book Repository
    /// </summary>
    /// <seealso cref="Repository.Interface.IBookRepository" />
    public class BookRepository : IBookRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public BookRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adds the book.
        /// </summary>
        /// <param name="bookData">The book data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">'Returns exception</exception>
        public bool AddBook(AddBookModel bookData)
        {
            try
            {
                if (bookData != null)
                {
                    BookModel bookModel = new BookModel();
                    var bookImage = this.AddImage(bookData.BookImage);
                    var bigImage = this.AddImage(bookData.BigImage);
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                            SqlCommand cmd = new SqlCommand("[dbo].[InsertBookData]", this.connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Title", bookData.Title);
                            cmd.Parameters.AddWithValue("@AuthorName", bookData.AuthorName);
                            cmd.Parameters.AddWithValue("@Price", bookData.Price);
                            cmd.Parameters.AddWithValue("@Rating", bookData.Rating);
                            cmd.Parameters.AddWithValue("@BookDetail", bookData.BookDetail);
                            cmd.Parameters.AddWithValue("@BookImage", bookImage);
                        cmd.Parameters.AddWithValue("@BigImage", bigImage);
                        cmd.Parameters.AddWithValue("@BookQuantity", bookData.BookQuantity);
                            int result = cmd.ExecuteNonQuery();
                            if (result != 0)
                            {
                                return true;
                            }

                            return false;
                        }
                    }
                
                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="bookData">The book data.</param>
        /// <returns>
        /// Returns true or false
        /// </returns>
        /// <exception cref="System.Exception">
        /// BookId does not exist
        /// or
        /// </exception>
        public bool UpdateBook(AddBookModel bookData)
        {
            try
            {
                if (bookData != null)
                {
                    BookModel bookModel = new BookModel();
                    var bookImage = this.AddImage(bookData.BookImage);
                    var bigImage = this.AddImage(bookData.BigImage);
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                            SqlCommand cmd = new SqlCommand("[dbo].[UpdateBookData]", this.connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BookId", bookData.BookId);
                            cmd.Parameters.AddWithValue("@Title", bookData.Title);
                            cmd.Parameters.AddWithValue("@AuthorName", bookData.AuthorName);
                            cmd.Parameters.AddWithValue("@Price", bookData.Price);
                            cmd.Parameters.AddWithValue("@Rating", bookData.Rating);
                            cmd.Parameters.AddWithValue("@BookDetail", bookData.BookDetail);
                            cmd.Parameters.AddWithValue("@BookImage", bookImage);
                            cmd.Parameters.AddWithValue("@BigBookImage", bigImage);
                            cmd.Parameters.AddWithValue("@BookQuantity", bookData.BookQuantity);
                            SqlDataReader sqlDataReader = cmd.ExecuteReader();
                            BookModel book = new BookModel();
                            if (sqlDataReader.Read())
                            {
                                {
                                    book.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                                    book.Title = sqlDataReader["Title"].ToString();
                                    book.AuthorName = sqlDataReader["AuthorName"].ToString();
                                    book.Price = Convert.ToInt32(sqlDataReader["Price"]);
                                    book.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                                    book.BookDetail = sqlDataReader["BookDetail"].ToString();
                                    book.BookImage = sqlDataReader["BookImage"].ToString();
                                    book.BigImage = sqlDataReader["BigImage"].ToString();
                                    book.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                                }
                            }

                            if (sqlDataReader.HasRows == false)
                            {
                                throw new Exception("BookId does not exist");
                            }

                            return true;
                        }
                    }
                
                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Gets the books.
        /// </summary>
        /// <returns>
        /// Returns list of book model
        /// </returns>
        /// <exception cref="System.Exception">
        /// The book database is empty
        /// or
        /// </exception>
        public List<BookModel> GetBooks()
        {
            try
            {
                this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                using (this.connection)
                {
                    this.connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllBooks", this.connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    List<BookModel> bookList = new List<BookModel>();
                    while (sqlDataReader.Read())
                    {
                        BookModel bookModel = new BookModel();
                        bookModel.BookId = Convert.ToInt32(sqlDataReader["BookId"]);
                        bookModel.Title = sqlDataReader["Title"].ToString();
                        bookModel.AuthorName = sqlDataReader["AuthorName"].ToString();
                        bookModel.Price = Convert.ToInt32(sqlDataReader["Price"]);
                        bookModel.Rating = Convert.ToInt32(sqlDataReader["Rating"]);
                        bookModel.BookDetail = sqlDataReader["BookDetail"].ToString();
                        bookModel.BookImage = sqlDataReader["BookImage"].ToString();
                        bookModel.BigImage = sqlDataReader["BigImage"].ToString();
                        bookModel.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                        bookList.Add(bookModel);
                    }

                    if (sqlDataReader.HasRows == false)
                    {
                        throw new Exception("The book database is empty");
                    }

                    return bookList;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Remove book 
        /// </summary>
        /// <param name="bookId">passing a bookId as integer</param>
        /// <returns>Returns true or false</returns>
        public bool RemoveBook(int bookId)
        {
            try
            {
                if (bookId != 0)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[RemoveBook]", this.connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookId", bookId);
                        int result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            return true;
                        }

                        return false;
                    }
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>Returns string</returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        private string AddImage(IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(this.Configuration["CloudinaryAccount:CloudName"], this.Configuration["CloudinaryAccount:ApiKey"], this.Configuration["CloudinaryAccount:ApiSecret"]);
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream())
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    var returnImage = uploadResult.Url.ToString();
                    return returnImage;
                }
                else
                {
                    return "update";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
