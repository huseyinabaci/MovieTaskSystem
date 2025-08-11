using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSystem.Common.Model;

namespace TaskSystem.Common.Helper
{
    public class ServiceResponseHelper : IServiceResponseHelper, IDisposable
    {
        private readonly ILogger logger;

        private bool isDisposed;

        public ServiceResponseHelper(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<ServiceResponseHelper>();
        }

        ~ServiceResponseHelper()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public ServiceResponse<T> SetError<T>(T data, string errorMessage, int statusCode = 500, bool isLogging = false)
        {
            ErrorInfo errorInfo = new ErrorInfo(statusCode, errorMessage)
            {
                CorrelationId = Guid.NewGuid()
            };
            if (isLogging)
            {
                logger.LogError(errorMessage, errorInfo);
            }

            return new ServiceResponse<T>(data, errorInfo);
        }

        public ServiceResponse SetError(string errorMessage, int statusCode = 500, bool isLogging = false)
        {
            ErrorInfo errorItem = new ErrorInfo(statusCode, errorMessage)
            {
                CorrelationId = Guid.NewGuid()
            };
            return SetError(errorItem, isLogging);
        }

        public ServiceResponse SetError(ErrorInfo errorItem, bool isLogging = false)
        {
            if (errorItem?.CorrelationId == Guid.Empty)
            {
                errorItem.CorrelationId = Guid.NewGuid();
            }

            if (isLogging)
            {
                logger.LogError(errorItem?.Message, errorItem);
            }

            return new ServiceResponse(errorItem);
        }

        public ServiceResponse<T> SetError<T>(T data, ErrorInfo errorInfo, bool isLogging = false)
        {
            if (errorInfo?.CorrelationId == Guid.Empty)
            {
                errorInfo.CorrelationId = Guid.NewGuid();
            }

            if (isLogging)
            {
                logger.LogError(errorInfo?.Message, errorInfo);
            }

            return new ServiceResponse<T>(data, errorInfo);
        }

        public ServiceResponse SetSuccess()
        {
            return new ServiceResponse();
        }

        public ServiceResponse<T> SetSuccess<T>(T data)
        {
            return new ServiceResponse<T>(data);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                isDisposed = true;
                if (disposing && logger is IDisposable disposable)
                {
                    disposable?.Dispose();
                }
            }
        }
    }
}
