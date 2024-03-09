using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Domain.Services.Exceptions
{
    public class InvalidContaException : Exception
    {
        public InvalidContaException(string message) : base(message) { }
    }
}
