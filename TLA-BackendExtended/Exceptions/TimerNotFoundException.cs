namespace TLA_BackendExtended.Exceptions
{
    public class TimerNotFoundException : Exception
    {
        public TimerNotFoundException(string message) : base(message) { }
    }
}
