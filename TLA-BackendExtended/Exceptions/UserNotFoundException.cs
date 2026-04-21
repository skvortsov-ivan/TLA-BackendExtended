namespace TLA_BackendExtended.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message){}
    }
}
