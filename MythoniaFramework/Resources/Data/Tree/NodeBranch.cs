



namespace Mythonia.Resources.Data.Tree
{
    public class NodeBranch<BranchType, LeaveType> : Node<BranchType, LeaveType>
        where BranchType : IBranchObject<BranchType, LeaveType> where LeaveType : ILeaveObject<BranchType, LeaveType>
    {
        #region Props



        #region Override Props - Node<BranchType, LeaveType>

        private readonly BranchType _branchObj;
        public BranchType BranchObj => _branchObj;
        public override IMClass Obj => BranchObj;

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// 由于是分支节点, 将返回 <b>'#' + 名字</b>, <br/>
        /// 若需要 <b>获取原名</b>, 请调用 <see langword="base"/>.Name
        /// </para>
        /// </summary>
        public override string Name => '#' + base.Name;



        public override int NodesCount
        {
            get
            {
                int count = 1;
                foreach (Node<BranchType, LeaveType> node in SubNodes) count += node.NodesCount;
                return count;
            }
        }
        public override int LeavesCount
        {
            get
            {
                int count = 0;
                foreach (Node<BranchType, LeaveType> node in SubNodes) count += node.LeavesCount;
                return count;
            }
        }

        #endregion



        /// <summary>子节点的列表</summary>
        public List<Node<BranchType, LeaveType>> SubNodes { get; private set; } = new();

        /// <summary>
        /// 子节点 Weight 的范围, <br/>
        /// 若 SubNodes 数量为 0, 将为默认值 (0, 0)
        /// </summary>
        public (float Min, float Max) WeightRange { get; set; } = (0, 0);

        #endregion



        #region Constructors

        public NodeBranch(BranchType branchObj, float weight) : base(weight)
        {
            _branchObj = branchObj;
        }

        public static NodeBranch<BranchType, LeaveType> ConstructBranch(BranchType branchObj, float weight) => new(branchObj, weight);

        #endregion



        #region Methods

        #region Override Methods - fr Node

        /// <summary>
        /// 根据名字, 获取并返回 <see cref="Node.Name"/> == <paramref name="name"/> 的叶子节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// 该分支后代中, (<see cref="SubNodes"/>) <br/>
        /// Name符合条件的 <seealso cref="NodeLeave"/>节点 组成的数组
        /// </returns>
        public override NodeLeave<BranchType, LeaveType>[] FindLeavesByName(string name)
        { 
            List<NodeLeave<BranchType, LeaveType>> nodes = new();

            //遍历调用 子节点的 FindLeavesByName(..) 方法, 递归返回所有符合条件的对象
            foreach(Node<BranchType, LeaveType> node in SubNodes) 
                nodes.AddRange(node.FindLeavesByName(name));

            return nodes.ToArray();
        }

        /// <summary>
        /// 根据名字, 获取并返回 <see cref="Node{LeaveType}.Name"/> == <paramref name="name"/> 的节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// 该分支 自身和后代 中, (<see langword="this"/> 和 <see cref="SubNodes"/>) <br/>
        /// Name符合条件的 <seealso cref="Node"/>节点 组成的数组<br/>
        /// (包括 <seealso cref="NodeLeave"/> 和 <seealso cref="NodeBranch"/>)
        /// </returns>
        public override Node<BranchType, LeaveType>[] FindNodesByName(string name)
        {
            List<Node<BranchType, LeaveType>> nodes = new();

            //如果自己符合条件, 添加自己
            if (Name == '#' + name) nodes.Add(this);

            //遍历调用 子节点的 FindNodesByName(..) 方法, 递归返回所有符合条件的对象
            foreach (Node<BranchType, LeaveType> node in SubNodes)
                nodes.AddRange(node.FindNodesByName(name));

            return nodes.ToArray();
        }

        #endregion

        

        /// <summary>
        /// 给定路径, 按照路径, 准确找到分支节点
        /// </summary>
        /// <param name="path">需求的节点 的路径</param>
        /// <returns>对应的后代分支节点</returns>
        /// <exception cref="NullReferenceException"></exception>
        public virtual NodeBranch<BranchType, LeaveType> TryFindBranch(string path)
        {
            try
            {
                if (path.Contains('.'))
                {
                    string[] pathSplited = path.Split('.', 2);
                    return TryFindChildBranch(pathSplited[0]).TryFindBranch(pathSplited[1]);
                }
                else
                {
                    return TryFindChildBranch(path);
                }
            }
            catch(NullReferenceException e)
            {
                throw new NullReferenceException(e + $"\n in FindBranchByPath({path})");
            }
        }

        /// <summary>
        /// 寻找并返回 <see cref="Name"/> == <paramref name="name"/> 的 <b>子分支节点</b>
        /// </summary>
        /// <param name="name">子节点的含编号全名</param>
        /// <returns>
        /// <see cref="NodeBranch"/> 类型的 子分支节点, <br/>
        /// 若找不到, 将抛出异常
        /// </returns>
        /// <exception cref="NullReferenceException"></exception>
        public NodeBranch<BranchType, LeaveType> TryFindChildBranch(string name)
        {
            foreach (Node<BranchType, LeaveType>  node in SubNodes)
            {
                if (node is NodeBranch<BranchType, LeaveType>  branch)
                {
                    if (branch.FullName == '#' + name) return branch;
                }
            }
            throw new NullReferenceException($"The Child Branch named \"{name}\" is Not Found under branch \"{Path + base.FullName}\".");
        }



        public void TryAdd(Node<BranchType, LeaveType> node, string path)
        {
            try
            {
                TryFindBranch(path).Add(node);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e + $"\n in TryAdd({node}, {path})");
            }

        }

