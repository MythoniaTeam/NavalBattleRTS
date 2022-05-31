



namespace Mythonia.Game.Objects.UI
{
    public class UIManager : Tree<IUIBranchObject, UIObject>, IMClass
    {

        #region Implement - IMClass 

        private readonly string _name;
        public string Name => _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        #if DEBUG
        #nullable enable
        Type? IMClass.TypeRecord { get; set; }
        #endif

        #endregion



        #region Constructors

        public UIManager(Screen screen) : base(screen.Node)
        {
            _game = screen.MGame;
            _name = "UIManager";
        }

        #endregion

    }
}
