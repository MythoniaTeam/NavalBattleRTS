



namespace Mythonia.Framework.Game.Objects
{
    public class MObject : DrawableGameComponent, INamed
    {
        private string _name;
        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public MPosition Position;


        protected MActionManager Actions = new MActionManager();
        protected SpriteBatch SpriteBatch => MGame.SpriteBatch;
        protected MGame MGame => (MGame)Game;


        public MObject (MGame game, string name, MVector? position) : base(game)
        {
            Name = name;
            Position = position ?? new(0);
        }

        private bool ContainsDrawModule = false;
        public override void Initialize()
        {
            base.Initialize();
            if (this is Draw.IDrawModule) ContainsDrawModule = true;
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

        protected void UpdateBefore(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected void UpdateAfter(GameTime gameTime)
        {
            Actions.Update(gameTime);
            if (ContainsDrawModule) ((Draw.IDrawModule)this).SpriteObject.Update(gameTime);
        }

        protected void Draw(SpriteBatch spriteBatch, float layer)
        {
            if (ContainsDrawModule) ((Draw.IDrawModule)this).SpriteObject.Draw(spriteBatch, layer);

        }

    }
}
