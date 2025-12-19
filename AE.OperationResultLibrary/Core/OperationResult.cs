using System;
using System.Collections.Generic;
using System.Text;

namespace AE.OperationResultLibrary.Core
{
    /// <summary>
    /// Represents the result of an operation, including its outcome and an associated response message.
    /// </summary>
    /// <remarks>Use this class to convey both the status and details of an operation's execution. The outcome
    /// is indicated by the <see cref="Value"/> property, while <see cref="ResponseText"/> provides additional context
    /// or error information. The <see cref="IsSuccess"/> and <see cref="isFailure"/> properties offer convenient checks
    /// for success or failure states.</remarks>
    public class OperationResult
    {
        public string ResponseText { get; set; } = string.Empty;
        public ResponseValue Value { get; set; } = ResponseValue.Failed;

        public bool IsSuccess => Value.IsSuccessCode;    
        public bool isFailure => !IsSuccess;


       /// <summary>
       /// Determines whether the specified object is null or an empty collection.
       /// </summary>
       /// <remarks>If the object is a collection, this method checks whether it contains any elements by
       /// attempting to enumerate it. For non-collection objects, only null is considered empty.</remarks>
       /// <param name="item">The object to evaluate. If the object implements <see cref="System.Collections.IEnumerable"/>, its contents
       /// are checked; otherwise, only null is considered empty.</param>
       /// <returns>true if the object is null or an empty collection; otherwise, false.</returns>
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


    /// <summary>
    /// Represents the result of an operation that returns a value of type <typeparamref name="T"/>, along with a
    /// message and response.
    /// </summary>
    /// <remarks>Use this class to encapsulate both the result value and additional information about the
    /// operation, such as success status and descriptive messages. The <see cref="IsNullOrEmpty"/> property can be used
    /// to determine whether the operation succeeded and the result is considered empty.</remarks>
    /// <typeparam name="T">The type of the value returned by the operation.</typeparam>
    /// <param name="item1">The value produced by the operation. May be <see langword="null"/> if the operation did not yield a result.</param>
    /// <param name="message">A message describing the outcome of the operation.</param>
    /// <param name="responseValue">Metadata indicating the status or details of the operation's response.</param>
    public class OperationResult<T>(T item1, string message, ResponseValue responseValue) : OperationResult(message, responseValue)
    {
        public T? Item1 { get; set; } = item1;

        public bool IsNullOrEmpty => IsSuccess && IsObjectEmpty(Item1);     
    }


    /// <summary>
    /// Represents the result of an operation that returns two items, a message, and a response value, providing
    /// additional information about the outcome.
    /// </summary>
    /// <remarks>Use this class to encapsulate the result of operations that return two values along with
    /// status information. The properties IsNullOrEmpty, IsItem1NullOrEmpty, and IsItem2NullOrEmpty can be used to
    /// check whether the returned items are considered empty when the operation is successful.</remarks>
    /// <typeparam name="T1">The type of the first item returned by the operation.</typeparam>
    /// <typeparam name="T2">The type of the second item returned by the operation.</typeparam>
    /// <param name="item1">The value of the first item returned by the operation. Can be null if the operation did not produce a value for
    /// this item.</param>
    /// <param name="item2">The value of the second item returned by the operation. Can be null if the operation did not produce a value for
    /// this item.</param>
    /// <param name="message">A message describing the result of the operation. Typically used to convey success, error details, or other
    /// status information.</param>
    /// <param name="responseValue">A value indicating the status or outcome of the operation.</param>
    public class OperationResult<T1,T2>(T1? item1, T2? item2, string message, ResponseValue responseValue)
    : OperationResult(message,responseValue )
    {
        public T1? Item1 { get; set; }=item1;

        public T2? Item2 { get; set; }=item2;
        public bool IsNullOrEmpty => IsSuccess && IsObjectEmpty(Item1) && IsObjectEmpty(Item2);
        public bool IsItem1NullOrEmpty => IsSuccess && IsObjectEmpty(Item1);
        public bool IsItem2NullOrEmpty => IsSuccess && IsObjectEmpty(Item2);
      
    }

    /// <summary>
    /// Represents the result of an operation that returns three values, along with a message and a response status.
    /// </summary>
    /// <remarks>Use this class to encapsulate the outcome of operations that produce multiple result values,
    /// along with additional context such as a message and a response status. The generic parameters allow for flexible
    /// typing of the returned values.</remarks>
    /// <typeparam name="T1">The type of the first value returned by the operation.</typeparam>
    /// <typeparam name="T2">The type of the second value returned by the operation.</typeparam>
    /// <typeparam name="T3">The type of the third value returned by the operation.</typeparam>
    /// <param name="item1">The first value returned by the operation.</param>
    /// <param name="item2">The second value returned by the operation.</param>
    /// <param name="item3">The third value returned by the operation.</param>
    /// <param name="message">A message describing the result of the operation.</param>
    /// <param name="responseValue">The response status associated with the operation result.</param>
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
