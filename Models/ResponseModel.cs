// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResponseModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// ----------------------------------------------------------------------------------------------------------
namespace Models
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Class for Response Model
    /// </summary>
    /// <typeparam name="T">Generic can be any data type</typeparam>
    public class ResponseModel<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether Status is True
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Message is Given
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Data is Given
        /// </summary>
        public T Data { get; set; }
    }
}
