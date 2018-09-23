namespace Products.Infrastucture.Core
{
    using System;

    public interface ILog
    {
        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Error(Exception exception);
    }
}