        /// <summary>
        /// 将 <paramref name="node"/> 添加到当前节点中 (添加到 <see cref="SubNodes"/>)
        /// </summary>
        /// <param name="node">需要添加的Node</param>
        public void Add(Node<BranchType, LeaveType> node)
        {
            //设置 node 的 父节点 为 this
            node.Initialize(this);

            if (SubNodes.Count > 0)
            {
                int index = SubNodes.FindIndex(v => v.Weight > node.Weight);
                if (index == -1)
                {
                    //如果是-1, 说明找不到 Weight 比 node 更大 的节点, 将 WeightRange.Max 设为 node.Weight
                    SubNodes.Add(node);
                    WeightRange = (WeightRange.Min, node.Weight);
                }
                else
                {
                    //否则插入 index 的位置 (将位于 Weight <= node 的节点之后)
                    SubNodes.Insert(index, node);

                    //如果 node.Weight < WeightRange.Min,  将 WeightRange.Min 设为 node.Weight
                    if (node.Weight < WeightRange.Min)
                        WeightRange = (node.Weight, WeightRange.Max);
                }
            }
            else
            {
                //如果 SubNodes 内没有对象, 直接添加node进入 SubNodes, 并初始化 WeightRange
                SubNodes.Add(node);
                WeightRange = (node.Weight, node.Weight);
            }
        }

        ///// <summary>
        ///// 检查 <see cref="SubNodes"/> 内是否已经有 <see cref="Node{LeaveType}.Name"/> == <paramref name="name"/> 的节点, <br/>
        ///// 如果有, 在名字后添加一个合适的数字编号返回
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns>可用的名字</returns>
        //private string CheckName(string name)
        //{

        //}


        /// <summary>
        /// 检查 <see cref="SubNodes"/> 内是否已经有 <see cref="Node{LeaveType}.Name"/> == <paramref name="name"/> 的节点,
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// <list type="table">
        /// <item><term><see langword="true"/></term> <description><see cref="SubNodes"/>中 <b>包含</b> 名字为<paramref name="name"/>的节点</description></item>
        /// <item><term><see langword="false"/></term> <description><see cref="SubNodes"/>中 <b>不包含</b> 名字为<paramref name="name"/>的节点</description></item>
        /// </list>
        /// </returns>
        public bool SubNodeExist(string name) => SubNodes.FindIndex(v => (v.Name == name)) != -1;

        /// <summary>
        /// 检查 <see cref="SubNodes"/> 内<br/>
        /// 是否已经有 <see cref="Node{BranchType, LeaveType}.Name"/> == <paramref name="name"/> 的<b>分支节点</b>, (<seealso cref="NodeBranch{BranchType, LeaveType}"/> 类型)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// <list type="table">
        /// <item><term><see langword="true"/></term> <description><see cref="SubNodes"/>中 <b>包含</b> 名字为<paramref name="name"/>的<b>分支节点</b></description></item>
        /// <item><term><see langword="false"/></term> <description><see cref="SubNodes"/>中 <b>不包含</b> 名字为<paramref name="name"/>的<b>分支节点</b></description></item>
        /// </list>
        /// </returns>
        public bool SubBranchExist(string name) => SubNodes.FindIndex(v => (v is NodeBranch<BranchType, LeaveType> && v.Name == name)) != -1;

