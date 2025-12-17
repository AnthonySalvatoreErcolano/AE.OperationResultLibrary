using System;
using System.Collections.Generic;
using System.Text;

namespace AE.OperationResultLibrary.Core
{
    public class OperationResult
    {
        public string ResponseText { get; set; } = string.Empty;
        public ResponseValue Value { get; set; } = ResponseValue.Failed;

        public bool IsSuccess => Value.IsSuccessCode;    
        public bool isFailure => !IsSuccess;
        protected bool IsObjectEmpty(object? item)
        {
            if (item is null) return true;

            if (item is System.Collections.IEnumerable collection)
            {
                var enumerator = collection.GetEnumerator();
                try
                {
                    return !enumerator.MoveNext();
                }
                finally
                {
                    if (enumerator is IDisposable disposable) disposable.Dispose();
                }
            }
            return false;
        }
        public OperationResult(string message, ResponseValue responseValue)
        {
            Value = responseValue;
            ResponseText = message;
        }

        public OperationResult()
        {
            
        }
    }

    public class OperationResult<T>(T item1, string message, ResponseValue responseValue) : OperationResult(message, responseValue)
    {
        public T? Item1 { get; set; } = item1;

        public bool IsNullOrEmpty => IsSuccess && IsObjectEmpty(Item1);     
    }

    public class OperationResult<T1,T2>(T1? item1, T2? item2, string message, ResponseValue responseValue)
    : OperationResult(message,responseValue )
    {
        public T1? Item1 { get; set; }=item1;

        public T2? Item2 { get; set; }=item2;
        public bool IsNullOrEmpty => IsSuccess && IsObjectEmpty(Item1) && IsObjectEmpty(Item2);
        public bool IsItem1NullOrEmpty => IsSuccess && IsObjectEmpty(Item1);
        public bool IsItem2NullOrEmpty => IsSuccess && IsObjectEmpty(Item2);
      
    }

    public class OperationResult<T1, T2,T3>(T1 item1, T2 item2, T3 item3, string message, ResponseValue responseValue) : OperationResult(message, responseValue)
    {
        public T1? Item1 { get; set; } = item1;

        public T2? Item2 { get; set; } = item2;
        public T3? Item3 { get; set; } = item3;
        public bool IsNullOrEmpty => IsSuccess && IsObjectEmpty(Item1) && IsObjectEmpty(Item2) && IsObjectEmpty(Item3);
        public bool IsItem1NullOrEmpty => IsSuccess && IsObjectEmpty(Item1);
        public bool IsItem2NullOrEmpty => IsSuccess && IsObjectEmpty(Item2); 
        public bool IsItem3NullOrEmpty => IsSuccess && IsObjectEmpty(Item3);
      
    }
}
