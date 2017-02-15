using System;

namespace ConsoleApp3
{
    public interface IService: IDisposable
    {
        void Initialize();
    }
}