        /// <summary>
        /// 检查 <see cref="SubNodes"/> 内<br/>
        /// 是否已经有 <see cref="Node{BranchType, LeaveType}.Name"/> == <paramref name="name"/> 的<b>叶子节点</b>, (<seealso cref="NodeLeave{BranchType, LeaveType}"/> 类型)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// <list type="table">
        /// <item><term><see langword="true"/></term> <description><see cref="SubNodes"/>中 <b>包含</b> 名字为<paramref name="name"/>的<b>叶子节点</b></description></item>
        /// <item><term><see langword="false"/></term> <description><see cref="SubNodes"/>中 <b>不包含</b> 名字为<paramref name="name"/>的<b>叶子节点</b></description></item>
        /// </list>
        /// </returns>
        public bool SubLeaveExist(string name) => SubNodes.FindIndex(v => (v is NodeLeave<BranchType, LeaveType> && v.Name == name)) != -1;



        public override Node<BranchType, LeaveType>[] GetAllNodes()
        {
            List<Node<BranchType, LeaveType>> nodes = new();
            nodes.Add(this);
            foreach (Node<BranchType, LeaveType> node in SubNodes)
            {
                nodes.AddRange(node.GetAllNodes());
            }
            return nodes.ToArray();
        }

        public override string GetAllNodesAsString()
        {
            string nodes = Name + " { ";
            foreach (Node<BranchType, LeaveType> node in SubNodes)
            {
                nodes += $"{node.GetAllNodesAsString()}, ";
            }

            nodes = (SubNodes.Count >= 1 ? nodes[0..^2] : nodes) + " }";
            return nodes;
        }


        public override NodeLeave<BranchType, LeaveType>[] GetAllLeaves()
        {
            List<NodeLeave<BranchType, LeaveType>> nodes = new();

            foreach (Node<BranchType, LeaveType> node in SubNodes)
            {
                nodes.AddRange(node.GetAllLeaves());
            }
            return nodes.ToArray();
        }



        #region Methods - Abandoned (Non-Try Mothods)

        ///// <summary>
        ///// <inheritdoc cref="TryFindBranch(string)"/>
        ///// <para>
        ///// 若找不到对应对象, 会创建一个新的对象, <br/>
        ///// Weight 为 <see cref="SubNodes"/> 最后一项的 Weight +1
        ///// </para>
        ///// </summary>
        ///// <param name="path">需求的节点 的路径</param>
        ///// <returns>对应的后代分支节点</returns>
        //public virtual NodeBranch<BranchType, LeaveType> FindBranch(string path)
        //{
        //    if (path.Contains('.'))
        //    {
        //        string[] pathSplited = path.Split('.', 2);
        //        return FindChildBranch(pathSplited[0]).FindBranch(pathSplited[1]);
        //    }
        //    else
        //    {
        //        return FindChildBranch(path);
        //    }

        //}

        ///// <summary>
        ///// <inheritdoc cref="TryFindChildBranch(string)"/>
        ///// <para>
        ///// 若找不到对应对象, 会创建一个新的对象, <br/>
        ///// Weight 为 <see cref="SubNodes"/> 最后一项的 Weight +1
        ///// </para>
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns><see cref="NodeBranch"/> 类型的 子分支节点, <br/></returns>
        //public NodeBranch<BranchType, LeaveType> FindChildBranch(string name)
        //{
        //    try
        //    {
        //        return TryFindChildBranch(name);
        //    }
        //    catch (NullReferenceException)
        //    {
        //        NodeBranch<BranchType, LeaveType>  branch = new(name, SubNodes[SubNodes.Count].Weight + 1);
        //        Add(branch);
        //        return branch;
        //    }
        //}

        //public void Add(Node<BranchType, LeaveType> node, string path)
        //{
        //    FindBranch(path).Add(node);
        //}

        #endregion


        #endregion
    }
}
