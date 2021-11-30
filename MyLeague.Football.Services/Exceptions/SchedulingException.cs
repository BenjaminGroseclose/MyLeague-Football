using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLeague.Football.Services.Exceptions
{
    public class SchedulingException : Exception
    {
        public SchedulingException(string? message) : base(message)
        {
        }
    }
}
