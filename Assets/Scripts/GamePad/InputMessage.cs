public sealed class InputMessage : Message
{
    public readonly string Input;

    public InputMessage(string input)
    {
        Input = input;
    }
}
