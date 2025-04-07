// task11/exception/BookingSystemException.cs
// ========================
using System;

namespace task11.exception
{
    public class BookingSystemException : Exception
    {
        public BookingSystemException(string message) : base(message) { }
    }
}
