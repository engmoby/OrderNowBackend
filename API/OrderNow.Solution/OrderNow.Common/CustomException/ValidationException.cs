using ApplicationException = OrderNow.Common.CustomException.ApplicationException;

namespace OrderNow.Common.CustomException
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(ErrorCodes errorCode) : base(errorCode)
        {
        }
    }
}
