



namespace Mythonia.Resources.Data.Tree
{
    public abstract class Node<LeaveType> where LeaveType : NodeLeave<LeaveType>
    {
        private readonly string _name;
        public virtual string Name => _name;

        public virtual string Path { get; private set; }
        public virtual string FullPath => Path + '.' + Name;

        public NodeBranch<LeaveType> Father { get; private set; }

        private readonly float _weight;
        /// <summary>在兄弟节点中的排序比重</summary>
        public float Weight => _weight;
        

        public abstract int LeavesCount { get; }
        public abstract int NodesCount { get; }



        #region Constructors

        public Node(string name, float weight)
        {
            _name = name;
            _weight = weight;
        }

        #endregion

        
        public virtual void Initialize(NodeBranch<LeaveType> father)
        {
            Father = father;
            Path = father.FullPath;
        }


        public abstract LeaveType[] FindLeavesByName(string name);
        public abstract Node<LeaveType>[] FindNodesByName(string name);

        public abstract Node<LeaveType>[] GetAllNodes();
        public abstract string GetAllNodesAsString();
        public abstract LeaveType[] GetAllLeaves();


        public override string ToString() => $"Node \"{Name}\"";

    }
}
