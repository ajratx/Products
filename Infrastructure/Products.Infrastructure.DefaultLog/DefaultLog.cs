namespace Products.Infrastructure.DefaultLogger
{
    using System;
    using System.Diagnostics;

    using Products.Infrastucture.Core;

    public class DefaultLog : ILog
    {
        public void Error(string message)
        {
            Debug.WriteLine($"Error: {message}");
        }

        public void Error(Exception exception)
        {
            Debug.WriteLine($"Error: {exception}");
        }

        public void Info(string message)
        {
            Debug.WriteLine($"Info: {message}");
        }

        public void Warn(string message)
        {
            Debug.WriteLine($"Warn: {message}");
        }
    }
}
