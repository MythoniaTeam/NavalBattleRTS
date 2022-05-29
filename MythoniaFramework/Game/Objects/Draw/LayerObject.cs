



namespace Mythonia.Game.Objects.Draw
{
    public class LayerObject : IBranchObject<LayerObject, Sprite>
    {

        #region Implement - IMClass 

        private readonly string _name;
        public string Name => _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        #endregion



        #region Implement - ILeaveObject <BranchType, LeaveType>

        private readonly NodeBranch<LayerObject, Sprite> _node;
        public NodeBranch<LayerObject, Sprite> Node => _node;

        #endregion



        #region Constructors

        public LayerObject(MGame game, string name)
        {
            _game = game;
            _name = name;
        }

        #endregion

    }
}
