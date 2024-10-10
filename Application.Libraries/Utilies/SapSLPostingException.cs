using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Utilies
{
    public class SapSLPostingException : Exception
    {
        public SapSLPostingException()
        {
        }

        public SapSLPostingException(string message) : base(message)
        {
        }

        public SapSLPostingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SapSLPostingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
