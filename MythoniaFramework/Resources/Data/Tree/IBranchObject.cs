


namespace Mythonia.Resources.Data.Tree
{
    public interface IBranchObject<BranchType, LeaveType> : IMClass
        where BranchType : IBranchObject<BranchType, LeaveType> where LeaveType : ILeaveObject<BranchType, LeaveType>
    {
        public NodeBranch<BranchType, LeaveType> Node { get; }
    }
}
