﻿using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True";



        SqlConnection connection = new SqlConnection(ConnectionString);
        public RegisterModel Register(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    RegisterModel registerModel = new RegisterModel();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (connection)
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand("Registration", connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FullName", userData.FullName);
                            cmd.Parameters.AddWithValue("@EmailId", userData.EmailId);
                            cmd.Parameters.AddWithValue("@Password", EncryptPassWord(userData.Password));
                            cmd.Parameters.AddWithValue("@MobileNumber", userData.MobileNumber);



                            int result = cmd.ExecuteNonQuery();
                            if (result != 0)
                            {
                                return userData;
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
        }



        public string EncryptPassWord(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(passwordInBytes);
            return encodePassword;
        }
    }
}