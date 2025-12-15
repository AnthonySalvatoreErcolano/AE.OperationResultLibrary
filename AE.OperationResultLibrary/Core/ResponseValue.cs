using System;
using System.Collections.Generic;
using System.Text;

namespace AE.OperationResultLibrary.Core
{
    public class ResponseValue
    {
        public int Code { get;}
        public string Name { get; }

        private ResponseValue(int code, string name)
        {
            Code = code;
            Name = name;

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
        /// Using the C# 14 extension blocks, you can define custom response values. 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ResponseValue DefineCustom(int code, string name) => new ResponseValue(code, name);
     

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
