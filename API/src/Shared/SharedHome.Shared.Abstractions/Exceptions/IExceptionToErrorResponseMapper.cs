using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Shared.Abstractions.Exceptions
{
    public interface IExceptionToErrorResponseMapper
    {
        public ErrorResponse Map(Exception exception);
    }
}
