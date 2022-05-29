



namespace Mythonia.Resources.Data.Tree
{
    public interface ILeaveObject<BranchType, LeaveType> : IMClass
        where BranchType : IBranchObject<BranchType, LeaveType> where LeaveType : ILeaveObject<BranchType, LeaveType>
    {
        public NodeLeave<BranchType, LeaveType> Node { get; }
    }
}
