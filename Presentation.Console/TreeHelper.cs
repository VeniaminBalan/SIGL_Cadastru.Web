using Domain.Models.Entities.Tree;
using System.Xml.Linq;

namespace Presentation.Console;
public static class TreeHelper
{
    public static TreeNodeDto GetTree(this IEnumerable<TreeNode> nodes)
    {
        TreeNodeDto root = null;

        foreach (var node in nodes)
        {
            if (node.Type == TreeNodeType.Root)
            {
                root = new TreeNodeDto
                {
                    Id = node.Id,
                    Type = Enum.GetName(typeof(TreeNodeType), node.Type),
                    Description = node.Description,
                    Price = node.Price,
                    Deadline = node.Deadline
                };

                root.Childrens.AddRange(nodes.GetChildrens(root.Id));
            }
        }

        return root;
    }

    public static IEnumerable<TreeNodeDto> GetChildrens(this IEnumerable<TreeNode> nodes, string parentId)
    {
        var childrens = nodes.Where(x => x.ParentId == parentId).Select(n => new TreeNodeDto
        {
            Id = n.Id,
            Type = Enum.GetName(typeof(TreeNodeType),n.Type),
            Description = n.Description,
            Price = n.Price,
            Deadline = n.Deadline
        }).ToList();

        foreach (var node in childrens)
        {
            node.Childrens.AddRange(nodes.GetChildrens(node.Id));
        }

        return childrens;
    }
}




public record TreeNodeDto()
{
    public string Id { get;  set; }
    public string Type { get;  set; }
    public string? Description { get;  set; }
    public decimal? Price { get;  set; }
    public int? Deadline { get;  set; }

    public List<TreeNodeDto> Childrens { get; set; } = [];
}