using System;
using System.Collections.Generic;

namespace App.Core.Response
{
    public interface IResponse<T>
    {
        IEnumerable<ErrorModel> Errors { get; set; }
        bool IsValid { get; set; }
        string Title { get; set; }
        string Message { get; set; }
        int Status { get; set; }
        T Data { get; set; }
    }

    public class ServiceResponse<T> : IResponse<T>
    {
        public ServiceResponse()
        {
            Data = (T)Activator.CreateInstance(typeof(T));
        }

        public ServiceResponse(bool isValid)
        {
            IsValid = isValid;
        }

        public ServiceResponse(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public ServiceResponse(T data, bool isValid)
        {
            IsValid = isValid;
            Data = data;
        }

        public ServiceResponse(T data, bool isValid, string message)
        {
            IsValid = isValid;
            Data = data;
            Message = message;
        }

        public ServiceResponse(ErrorModel error, int status, string title)
        {
            Errors = new List<ErrorModel> { error };
            Status = status;
            Title = title;
        }

        public ServiceResponse(IEnumerable<ErrorModel> errors, int status, string title)
        {
            Errors = errors;
            Status = status;
            Title = title;
        }

        public ServiceResponse(T data, int status, string title, bool isValid, string message)
        {
            Data = data;
            Status = status;
            Title = title;
            IsValid = isValid;
            Message = message;
        }


        public IEnumerable<ErrorModel> Errors { get; set; }
        public bool IsValid { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }
    }

    public class ErrorModel
    {
        public ErrorModel(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }
}
