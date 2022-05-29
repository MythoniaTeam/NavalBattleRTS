



namespace Mythonia.Resources.Data.Tree
{
    public class NodeLeave<BranchType, LeaveType> : Node<BranchType, LeaveType> 
        where BranchType : IBranchObject<BranchType, LeaveType> where LeaveType : ILeaveObject<BranchType, LeaveType>
    {
        #region Props

        private readonly LeaveType _leaveObj;
        public LeaveType LeaveObj => _leaveObj;
        public override IMClass Obj => LeaveObj;



        #region Override Props - Node<BranchType, LeaveType> 

        public override int LeavesCount => 1;
        public override int NodesCount => 1; 

        #endregion

        #endregion



        #region Constructors

        public NodeLeave(LeaveType leaveObj, float weight) : base(weight)
        {
            _leaveObj = leaveObj;
        }

        #endregion



        #region Methods

        #region Override Methods - Node<LeaveType>

        public override Node<BranchType, LeaveType>[] FindNodesByName(string name) => 
            Name == name ? new[] { this } : Array.Empty<Node<BranchType, LeaveType>>();
        public override NodeLeave<BranchType, LeaveType>[] FindLeavesByName(string name) => 
            Name == name && this is NodeLeave<BranchType, LeaveType> leave ? new[] { leave } : Array.Empty<NodeLeave<BranchType, LeaveType>>();

        public override Node<BranchType, LeaveType>[] GetAllNodes() => new[] { this };
        public override string GetAllNodesAsString() => Name;
        public override NodeLeave<BranchType, LeaveType>[] GetAllLeaves() => new[] { this };

        #endregion

        #endregion

    }
}
