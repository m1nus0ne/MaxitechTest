namespace MaxitechTest;

public class Node(char value)
{
    public char Value { get; } = value;
    public Node? Left { get; set; }
    public Node? Right { get; set; }
}