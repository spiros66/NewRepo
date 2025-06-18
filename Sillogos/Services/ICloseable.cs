using System;

namespace Sillogos.Services
{
    public interface ICloseable
    {
        event EventHandler RequestClose;
    }
}