
using Mythonia.Framework.Game.Objects.Draw;
using Microsoft.Xna.Framework.Input;

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

            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(20), n => Position.X += n.Value));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(20), n => Position.Y += n.Value));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(50),  n => Position.X += n.Value)); 
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(20), n => Position.X += n.Value));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(20), n => Position.Y += n.Value));
            Actions.Add(new MAction.MAction<ActionArgs.Numerical>(100, new(50),  n => Position.X += n.Value));
            
        }

        private float speed = 1.2f;
        private float speedCam = 3f;
        private float speedCamZoom = 0.02f;

        public override void Update(GameTime gameTime)
        {
            base.UpdateBefore(gameTime);


            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.D       )) /*Debug.WriteLine("KeyDown W       ");*/Position += (   speed * gameTime.ElapsedGameTime.ToStandardFrame(), 0);
            if (key.IsKeyDown(Keys.A       )) /*Debug.WriteLine("KeyDown S       ");*/Position -= (   speed * gameTime.ElapsedGameTime.ToStandardFrame(), 0);
            if (key.IsKeyDown(Keys.W       )) /*Debug.WriteLine("KeyDown D       ");*/Position += (0, speed * gameTime.ElapsedGameTime.ToStandardFrame()   );
            if (key.IsKeyDown(Keys.S       )) /*Debug.WriteLine("KeyDown A       ");*/Position -= (0, speed * gameTime.ElapsedGameTime.ToStandardFrame()   );
            if (key.IsKeyDown(Keys.Right   )) /*Debug.WriteLine("KeyDown Up      ");*/MGame.CurrentCamera.Position += (   speedCam * gameTime.ElapsedGameTime.ToStandardFrame(), 0);
            if (key.IsKeyDown(Keys.Left    )) /*Debug.WriteLine("KeyDown Down    ");*/MGame.CurrentCamera.Position -= (   speedCam * gameTime.ElapsedGameTime.ToStandardFrame(), 0);
            if (key.IsKeyDown(Keys.Up      )) /*Debug.WriteLine("KeyDown Right   ");*/MGame.CurrentCamera.Position += (0, speedCam * gameTime.ElapsedGameTime.ToStandardFrame()   );
            if (key.IsKeyDown(Keys.Down    )) /*Debug.WriteLine("KeyDown Left    ");*/MGame.CurrentCamera.Position -= (0, speedCam * gameTime.ElapsedGameTime.ToStandardFrame()   );
            if (key.IsKeyDown(Keys.PageUp  )) /*Debug.WriteLine("KeyDown PageUp  ");*/MGame.CurrentCamera.Scale += speedCamZoom * gameTime.ElapsedGameTime.ToStandardFrame();
            if (key.IsKeyDown(Keys.PageDown)) /*Debug.WriteLine("KeyDown PageDown");*/MGame.CurrentCamera.Scale -= speedCamZoom * gameTime.ElapsedGameTime.ToStandardFrame();


            base.UpdateAfter(gameTime);
            this.Log(true, "Position: " + Position);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteObject.Draw(MGame.CurrentCamera, SpriteBatch, 0);
        }

        
    }
}
