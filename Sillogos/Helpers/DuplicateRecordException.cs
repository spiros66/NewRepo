using System;

namespace Sillogos.Helpers;

public class DuplicateRecordException : Exception
{
    public DuplicateRecordException() { }

    public DuplicateRecordException(string message)
        : base(message) { }

    public DuplicateRecordException(string message, Exception innerException)
        : base(message, innerException) { }
}