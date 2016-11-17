using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePointSecurity.Common.Exceptions
{
    public class SafePointStringConnectionException : Exception
    {

        public SafePointStringConnectionException() { }

        public SafePointStringConnectionException(string message) : base(message) { }

        public SafePointStringConnectionException(string message, Exception innerException) : base(message, innerException) { }

    }
}
