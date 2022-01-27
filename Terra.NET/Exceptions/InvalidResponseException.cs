using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terra.NET.Exceptions
{
    internal class InvalidResponseException : TerraApiException
    {
        public InvalidResponseException(string rawResponse)
        {
            this.RawResponse = rawResponse;
        }

        public string RawResponse { get; }
    }
}
