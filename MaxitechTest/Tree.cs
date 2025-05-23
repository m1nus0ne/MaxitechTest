namespace MaxitechTest;

public class Tree
{
    private Node? root;

    public Tree()
    {
        root = null;
    }

    public void Insert(char value)
    {
        root = InsertRecursive(root, value);
    }

    public List<char> InOrderTraversal()
    {
        var result = new List<char>();
        InOrderTraversal(root, result);
        return result;
    }

    private Node InsertRecursive(Node? node, char value)
    {
        if (node == null)
            return new Node(value);
        if (value < node.Value)
            node.Left = InsertRecursive(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursive(node.Right, value);
        return node;
    }
    
    private void InOrderTraversal(Node? node, List<char> result)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, result);
            result.Add(node.Value);
            InOrderTraversal(node.Right, result);
        }
    }
}