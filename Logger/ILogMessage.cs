using System;

namespace simbirsoft_test
{
    public interface ILogMessage
    {
        string Message { get; }
        Exception Exception { get; }
    }
}
