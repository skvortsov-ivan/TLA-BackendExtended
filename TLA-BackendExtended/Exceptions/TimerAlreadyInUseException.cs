namespace TLA_BackendExtended.Exceptions
{
    public class TimerAlreadyInUseException : Exception
    {
        public TimerAlreadyInUseException(string message) : base(message) { }
    }
}
