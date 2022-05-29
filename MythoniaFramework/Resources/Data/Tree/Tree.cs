


namespace Mythonia.Resources.Data.Tree
{
    public class Tree<BranchType, LeaveType>
        where BranchType : IBranchObject<BranchType, LeaveType> where LeaveType : ILeaveObject<BranchType, LeaveType>
    {
        private readonly NodeRoot<BranchType, LeaveType> _root;
        public NodeRoot<BranchType, LeaveType> Root => _root;



        #region Constructors

        public Tree(NodeRoot<BranchType, LeaveType> root)
        {
            _root = root;
            _root.Initialize(null);

        }



        #endregion


        public override string ToString() => Root.GetAllNodesAsString();
    }
}
