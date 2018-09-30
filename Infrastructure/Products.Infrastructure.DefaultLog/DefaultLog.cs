namespace Products.Infrastructure.DefaultLog
{
    using System;
    using System.Diagnostics;

    using Products.Infrastructure.Core;

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
