using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
