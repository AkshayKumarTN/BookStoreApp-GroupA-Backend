﻿using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;

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
        public RegisterModel Login(LoginModel loginData)
        {
            try
            {
                if (loginData != null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Login", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@EmailId", loginData.EmailId);
                    cmd.Parameters.AddWithValue("@Password", EncryptPassWord(loginData.Password));
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    RegisterModel registerModel = new RegisterModel();
                    if (sqlDataReader.Read())
                    {
                        registerModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        registerModel.FullName = sqlDataReader["FullName"].ToString();
                        registerModel.EmailId = sqlDataReader["EmailId"].ToString();
                        registerModel.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                        registerModel.Password = sqlDataReader["Password"].ToString();
                    }
                    if (sqlDataReader.HasRows==false)
                    {
                        throw new Exception("EmailId does not exist");
                    }
                    else if (registerModel.Password!= EncryptPassWord(loginData.Password))
                    {
                        throw new Exception("Password does not match");
                    }
                    registerModel.Password = null;
                    return registerModel;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OTPModel ForgotPassword(string email)
        {
            try
            {
                if(email!=null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[ForgotPassword]", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@EmailId",email);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    OTPModel otpModel = new OTPModel();
                    if(sqlDataReader.Read())
                    {
                        otpModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                        otpModel.EmailId = sqlDataReader["EmailId"].ToString();
                    }
                    var generatedOTP= GenerateRandomOTP();
                    this.SendMSMQ(generatedOTP);
                    if (this.SendMail(email))
                    {
                        otpModel.OTP = generatedOTP;
                        return otpModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private string GenerateRandomOTP()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string OTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            int length = 4;
            for (int i = 0; i < length; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                OTP += sTempChars;
            }
            return OTP;
        }


        private void SendMSMQ(string otp)
        {
            MessageQueue msgqueue;

            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msgqueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message();
            var formatter = new BinaryMessageFormatter();
            message.Formatter = formatter;
            msgqueue.Label = "url Link";
            message.Body = "OTP for Reset Password "+otp;
            msgqueue.Send(message);
        }

        private bool SendMail(string email)
        {
            string emailMessage = this.ReceiveMSMQ();
            if (this.SendMailToUser(email, emailMessage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string ReceiveMSMQ()
        {
            // for reading msmq
            var receivequeue = new MessageQueue(@".\Private$\MyQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();
            string emailMessage = receivemsg.Body.ToString();
            return emailMessage;
        }

        private bool SendMailToUser(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            mailMessage.From = new MailAddress("radhika.shankar1220@gmail.com");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Link to reset your password for BookStore";
            mailMessage.Body = message;
            smtp.EnableSsl = true;
            mailMessage.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("radhika.shankar1220@gmail.com", "kriyanthi");
            smtp.Send(mailMessage);
            return true;
        }
    }
}