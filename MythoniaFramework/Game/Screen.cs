using Mythonia.Game.Objects.UI;


namespace Mythonia.Game
{
    /// <summary>
    /// 屏幕对象, <br/>
    /// 透过该对象, 可以获取屏幕的边缘点, 尺寸等属性
    /// <para>
    /// 同时也负责 初始化 <seealso cref="UIManager"/>
    /// </para>
    /// </summary>
    public class Screen : IUIBranchObject, IMClass
    {

        #region Implement - IMClass 
        public string Name => "Screen";
        private readonly MGame _game;
        public MGame MGame => _game;

        #if DEBUG
        #nullable enable
        Type? IMClass.TypeRecord { get; set; }
        #endif

        #endregion



        #region Implement - IBranchObject <BranchType, LeaveType>

        private readonly UINodeRoot _node;
        public UINodeRoot Node => _node;

        NodeBranch<IUIBranchObject, UIObject> IBranchObject<IUIBranchObject, UIObject>.Node => Node;

        #endregion



        #region Props 

        private readonly GraphicsDevice _graphics;
        private Viewport Viewport => _graphics.Viewport;


        public MVector Size => new(Viewport.Width, Viewport.Height);
        public IVector Position => new MVector(0);

        #endregion



        #region Constructors

        public Screen(MGame game)
        {
            _node = new UINodeRoot(this);
            _game = game;
            _graphics = game.GraphicsDevice;
        }

        #endregion

        

        #region Implement - IRectangle 

        public IRectangle AsRect => this;

        float IRectangle.Width => Viewport.Width;
        float IRectangle.Height => Viewport.Height;

        float IRectangle.CenterX => 0;
        float IRectangle.CenterY => 0;

        #endregion
    }
}
