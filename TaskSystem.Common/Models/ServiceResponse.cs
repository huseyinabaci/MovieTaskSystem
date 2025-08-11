using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSystem.Common.Model
{
    public class ServiceResponse
    {
        public ErrorInfo Error { get; set; }

        public bool IsSuccessful { get; set; }

        public ServiceResponse(bool isSuccessful = true)
        {
            IsSuccessful = isSuccessful;
        }

        public ServiceResponse(ErrorInfo error, bool isSuccessful = false)
        {
            Error = error;
            IsSuccessful = isSuccessful;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            if (obj.GetType() == GetType())
            {
                return Equals((ServiceResponse)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsSuccessful, Error);
        }

        protected bool Equals(ServiceResponse other)
        {
            if (IsSuccessful == other?.IsSuccessful)
            {
                return Equals(Error, other?.Error);
            }

            return false;
        }
    }
    public sealed class ServiceResponse<TResult> : ServiceResponse
    {
        public TResult Result { get; set; }

        public ServiceResponse(TResult result, bool isSuccessful = true)
            : base(isSuccessful)
        {
            Result = result;
        }

        public ServiceResponse(TResult result, ErrorInfo error, bool isSuccessful = false)
            : base(error, isSuccessful)
        {
            Result = result;
        }

        public override bool Equals(object obj)
        {
            if (this != obj)
            {
                if (obj is ServiceResponse<TResult> other)
                {
                    return Equals(other);
                }

                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TResult>.Default.GetHashCode(Result);
        }

        private bool Equals(ServiceResponse<TResult> other)
        {
            return EqualityComparer<TResult>.Default.Equals(Result, other.Result);
        }
    }
}
