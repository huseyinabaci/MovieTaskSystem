using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSystem.Common.Model
{
    public class ValidationErrorInfo
    {
        public string Message { get; set; }

        public ValidationErrorInfo()
        {
        }

        public ValidationErrorInfo(string message)
        {
            Message = message;
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
                return Equals((ValidationErrorInfo)obj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Message == null)
            {
                return 0;
            }

            return Message.GetHashCode();
        }

        protected bool Equals(ValidationErrorInfo other)
        {
            if (other != null)
            {
                return Message == other.Message;
            }

            return false;
        }
    }

}
