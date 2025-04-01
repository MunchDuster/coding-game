public class DumpilerError 
{
    public string Message;
    public int Line;

    public DumpilerError(string message, int line)
    {
        Message = message;
        Line = line;
    }
}