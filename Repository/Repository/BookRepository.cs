﻿using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class BookRepository : IBookRepository
    {
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True";
        SqlConnection connection = new SqlConnection(ConnectionString);
        public BookModel AddBook(BookModel bookData)
        {
            try
            {
                if (bookData != null)
                {
                    BookModel bookModel = new BookModel();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (connection)
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("[dbo].[InsertBookData]", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Title", bookData.Title);
                            cmd.Parameters.AddWithValue("@AuthorName", bookData.AuthorName);
                            cmd.Parameters.AddWithValue("@Price", bookData.Price);
                            cmd.Parameters.AddWithValue("@Rating", bookData.Rating);
                            cmd.Parameters.AddWithValue("@BookDetail", bookData.BookDetail);
                            cmd.Parameters.AddWithValue("@BookImage", bookData.BookImage);
                            cmd.Parameters.AddWithValue("@BookQuantity", bookData.BookQuantity);
                            int result = cmd.ExecuteNonQuery();
                            if (result != 0)
                            {
                                return bookData;
                            }
                            return null;
                        }
                    }
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public BookModel UpdateBook(BookModel bookData)
        {
            try
            {
                if (bookData != null)
                {
                    BookModel bookModel = new BookModel();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (connection)
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("[dbo].[UpdateBookData]", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@BookId", bookData.BookId);
                            cmd.Parameters.AddWithValue("@Title", bookData.Title);
                            cmd.Parameters.AddWithValue("@AuthorName", bookData.AuthorName);
                            cmd.Parameters.AddWithValue("@Price", bookData.Price);
                            cmd.Parameters.AddWithValue("@Rating", bookData.Rating);
                            cmd.Parameters.AddWithValue("@BookDetail", bookData.BookDetail);
                            cmd.Parameters.AddWithValue("@BookImage", bookData.BookImage);
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
                                    book.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                                };

                            }
                            if (sqlDataReader.HasRows == false)
                            {
                                throw new Exception("BookId does not exist");
                            }
                            return book;
                        }
                    }
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public List<BookModel> GetBooks()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetAllBooks", connection)
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
                    bookModel.BookQuantity = Convert.ToInt32(sqlDataReader["BookQuantity"]);
                    bookList.Add(bookModel);
                }
                if (sqlDataReader.HasRows == false)
                {
                    throw new Exception("The book database is empty");
                }
                return bookList;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}