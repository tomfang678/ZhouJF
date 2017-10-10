using System.Collections.Generic;

namespace YHPT.Management.WebUI.Models
{
    /// <summary>
    /// Tree类
    /// </summary>
    public class Node
    {
        public Node() { }

        public int nodeId { get; set; }    //树的节点Id，区别于数据库中保存的数据Id。若要存储数据库数据的Id，添加新的Id属性；若想为节点设置路径，类中添加Path属性
        public int id { get; set; }
        public string code { get; set; }
        public string text { get; set; }   //节点名称
        public bool isSelected { get; set; } //是否选中
        public List<Node> nodes { get; set; }    //子节点，可以用递归的方法读取，方法在下一章会总结
    }
}