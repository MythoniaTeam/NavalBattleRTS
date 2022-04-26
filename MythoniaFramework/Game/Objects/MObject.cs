



namespace Mythonia.Framework.Game.Objects
{
    public class MObject : GameComponent, INamed
    {
        private string _name;
        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public MVector Position;

        protected MActionManager Actions = new MActionManager();

        public MObject (MGame game, string name, MVector? position) : base(game)
        {
            Name = name;
            Position = position ?? new(0);
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
        }
    }
}
