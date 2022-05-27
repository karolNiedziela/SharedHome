namespace SharedHome.Shared.Abstractions.Exceptions
{
    public class InvalidEnumException : SharedHomeException
    {
        public override string ErrorCode => "InvalidEnum";
     
        public InvalidEnumException() : base("Invalid enum value.")
        {

        }

    }
}
