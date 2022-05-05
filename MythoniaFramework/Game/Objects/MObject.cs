



namespace Mythonia.Framework.Game.Objects
{
    public abstract class MObject : DrawableGameComponent, INamed
    {
        private string _name;
        public string Name
        {
            get => _name;
            private set => _name = value;
        }



        protected MActionManager Actions = new MActionManager();
        protected SpriteBatch SpriteBatch => MGame.SpriteBatch;
        protected MGame MGame => (MGame)Game;


        public MObject (MGame game, string name) : base(game)
        {
            Name = name;
        }

        private bool ContainsDrawModule = false;
        public override void Initialize()
        {
            base.Initialize();
            if (this is IDrawModule) ContainsDrawModule = true;
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
            if (ContainsDrawModule) ((IDrawModule)this).SpriteObject.UpdateSprite(gameTime);
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


    }
}
