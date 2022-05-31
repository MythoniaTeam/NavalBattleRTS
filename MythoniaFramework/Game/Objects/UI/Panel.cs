



namespace Mythonia.Game.Objects.UI
{
    public abstract class Panel : IUIBranchObject, IBranchObject<IUIBranchObject, UIObject>, IUIObject
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



        #region Implement - IBranchObject <BranchType, LeaveType>

        private readonly UINodeBranch _node;
        public UINodeBranch Node => _node;

        NodeBranch<IUIBranchObject, UIObject> IBranchObject<IUIBranchObject, UIObject>.Node => Node;

        #endregion



        #region Props 


        #endregion Props



        #region Constructors

        public Panel(MGame game, string name, float weight)
        {
            _name = name;
            _game = game;
            _node = new UINodeBranch(this, weight);
        }

        #endregion



        #region Methods 

        protected void Add(IUIBranchObject uiObject)
        {
            Node.Add(uiObject.Node);
        }



        #region Abstract / Virtual Methods

        /// <summary>
        /// 用于初始化面板, 添加 子UI部件 (<see cref="Panel"/> 和 <seealso cref="UIObject"/>)
        /// </summary>
        public virtual void Initialize()
        {

        }

        public virtual void LoseFocus()
        {

        }



        #endregion



        

        #endregion Methods

        

        #region Implement - IUIObject

        protected bool _wasClick;
        bool IUIObject.WasClick { get => _wasClick; set => _wasClick = value; }

        protected bool _wasTouch;
        bool IUIObject.WasTouch { get => _wasTouch; set => _wasTouch = value; }



        #region Abstract Methods

        public virtual void TouchAction() { }
        public virtual void DeTouchAction() { }
        public virtual void HoverAction() { }
        public virtual void ClickAction() { }

        #endregion

        #endregion



        #region Implement - IRectangle 

        public IRectangle AsRect => this;

        float IRectangle.CenterX => throw new NotImplementedException();
        float IRectangle.CenterY => throw new NotImplementedException();
        float IRectangle.Height => throw new NotImplementedException();
        float IRectangle.Width => throw new NotImplementedException();


        #endregion

    }
}
