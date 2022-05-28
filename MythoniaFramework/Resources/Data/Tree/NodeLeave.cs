



namespace Mythonia.Resources.Data.Tree
{
    public abstract class NodeLeave<LeaveType> : Node<LeaveType> where LeaveType : NodeLeave<LeaveType>
    {
        #region Props

        #region Override Props - Node<LeaveType>

        public override int LeavesCount => 1;
        public override int NodesCount => 1; 

        #endregion

        #endregion


        #region Constructors

        public NodeLeave(string name, float weight) : base(name, weight)
        {

        }

        #endregion


        #region Override Methods - Node<LeaveType>

        public override Node<LeaveType>[] FindNodesByName(string name) => Name == name ? new[] { this } : Array.Empty<Node<LeaveType>>();
        public override LeaveType[] FindLeavesByName(string name) => 
            Name == name && this is LeaveType leave ? new[] { leave } : Array.Empty<LeaveType>();

        public override Node<LeaveType>[] GetAllNodes() => new[] { this };
        public override string GetAllNodesAsString() => Name;
        public override LeaveType[] GetAllLeaves() => new[] { (LeaveType)this };

        #endregion

    }
}
