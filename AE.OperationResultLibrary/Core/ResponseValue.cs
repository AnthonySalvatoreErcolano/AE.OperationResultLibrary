using System;
using System.Collections.Generic;
using System.Text;

namespace AE.OperationResultLibrary.Core
{
    /// <summary>
    /// Represents a response code and its associated name, typically used to indicate the result of an operation or
    /// request.
    /// </summary>
    /// <remarks>The ResponseValue class provides a set of predefined static instances for common response
    /// scenarios, such as Success, NotFound, Invalid, Failed, Unauthorized, Forbidden, and Warning. Custom response values can be defined using the DefineCustomResponse method. Instances are compared based on their response code. Standard HTTP response code conventions are followed for the predefined responses.  This class is immutable and thread-safe.</remarks>
    public class ResponseValue
    {
        public int Code { get;}
        public string Name { get; }

        private readonly bool? _isSuccessOverride ;

        public bool IsSuccessCode => _isSuccessOverride ?? (Code >= 200 && Code <= 299);

        /// <summary>
        /// Initializes a new instance of the ResponseValue class with the specified code, name, and optional success
        /// override.
        /// </summary>
        /// <param name="code">The status or result code associated with the response.</param>
        /// <param name="name">The name or description of the response value.</param>
        /// <param name="isSuccessOverride">An optional value that, if specified, overrides the default success determination for this response. If
        /// null, the default logic is used. Standard HTTP response code conventions are used and values between 200 and 299 are classified as success. If a custom success response needs to fall outside of this range, this indicator is used to specifiy the response is success. </param>
        private ResponseValue(int code, string name, bool? isSuccessOverride =null)
        {
            Code = code;
            Name = name;
            _isSuccessOverride = isSuccessOverride;

        }

        public static bool operator ==(ResponseValue a, ResponseValue b) => a.Code == b.Code;
        public static bool operator !=(ResponseValue a, ResponseValue b) => a.Code != b.Code;
        public override string ToString() => Name;

        public static ResponseValue Success { get; } = new ResponseValue(200, nameof(Success));
        public static ResponseValue NotFound { get; } = new ResponseValue(404, nameof(NotFound));
        public static ResponseValue Invalid { get; } = new ResponseValue(400, nameof(Invalid));
        public static ResponseValue Failed { get; } = new ResponseValue(500, nameof(Failed));
        public static ResponseValue Unauthorized { get; } = new ResponseValue(401, nameof(Unauthorized));
        public static ResponseValue Forbidden { get; } = new ResponseValue(403, nameof(Forbidden));
        public static ResponseValue Warning { get; } = new ResponseValue(202, nameof(Warning));


        /// <summary>
        /// Creates a custom response value with the specified status code, name, and optional success indicator.
        /// </summary>
        /// <param name="code">The status code to associate with the response. Typically corresponds to an HTTP status code or
        /// application-specific code.</param>
        /// <param name="name">The name or description of the response. Cannot be null.</param>
        /// <param name="isSuccess">An optional value indicating whether the response represents a successful outcome and the response code is outside of the standard HTTP suces response range. If null, the success
        /// status is unspecified.</param>
        /// <returns>A new instance of <see cref="ResponseValue"/> representing the custom response.</returns>
        public static ResponseValue DefineCustomResponse(int code, string name, bool? isSuccess = null)
        {
            return new ResponseValue(code, name, isSuccess);
        }


        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            if (obj is ResponseValue other)
            {
                return Code == other.Code; 
            }
            return false;
        }

        public override int GetHashCode() =>  HashCode.Combine(Code, Name);
        

    }
}
