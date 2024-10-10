using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Utilies
{
    public class SapSLPostingValidationException : Exception
    {
        public SapSLPostingValidationException()
        {
        }

        public SapSLPostingValidationException(string message) : base(message)
        {
        }

        public SapSLPostingValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SapSLPostingValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
