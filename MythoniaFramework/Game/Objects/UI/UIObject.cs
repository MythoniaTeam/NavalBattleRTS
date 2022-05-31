



namespace Mythonia.Game.Objects.UI
{
    /// <summary>
    /// UI对象类, 负责处理 位置, 绘制UI 等属性和行为
    /// <para>
    /// 存储在 <seealso cref="UI"/> 类 当中, 可以通过其访问该对象
    /// </para>
    /// </summary>
    public abstract class UIObject : MObject, IDrawModule, ILeaveObject<IUIBranchObject, UIObject>, IUIObject
    {

        #region Implement - ILeaveObject <BranchType, LeaveType>

        private readonly UINodeLeave _node;
        public UINodeLeave Node => _node;

        NodeLeave<IUIBranchObject, UIObject> ILeaveObject<IUIBranchObject, UIObject>.Node => Node;

        #endregion



        #region Props

        public Sprite SpriteObject { get; set; }

        #endregion



        #region Constructors

        public UIObject(MGame game, string name) :base(game, name)
        {
            //if (this is not IRectangle) throw new NotImplementedException("UIObject should implementing IRectangle");
            
        }

        #endregion



        #region Methods



        #region Override Methods - GameComponent

        public override void Update(GameTime gameTime)
        {
            UpdateBefore(gameTime);
            UpdateAfter(gameTime);
        }

        protected override void UpdateAfter(GameTime gameTime)
        {
            SpriteObject.UpdateSprite(gameTime);
            base.UpdateAfter(gameTime);
        }

        #endregion



        #region Override Methods
        public override string ToString() => $"UIObject \"{Name}\"";
        #endregion



        #endregion



        #region Implement - IUIObject

        protected bool _wasClick;
        bool IUIObject.WasClick { get => _wasClick; set => _wasClick = value; }

        protected bool _wasTouch;
        bool IUIObject.WasTouch { get => _wasTouch; set => _wasTouch = value; }



        #region Abstract Methods

        public abstract bool TouchCheck(MVector mousePos);

        public abstract void TouchAction();
        public abstract void DeTouchAction();
        public abstract void HoverAction();
        public abstract void ClickAction();

        #endregion

        #endregion

    }
}
