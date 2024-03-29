﻿using System.Net;

namespace WhatsInt.Infrastructure.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public AppException(HttpStatusCode statusCode, string? message = null) : base(message) { StatusCode = statusCode; }
    }
}
