using System.Collections.Generic;

public abstract class Node
{
    public abstract bool Execute();
}

public class Sequence : Node
{
    private List<Node> nodes = new List<Node>();

    public void AddNode(Node node)
    {
        nodes.Add(node);
    }

    public override bool Execute()
    {
        foreach (Node node in nodes)
        {
            if (!node.Execute())
            {
                return false;
            }
        }
        return true;
    }
}

public class Selector : Node
{
    private List<Node> nodes = new List<Node>();

    public void AddNode(Node node)
    {
        nodes.Add(node);
    }

    public override bool Execute()
    {
        foreach (Node node in nodes)
        {
            if (node.Execute())
            {
                return true;
            }
        }
        return false;
    }
}

public abstract class ActionNode : Node
{
    public override abstract bool Execute();
}