using System;

namespace FiveGears.Core.ExceptionHandling.Models
{
    public class ErrorDetails
    {
        public string Title { get; set; }
        public bool IsErrorKnown { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }

    }
}
