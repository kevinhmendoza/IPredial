using Core.Entities.Contracts;
using NLog;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Infrastructure.System.Logging
{

    public class Logger : Core.Entities.Contracts.ILogger
    {

        private static NLog.Logger GetInnerLogger(string sourceFilePath)
        {
            var logger = sourceFilePath == null ? LogManager.GetCurrentClassLogger() : LogManager.GetLogger(Path.GetFileName(sourceFilePath));
            return logger;
        }
        public void LogWrite(string message)
        {
            Info(message);
        }

        public void Info(string message, [CallerFilePath] string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Info(message);
        }

        public void Info(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Info(exc, message);
        }

        public void Debug(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Debug(message);
        }

        public void Debug(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Debug(exc, message);
        }

        public void Warn(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Warn(message);
        }

        public void Warn(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Warn(exc, message);
        }

        public void Error(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Error(message);
        }

        public void Error(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Error(exc, message);
        }

        public void Fatal(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Fatal(message);
        }

        public void Fatal(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Fatal(exc, message);
        }

        public void Trace(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Trace(message);
        }

        public void Trace(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Trace(exc, message);
        }
    }
}
