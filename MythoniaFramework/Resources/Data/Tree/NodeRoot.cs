



namespace Mythonia.Resources.Data.Tree
{
    public class NodeRoot<BranchType, LeaveType> : NodeBranch<BranchType, LeaveType>
        where BranchType : IBranchObject<BranchType, LeaveType> where LeaveType : ILeaveObject<BranchType, LeaveType>
    {
        public override string Path => "";
        public override string FullPath => "";
        public override string Name => "#ROOT#";

        public NodeRoot(BranchType rootObj) : base(rootObj, 1)
        {

        }

        public override void Initialize(NodeBranch<BranchType, LeaveType> father) { }

        public static NodeRoot<BranchType, LeaveType> ConstructRoot(BranchType rootObj) => new(rootObj);

    }
}
