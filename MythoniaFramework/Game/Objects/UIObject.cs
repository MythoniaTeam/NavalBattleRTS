



namespace Mythonia.Game.Objects
{
    public abstract class UIObject : MObject, IDrawModule
    {

        public Sprite SpriteObject { get; set; }


        public UIObject(MGame game, string name) :base(game, name)
        {
            //if (this is not IRectangle) throw new NotImplementedException("UIObject should implementing IRectangle");
            
        }

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


        public override string ToString() => $"UIObject \"{Name}\"";



        
    }
}
