
using Mythonia.Framework.Game.Objects.Draw;

namespace Mythonia.Framework.Game.Objects
{
    public class TObject : MObject, IDrawModule
    {
        public Sprite SpriteObject { get; set; }



        public TObject(MGame game, string name, MVector? position) : base(game, name, position)
        {
            this.LogConstruct(true);
            Texture2D texture = game.Content.Load<Texture2D>("Images/RECTANGLE");
            SpriteObject = new Sprite("TObjectSprite", game.ContentsManager["RECTANGLE"], Position);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.Log(true, "is Initializing");

            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(200), n => Position.X += n.Value));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(200), n => Position.Y += n.Value));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(50),  n => Position.X += n.Value)); 
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(200), n => Position.X += n.Value));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(200), n => Position.Y += n.Value));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(50),  n => Position.X += n.Value));
            
        }

        public override void Update(GameTime gameTime)
        {
            base.UpdateBefore(gameTime);

            this.Log(true, "Position: " + Position);

            base.UpdateAfter(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteObject.Draw(SpriteBatch, 0);
        }

        
    }
}
