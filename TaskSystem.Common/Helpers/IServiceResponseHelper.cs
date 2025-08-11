using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSystem.Common.Model;

namespace TaskSystem.Common.Helper
{
    public interface IServiceResponseHelper
    {
        ServiceResponse SetSuccess();

        ServiceResponse<T> SetSuccess<T>(T data);

        ServiceResponse<T> SetError<T>(T data, string errorMessage, int statusCode = 500, bool isLogging = false);

        ServiceResponse SetError(string errorMessage, int statusCode = 500, bool isLogging = false);

        ServiceResponse SetError(ErrorInfo errorItem, bool isLogging = false);

        ServiceResponse<T> SetError<T>(T data, ErrorInfo errorInfo, bool isLogging = false);
    }
}
