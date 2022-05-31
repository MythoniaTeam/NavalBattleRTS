



namespace Mythonia.Game.Objects
{
    public abstract class MObject : DrawableGameComponent, IMClass
    {



        #region Implement - IMClass 

        private readonly string _name;
        public string Name => _name;
        public MGame MGame => (MGame) Game;

        #if DEBUG
        #nullable enable
        Type? IMClass.TypeRecord { get; set; }
        #endif

        #endregion



        #region Props 

        protected MActionManager Actions = new MActionManager();
        protected SpriteBatch SpriteBatch => MGame.SpriteBatch;

        public MObject (MGame game, string name) : base(game)
        {
            _name = name;
        }

        #endregion



        #region Methods

        public override void Initialize()
        {
            base.Initialize();
        }


        /// <summary>
        /// !!! 子类不应该引用Base.Update(..)而是分别引用Base.UpdateBefore(..)和UpdateAfter(..)
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            UpdateBefore(gameTime);
            UpdateAfter(gameTime);
        }        

        protected virtual void UpdateBefore(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected virtual void UpdateAfter(GameTime gameTime)
        {
            Actions.Update(gameTime);
            if (this is IDrawModule obj) obj.SpriteObject.UpdateSprite(gameTime);
        }

        //protected void Draw(SpriteBatch spriteBatch, float layer)
        //{
        //    if (ContainsDrawModule) ((IDrawModule)this).SpriteObject.DrawSprite(MGame.CurrentCamera, spriteBatch, layer);

        //}

        //public override void Draw(GameTime gameTime)
        //{
        //    base.Draw(gameTime);
        //    Draw(MGame.SpriteBatch, 0);
        //}




        public override string ToString() => $"MObject \"{Name}\"";

        #endregion

    }
}
