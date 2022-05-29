



namespace Mythonia.Resources.Data.Tree
{
    public abstract class Node<BranchType, LeaveType>
        where BranchType : IBranchObject<BranchType, LeaveType> where LeaveType : ILeaveObject<BranchType, LeaveType>
    {
        #region Props

        public abstract IMClass Obj { get; }

        public virtual string Name => Obj.Name;
        /// <summary>加上 "#No" 后的名称</summary>
        public string FullName => Name + (No == 0 ? "" : ('#' + No.ToString()));


        private int _no;
        public int No { get => _no; private set => _no = value; }


        public virtual string Path { get; private set; }
        public virtual string FullPath => Path + '.' + Name;


        public NodeBranch<BranchType, LeaveType> Father { get; private set; }

        private readonly float _weight;
        /// <summary>在兄弟节点中的排序比重</summary>
        public float Weight => _weight;
        


        public abstract int LeavesCount { get; }
        public abstract int NodesCount { get; }

        #endregion



        #region Constructors

        public Node(float weight)
        {
            _weight = weight;
        }

        #endregion

        
        public virtual void Initialize(NodeBranch<BranchType, LeaveType> father)
        {
            Father = father;
            Path = father.FullPath;
        }


        public abstract NodeLeave<BranchType, LeaveType>[] FindLeavesByName(string name);
        public abstract Node<BranchType, LeaveType>[] FindNodesByName(string name);

        public abstract Node<BranchType, LeaveType>[] GetAllNodes();
        public abstract string GetAllNodesAsString();
        public abstract NodeLeave<BranchType, LeaveType>[] GetAllLeaves();


        public override string ToString() => $"Node \"{Name}\"";

    }
}
