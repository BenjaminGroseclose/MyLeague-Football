using System;

namespace MyLeague.Football.Services.Exceptions
{
    public class SchedulingException : Exception
    {
        public SchedulingException(string? message) : base(message)
        {
        }
    }
}
