
using Microsoft.Xna.Framework.Input;

namespace Mythonia.Game.Objects
{
    public class TObject : GameObject
    {



        public TObject(MGame game, string name, MVector? position, string layer, float layerWeight) : base(game, name, position)
        {
            this.LogConstruct(true);
            //SpriteObject = new Sprite("TObjectSprite", game.ContentsManager["RECTANGLE"], Position);
            SpriteObject = 
                new Sprite(
                    MGame, $"TObject\"{name}\"Sprite",
                    texture: game.ContentsManager.GetAnimatedTexture("BouncingBomb"), 
                    getOriginPosMethod: () => Position,
                    isWorldPos: true,
                    isGameObject: true,
                    layerInfo: new(layer, layerWeight, name),
                    scale: new(4)
                    );
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
        private float speedCamZoom = 1.1f;
        private float maxCamZoom = 8;
        private float minCamZoom = 0.125f;


        public override void Update(GameTime gameTime)
        {
            base.UpdateBefore(gameTime);


            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.D       )) /*Debug.WriteLine("KeyDown W       ");*/Position.Vec += (   speed * gameTime.ElapsedGameTime.ToStandardFrame(), 0);
            if (key.IsKeyDown(Keys.A       )) /*Debug.WriteLine("KeyDown S       ");*/Position.Vec -= (   speed * gameTime.ElapsedGameTime.ToStandardFrame(), 0);
            if (key.IsKeyDown(Keys.W       )) /*Debug.WriteLine("KeyDown D       ");*/Position.Vec += (0, speed * gameTime.ElapsedGameTime.ToStandardFrame()   );
            if (key.IsKeyDown(Keys.S       )) /*Debug.WriteLine("KeyDown A       ");*/Position.Vec -= (0, speed * gameTime.ElapsedGameTime.ToStandardFrame()   );
            if (key.IsKeyDown(Keys.Right   )) /*Debug.WriteLine("KeyDown Up      ");*/MGame.CurrentCamera.Position.Vec += (   speedCam * gameTime.ElapsedGameTime.ToStandardFrame(), 0);
            if (key.IsKeyDown(Keys.Left    )) /*Debug.WriteLine("KeyDown Down    ");*/MGame.CurrentCamera.Position.Vec -= (   speedCam * gameTime.ElapsedGameTime.ToStandardFrame(), 0);
            if (key.IsKeyDown(Keys.Up      )) /*Debug.WriteLine("KeyDown Right   ");*/MGame.CurrentCamera.Position.Vec += (0, speedCam * gameTime.ElapsedGameTime.ToStandardFrame()   );
            if (key.IsKeyDown(Keys.Down    )) /*Debug.WriteLine("KeyDown Left    ");*/MGame.CurrentCamera.Position.Vec -= (0, speedCam * gameTime.ElapsedGameTime.ToStandardFrame()   );
            if (key.IsKeyDown(Keys.PageUp  )) /*Debug.WriteLine("KeyDown PageUp  ");*/MGame.CurrentCamera.Scale *= MathF.Pow(speedCamZoom, gameTime.ElapsedGameTime.ToStandardFrame());
            if (key.IsKeyDown(Keys.PageDown)) /*Debug.WriteLine("KeyDown PageDown");*/MGame.CurrentCamera.Scale /= MathF.Pow(speedCamZoom, gameTime.ElapsedGameTime.ToStandardFrame());

            if (MGame.CurrentCamera.Scale > maxCamZoom) MGame.CurrentCamera.Scale = new(maxCamZoom);
            if (MGame.CurrentCamera.Scale < minCamZoom) MGame.CurrentCamera.Scale = new(minCamZoom);

            base.UpdateAfter(gameTime);
            this.Log(false, "Position: " + Position);
        }


        
    }
}
