﻿using System;

namespace OrderNow.Common.CustomException
{
    public class ApplicationException : Exception
    {
        public ErrorCodes ErrorCode { get; set; }
        protected ApplicationException(ErrorCodes errorCode)
        {
            ErrorCode = errorCode;
        }
        public string ErrorCodeMessageKey
        {
            get { return ErrorCode.ToString(); }
        }
    }
}
