using System;
using System.Collections.Generic;
using System.Text;

namespace AE.OperationResultLibrary.Core
{
    public  class OperationResult
    {
        public string ResponseText { get; set; } = string.Empty;
        public ResponseValue Value { get; set; } = ResponseValue.Failed;

        public OperationResult(string message, ResponseValue responseValue)
        {
            Value = responseValue;
            ResponseText = message;
        }

        public OperationResult()
        {
            
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Item1 { get; set; }

        public OperationResult(T data, string message, ResponseValue responseValue) :base (message, responseValue)
        {
            Item1= data;    
        }

        public static OperationResult<T> Success(T data, string message = "Operation completed successfully.")
        {
            return new OperationResult<T>(data, message, ResponseValue.Success);
        }
    }

    public class OperationResult<T1,T2> : OperationResult
    {
        public T1? Item1 { get; set; }

        public T2? Item2 { get; set; }
    }

    public class OperationResult<T1, T2,T3> : OperationResult
    {
        public T1? Item1 { get; set; }

        public T2? Item2 { get; set; }

        public T3? Item3 { get; set; }
    }
}
