namespace BetterGameUI.Exception
{
    public class InstructionNotFound : System.Exception
    {
        public InstructionNotFound() { }
        public InstructionNotFound(string message) : base(message) { }
        public InstructionNotFound(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
