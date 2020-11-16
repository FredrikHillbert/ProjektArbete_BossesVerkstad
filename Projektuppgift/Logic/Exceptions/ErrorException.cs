using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.MyExceptions
{
    public class ErrorException : Exception
    {
        public ErrorException() : base() { }
        public ErrorException(string message) : base(message) { }
        public ErrorException(string message, Exception inner) : base(message, inner) { }
        protected ErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected string _info;

        public string ErrorInfo
        {
            get
            {
                return _info;
            }

            set
            {
                _info = value;
            }

        }










    }

}

