



namespace Mythonia.Resources.Data.Tree
{
    public class NodeBranch<LeaveType> : Node<LeaveType> where LeaveType : NodeLeave<LeaveType>
    {
        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// 由于是分支节点, 将返回 <b>'#' + 名字</b>, <br/>
        /// 若需要 <b>获取原名</b>, 请调用 <see langword="base"/>.Name
        /// </para>
        /// </summary>
        public override string Name => '#' + base.Name;

        

        /// <summary>子节点的列表</summary>
        public List<Node<LeaveType>> SubNodes { get; private set; } = new();

        /// <summary>
        /// 子节点 Weight 的范围, <br/>
        /// 若 SubNodes 数量为 0, 将为默认值 (0, 0)
        /// </summary>
        public (float Min, float Max) WeightRange { get; set; } = (0, 0);


        public override int NodesCount
        {
            get
            {
                int count = 1;
                foreach (Node<LeaveType> node in SubNodes) count += node.NodesCount;
                return count;
            }
        }
        public override int LeavesCount
        {
            get
            {
                int count = 0;
                foreach (Node<LeaveType> node in SubNodes) count += node.LeavesCount;
                return count;
            }
        }

        #region Constructors

        public NodeBranch(string name, float weight) : base(name, weight)
        {

        }

        public static NodeBranch<LeaveType> ConstructBranch(string name, float weight) => new(name, weight);

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
        public override LeaveType[] FindLeavesByName(string name)
        { 
            List<LeaveType> nodes = new();

            //遍历调用 子节点的 FindLeavesByName(..) 方法, 递归返回所有符合条件的对象
            foreach(Node<LeaveType> node in SubNodes) 
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
        public override Node<LeaveType>[] FindNodesByName(string name)
        {
            List<Node<LeaveType>> nodes = new();

            //如果自己符合条件, 添加自己
            if (Name == '#' + name) nodes.Add(this);

            //遍历调用 子节点的 FindNodesByName(..) 方法, 递归返回所有符合条件的对象
            foreach (Node<LeaveType> node in SubNodes)
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
        public virtual NodeBranch<LeaveType> TryFindBranch(string path)
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
        /// <inheritdoc cref="TryFindBranch(string)"/>
        /// <para>
        /// 若找不到对应对象, 会创建一个新的对象, <br/>
        /// Weight 为 <see cref="SubNodes"/> 最后一项的 Weight +1
        /// </para>
        /// </summary>
        /// <param name="path">需求的节点 的路径</param>
        /// <returns>对应的后代分支节点</returns>
        public virtual NodeBranch<LeaveType> FindBranch(string path)
        {
            if (path.Contains('.'))
            {
                string[] pathSplited = path.Split('.', 2);
                return FindChildBranch(pathSplited[0]).FindBranch(pathSplited[1]);
            }
            else
            {
                return FindChildBranch(path);
            }

        }



        /// <summary>
        /// 寻找并返回 <see cref="Name"/> == <paramref name="name"/> 的 <b>子分支节点</b>
        /// </summary>
        /// <param name="name">子节点的名字</param>
        /// <returns>
        /// <see cref="NodeBranch"/> 类型的 子分支节点, <br/>
        /// 若找不到, 将抛出异常
        /// </returns>
        /// <exception cref="NullReferenceException"></exception>
        public NodeBranch<LeaveType> TryFindChildBranch(string name)
        {
            foreach (Node<LeaveType> node in SubNodes)
            {
                if (node is NodeBranch<LeaveType> branch)
                {
                    if (branch.Name == '#' + name) return branch;
                }
            }
            throw new NullReferenceException($"The Child Branch named \"{name}\" is Not Found under branch \"{Path + base.Name}\".");
        }

        /// <summary>
        /// <inheritdoc cref="TryFindChildBranch(string)"/>
        /// <para>
        /// 若找不到对应对象, 会创建一个新的对象, <br/>
        /// Weight 为 <see cref="SubNodes"/> 最后一项的 Weight +1
        /// </para>
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="NodeBranch"/> 类型的 子分支节点, <br/></returns>
        public NodeBranch<LeaveType> FindChildBranch(string name)
        {
            try
            {
                return TryFindChildBranch(name);
            }
            catch (NullReferenceException)
            {
                NodeBranch<LeaveType> branch = new(name, SubNodes[SubNodes.Count].Weight + 1);
                Add(branch);
                return branch;
            }
        }



        public void TryAdd(Node<LeaveType> node, string path)
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

        public void Add(Node<LeaveType> node, string path)
        {
            FindBranch(path).Add(node);
        }

        /// <summary>
        /// 将 <paramref name="node"/> 添加到当前节点中 (添加到 <see cref="SubNodes"/>)
        /// </summary>
        /// <param name="node">需要添加的Node</param>
        public void Add(Node<LeaveType> node)
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

        public override Node<LeaveType>[] GetAllNodes()
        {
            List<Node<LeaveType>> nodes = new();
            nodes.Add(this);
            foreach(Node<LeaveType> node in SubNodes)
            {
                nodes.AddRange(node.GetAllNodes());
            }
            return nodes.ToArray();
        }

        public override string GetAllNodesAsString()
        {
            string nodes = Name + " { ";
            foreach (Node<LeaveType> node in SubNodes)
            {
                nodes += $"{node.GetAllNodesAsString()}, ";
            }
            
            nodes = (SubNodes.Count >= 1 ? nodes[0..^2] : nodes) + " }";
            return nodes;
        }


        public override LeaveType[] GetAllLeaves()
        {
            List<LeaveType> nodes = new();

            foreach (Node<LeaveType> node in SubNodes)
            {
                nodes.AddRange(node.GetAllLeaves());
            }
            return nodes.ToArray();
        }




        #endregion
    }
}
