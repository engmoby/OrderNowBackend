namespace OrderNow.Common.CustomException
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(ErrorCodes errorCode) : base(errorCode)
        {

        }
    }
}
