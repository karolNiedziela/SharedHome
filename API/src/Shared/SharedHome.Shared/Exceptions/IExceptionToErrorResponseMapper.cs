namespace SharedHome.Shared.Exceptions
{
    public interface IExceptionToErrorResponseMapper
    {
        public ErrorResponse Map(Exception exception);
    }
}
