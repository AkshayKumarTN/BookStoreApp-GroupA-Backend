// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Repository.Repository
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using Experimental.System.Messaging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using global::Repository.Interface;

    /// <summary>
    /// User repository implements interface
    /// </summary>
    /// <seealso cref="Repository.Interface.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The connection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public UserRepository(IConfiguration configuration)
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
        /// Registers the specified user data.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>
        /// Return model
        /// </returns>
        /// <exception cref="System.Exception">Returns exception message</exception>
        public RegisterModel Register(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    RegisterModel registerModel = new RegisterModel();
                    connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (connection)
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("Registration", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FullName", userData.FullName);
                        cmd.Parameters.AddWithValue("@EmailId", userData.EmailId);
                        cmd.Parameters.AddWithValue("@Password", EncryptPassWord(userData.Password));
                        cmd.Parameters.AddWithValue("@MobileNumber", userData.MobileNumber);
                        var returnedSQLParameter = cmd.Parameters.Add("@result", SqlDbType.Int);
                        returnedSQLParameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var result = (int)returnedSQLParameter.Value;
                        if (result == 1)
                        {
                            return userData;
                        }
                       else if(result==2)
                        {
                            throw new Exception("Email Id already exist");
                        }
                        else if(result==0)
                        {
                            throw new Exception("Some exception has occured");
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

        /// <summary>
        /// Encrypts the pass word.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Return string</returns>
        public string EncryptPassWord(string password)
        {
            var passwordInBytes = Encoding.UTF8.GetBytes(password);
            string encodePassword = Convert.ToBase64String(passwordInBytes);
            return encodePassword;
        }

        /// <summary>
        /// Logins the specified login data.
        /// </summary>
        /// <param name="loginData">The login data.</param>
        /// <returns>Returns model</returns>
        /// <exception cref="System.Exception">
        /// EmailId does not exist
        /// or
        /// Password does not match
        /// or
        /// </exception>
        public RegisterModel Login(LoginModel loginData)
        {
            try
            {
                if (loginData != null)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("AdminLogin", this.connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@EmailId", loginData.EmailId);
                        cmd.Parameters.AddWithValue("@Password", this.EncryptPassWord(loginData.Password));
              
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();
                        RegisterModel registerModel = new RegisterModel();
                            if (sqlDataReader.Read())
                            {
                                registerModel.UserId = Convert.ToInt32(sqlDataReader["AdminId"]);
                                registerModel.EmailId = sqlDataReader["EmailId"].ToString();
                                registerModel.Password = sqlDataReader["Password"].ToString();
                            }
                        if (sqlDataReader.HasRows == false)
                        {
                            this.connection.Close();
                            this.connection.Open();
                            SqlCommand cmds = new SqlCommand("Login", this.connection)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmds.Parameters.AddWithValue("@EmailId", loginData.EmailId);
                            cmds.Parameters.AddWithValue("@Password", this.EncryptPassWord(loginData.Password));

                            sqlDataReader = cmds.ExecuteReader();

                            if (sqlDataReader.Read())
                            {
                                registerModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                                registerModel.FullName = sqlDataReader["FullName"].ToString();
                                registerModel.EmailId = sqlDataReader["EmailId"].ToString();
                                registerModel.MobileNumber = sqlDataReader["MobileNumber"].ToString();
                                registerModel.Password = sqlDataReader["Password"].ToString();
                            }
                        }
                        if (sqlDataReader.HasRows == false)
                        {
                            throw new Exception("EmailId does not exist");
                        }
                     
                        else if (registerModel.Password != this.EncryptPassWord(loginData.Password))
                        {
                           throw new Exception("Password does not match");
                        }

                        return registerModel;
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
                this.connection.Close();
            }
        }

        /// <summary>
        /// Forget the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns model</returns>
        /// <exception cref="System.Exception">Return exception message</exception>
        public OTPModel ForgotPassword(string email)
        {
            try
            {
                if (email != null)
                {
                    this.connection = new SqlConnection(this.Configuration["ConnectionStrings:DbConnection"]);
                    using (this.connection)
                    {
                        this.connection.Open();
                        SqlCommand cmd = new SqlCommand("[dbo].[ForgotPassword]", this.connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("@EmailId", email);
                        SqlDataReader sqlDataReader = cmd.ExecuteReader();
                        OTPModel otpModel = new OTPModel();
                        if (sqlDataReader.Read())
                        {
                            otpModel.UserId = Convert.ToInt32(sqlDataReader["UserId"]);
                            otpModel.EmailId = sqlDataReader["EmailId"].ToString();
                        }

                        var generatedOTP = this.GenerateRandomOTP();
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
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Generates the random string.
        /// </summary>
        /// <returns>Returns string</returns>
        public string GenerateRandomOTP()
        {
            string[] sallowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string otp = string.Empty;
            string stempChars = string.Empty;
            Random rand = new Random();
            int length = 4;
            for (int i = 0; i < length; i++)
            {
                int p = rand.Next(0, sallowedCharacters.Length);
                stempChars = sallowedCharacters[rand.Next(0, sallowedCharacters.Length)];
                otp += stempChars;
            }

            return otp;
        }

        /// <summary>
        /// Sends the MSMQ.
        /// </summary>
        /// <param name="otp">The string.</param>
        public void SendMSMQ(string otp)
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
            message.Body = "OTP for Reset Password " + otp;
            msgqueue.Send(message);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns true or false</returns>
        public bool SendMail(string email)
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

        /// <summary>
        /// Receives the MSMQ.
        /// </summary>
        /// <returns>Return string</returns>
        public string ReceiveMSMQ()
        {
            // for reading msmq
            var receivequeue = new MessageQueue(@".\Private$\MyQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();
            string emailMessage = receivemsg.Body.ToString();
            return emailMessage;
        }

        /// <summary>
        /// Sends the mail to user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="message">The message.</param>
        /// <returns>Returns true or false</returns>
        public bool SendMailToUser(string email, string message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            mailMessage.From = new MailAddress("radhika.shankar1220@gmail.com");
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = "Link to reset your password for BookStore App";
            mailMessage.Body = message;
            smtp.EnableSsl = true;
            mailMessage.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("radhika.shankar1220@gmail.com", "kriyanthi");
            smtp.Send(mailMessage);
            return true;
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetData">The reset data.</param>
        /// <returns>Return true or false</returns>
        /// <exception cref="System.Exception">Return exception message</exception>
        public bool ResetPassword(ResetPasswordModel resetData)
        {
            try
            {
                if (resetData != null)
                {
                    string newPassword = this.EncryptPassWord(resetData.Password);
                    this.connection = new SqlConnection(this.Configuration.GetConnectionString("DbConnection"));
                    using (this.connection)
                    {
                        this.connection.Open();
                            SqlCommand cmd = new SqlCommand("RestPassword", this.connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserId", resetData.UserId);
                            cmd.Parameters.AddWithValue("@Password", newPassword);
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
        }

        /// <summary>
        /// Edits the personal details.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>returns true or false</returns>
        /// <exception cref="System.Exception">
        /// UserId does not exist
        /// or
        /// </exception>
        public bool EditPersonalDetails(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    this.connection = new SqlConnection(this.Configuration.GetConnectionString("DbConnection"));
                    using (this.connection)
                    {
                        this.connection.Open();
                            SqlCommand cmd = new SqlCommand("[dbo].[EditPersonalDetails]", this.connection);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UserId", userData.UserId);
                            cmd.Parameters.AddWithValue("@FullName", userData.FullName);
                            cmd.Parameters.AddWithValue("@EmailId", userData.EmailId);
                            cmd.Parameters.AddWithValue("@Password", this.EncryptPassWord(userData.Password));
                            cmd.Parameters.AddWithValue("@MobileNumber", userData.MobileNumber);
                            SqlDataReader sqlDataReader = cmd.ExecuteReader();
                            RegisterModel registerModel = new RegisterModel();
                            if (sqlDataReader.HasRows == false)
                            {
                                throw new Exception("UserId does not exist");
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
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns string</returns>
        public string GenerateToken(string email)
        {
            byte[] key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}