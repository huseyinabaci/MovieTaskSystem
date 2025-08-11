using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Common.Helper;
using TaskSystem.Common.Model;

namespace TaskSystem.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        private IServiceResponseHelper ServiceResponseHelper => HttpContext.RequestServices.GetService<IServiceResponseHelper>();

        /// <summary>
        /// Errors the specified data.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="isLogging">if set to <c>true</c> [is logging].</param>
        /// <returns>ServiceResponse.</returns>
        protected ServiceResponse<T> Error<T>(T data, string errorMessage, int statusCode = StatusCodes.Status500InternalServerError, bool isLogging = false)
        {
            return ServiceResponseHelper.SetError(data, errorMessage, statusCode, isLogging);
        }

        /// <summary>
        /// Errors the specified error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="isLogging">if set to <c>true</c> [is logging].</param>
        /// <returns>ServiceResponse.</returns>
        protected ServiceResponse Error(string errorMessage, int statusCode = StatusCodes.Status500InternalServerError, bool isLogging = false)
        {
            return ServiceResponseHelper.SetError(errorMessage, statusCode, isLogging);
        }

        /// <summary>
        /// Errors the specified error.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="isLogging">if set to <c>true</c> [is logging].</param>
        /// <returns>ServiceResponse.</returns>
        protected ServiceResponse Error(ErrorInfo error, bool isLogging = false)
        {
            return ServiceResponseHelper.SetError(error, isLogging);
        }

        /// <summary>
        /// Successes this instance.
        /// </summary>
        /// <returns>ServiceResponse.</returns>
        protected ServiceResponse Success()
        {
            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Successes the specified data.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>ServiceResponse.</returns>
        protected ServiceResponse<T> Success<T>(T data)
        {
            return ServiceResponseHelper.SetSuccess(data);
        }
    }
}
