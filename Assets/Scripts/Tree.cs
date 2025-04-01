using System.Collections.Generic;


public class Tree<T>
{
    public Tree<T> Parent;
    public List<Tree<T>> Children = new();
    public List<T> values = new();

    public void AddChild(Tree<T> tree)
    {
        Children.Add(tree);
        tree.Parent = this;
    }
    public void AddValue(T value)
    {
        values.Add(value);
    }

    public override string ToString()
    {
        return string.Join('\n', ToStringLines(0));
    }
    public string[] ToStringLines(int depth)
    {
        string tabs = string.Empty;
        for (int i = 0; i < depth; i++)
        {
            tabs += '\t';
        }

        List<string> lines = new() { $"{tabs}{{TREE", $"{tabs}Values: {string.Join(';', values)}", $"{tabs}Children:" };
        foreach (Tree<T> child in Children) 
        {
            foreach(string line in child.ToStringLines(depth + 1))
            {
                lines.Add(tabs + line);
            }
        }
        lines.Add(tabs + "}");
        return lines.ToArray();
    }
}