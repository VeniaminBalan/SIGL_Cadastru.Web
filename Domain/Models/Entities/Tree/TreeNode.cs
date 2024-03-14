using Domain.Shared;

namespace Domain.Models.Entities.Tree;

public sealed class TreeNode
{
    public string Id { get; private set; }
    public string? ParentId { get; private set; }
    public TreeNodeType Type { get; private set; }
    public string? Description { get; private set; }
    public decimal? Price { get; private set; }
    public int? Deadline { get; private set; }


    public bool IsLeaf => Type == TreeNodeType.Leaf;

    private TreeNode()
    {

    }

    public static TreeNode CreateRoot(string id, string description)
    {
        return new TreeNode
        {
            Id = id,
            Type = TreeNodeType.Root,
            Description = description,
            ParentId = null,
            Price = null,
            Deadline = null,
        };
    }

    public static TreeNode CreateBranch(string id, string parentId, string description)
    {
        return new TreeNode
        {
            Id = id,
            Type = TreeNodeType.Branch,
            Description = description,
            ParentId = parentId,
            Price = null,
            Deadline = null,
        };
    }

    public static TreeNode CreateWithDots(string id, string parentId, string description, int price)
    {
        return new TreeNode
        {
            Id = id,
            Type = TreeNodeType.Dots,
            Description = description,
            ParentId = parentId,
            Price = price,
            Deadline = null,
        };
    }

    public static TreeNode CreateLeaf(string id, string parentId, decimal price, int deadline)
    {
        return new TreeNode
        {
            Id = id,
            Type = TreeNodeType.Leaf,
            ParentId = parentId,
            Price = price,
            Deadline = deadline
        };

    }

}


public enum TreeNodeType
{
    Root,
    Branch,
    Dots,
    Leaf
}