


namespace Mythonia.Resources.Data.Tree
{
    public class Tree<LeaveType> where LeaveType : NodeLeave<LeaveType>
    {
        private readonly NodeBranch<LeaveType> _root;
        public NodeBranch<LeaveType> Root => _root;



        #region Constructors

        public Tree()
        {
            _root = NodeRoot<LeaveType>.ConstructRoot();
            _root.Initialize(null);

        }



        #endregion


        public override string ToString() => Root.GetAllNodesAsString();
    }
}
