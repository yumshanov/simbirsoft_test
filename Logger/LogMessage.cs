using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simbirsoft_test
{
    public class LogMessage : ILogMessage
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public LogMessage(){}
    }
}
