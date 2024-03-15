
using System.Text.Json.Serialization;
using Domain.Models.Entities.Tree;
using Presentation.Console;

var root = TreeNode.CreateRoot("0", "Lucrari"); 
    var branch1 = TreeNode.CreateBranch("1", "0", "Elaborarea planului geometric");
        var branch1_1 = TreeNode.CreateBranch("2", "1", "0,1 - 0,3 ha");
            var leaf1_1_1 = TreeNode.CreateLeaf("3", "2", 1800, 20);
            var leaf1_1_2 = TreeNode.CreateLeaf("4", "2", 5000, 7);

        var branch1_2 = TreeNode.CreateBranch("5", "1", "0,3 - 1,0 ha");
            var leaf1_2_1 = TreeNode.CreateLeaf("6", "5", 2500, 20); 
            var leaf1_2_2 = TreeNode.CreateLeaf("7", "5", 3600, 7);
            
        var branch1_3 = TreeNode.CreateBranch("8", "1", "1,0 - 2,0 ha");
            var leaf1_3_1 = TreeNode.CreateLeaf("9", "8", 3000, 20);
            var leaf1_3_2 = TreeNode.CreateLeaf("10", "8", 6000, 7);

    var branch2 = TreeNode.CreateBranch("11", "0", "Transpunerea in termen a hotarelor");
        var leaf2_1 = TreeNode.CreateWithDots("12", "11", 200);

    var branch3 = TreeNode.CreateBranch("13", "0", "Identificarea bunului imobil");
        var leaf3_1 = TreeNode.CreateLeaf("14", "13", 1800, 10);
        var leaf3_2 = TreeNode.CreateLeaf("15", "13", 5400, 5);


var tree = new List<TreeNode> {root, branch1, branch1_1, branch1_2, branch1_3, leaf1_1_1, leaf1_1_2, leaf1_2_1, leaf1_2_2, leaf1_3_1, leaf1_3_2, branch2, leaf2_1, branch3, leaf3_1, leaf3_2};



var json = System.Text.Json.JsonSerializer.Serialize(tree.TreeIterator());

Console.WriteLine(json);