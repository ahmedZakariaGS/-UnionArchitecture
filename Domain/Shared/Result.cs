 using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Models
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public StatusResult StatusResult { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }


        public Result(bool succeeded, StatusResult statusResult, string? message = null, object? data = null)
        {
            Succeeded = succeeded;
            StatusResult = statusResult;
            Message = message;
            Data = data;
        }

        public static Result Success(string? messages = null, object? data = null)
        {
            return new Result(true, StatusResult.Success, messages, data);
        }

        public static Result Failure(StatusResult statusResult = StatusResult.Falid, string? messages = null, object? data = null)
        {
            return new Result(false, statusResult, messages, data);
        }
    }
}
