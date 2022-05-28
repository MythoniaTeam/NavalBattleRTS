



namespace Mythonia.Resources.Data.Tree
{
    public class NodeRoot<LeaveType> : NodeBranch<LeaveType> where LeaveType : NodeLeave<LeaveType>
    {
        public override string Path => "";
        public override string FullPath => "";
        public override string Name => "#ROOT#";

        public NodeRoot() : base("ROOT", 1)
        {

        }

        public override void Initialize(NodeBranch<LeaveType> father) { }

        public static NodeRoot<LeaveType> ConstructRoot() => new();

    }
}
