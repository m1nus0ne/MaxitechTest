namespace MaxitechTest;

public class Node(char value)
{
    public char Value { get; } = value;
    public Node? Left { get; private set; }
    public Node? Right { get; private set; }

    public void Insert(char value)
    {
        if (value <= Value)
        {
            if (Left == null)
            {
                Left = new Node(value);
            }
            else
            {
                Left.Insert(value);
            }
        }
        else
        {
            if (Right == null)
            {
                Right = new Node(value);
            }
            else
            {
                Right.Insert(value);
            }
        }
    }

    public void InOrderTraversal(List<char> result)
    {
        Left?.InOrderTraversal(result);
        result.Add(Value);
        Right?.InOrderTraversal(result);
    }
}