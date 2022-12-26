namespace BetterGameUI.Exception
{
    public class FailToFindInstruction : System.Exception
    {
        public FailToFindInstruction() { }
        public FailToFindInstruction(string message) : base(message) { }
        public FailToFindInstruction(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
