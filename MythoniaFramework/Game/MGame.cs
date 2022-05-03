using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mythonia.Framework.Game
{
    public abstract class MGame : XNA.Game
    {
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public MContentsManager ContentsManager;
        public Camera CurrentCamera;

        public MGame()
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            ContentsManager = new(this);
            CurrentCamera = new(this, new(0));

            Window.AllowUserResizing = true;
        }


        protected abstract string[] TextureLoadList();

        public SpriteFont DefaultFont;
        protected override void Initialize()
        {
            base.Initialize();
            Utility.Initialize(this);
            DefaultFont = Content.Load<SpriteFont>("Default");
        }

        protected override void LoadContent()
        {
            ContentsManager.LoadTextures(TextureLoadList());

            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            FrameCounter.UpdateNewFrame(gameTime);
            Debug.WriteLine("");
            Debug.WriteLine("----------------------------------------------- new frame -----------------------------------------------");
            Debug.WriteLine($"Updating frame {FrameCounter.FrameCount},".PadRight(22) + $"elapse time: {gameTime.ElapsedGameTime.ToStandardFrame()}(F)".PadRight(18) + $" / {gameTime.ElapsedGameTime.TotalSeconds}(s)");

            this.Log(false, $"Updating frame {FrameCounter.FrameCount},".PadRight(22) + $"elapse time: {gameTime.ElapsedGameTime.ToStandardFrame()}(F)".PadRight(18) + $" / {gameTime.ElapsedGameTime.TotalSeconds}(s)");
            Debug.WriteLine("");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Utility.DrawLineX(1, -9999, 9999, 0, Color.White, 3);
            Utility.DrawLineX(1, -9999, 9999, 200, Color.White, 1);
            Utility.DrawLineX(1, -9999, 9999, -200, Color.White, 1);
            Utility.DrawLineY(1, -9999, 9999, 0, Color.White, 3);
            Utility.DrawLineY(1, -9999, 9999, 200, Color.White, 1);
            Utility.DrawLineY(1, -9999, 9999, -200, Color.White, 1);

            base.Draw(gameTime);

            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.PageDown) || key.IsKeyDown(Keys.PageUp) ||
                key.IsKeyDown(Keys.Down) || key.IsKeyDown(Keys.Up) ||
                key.IsKeyDown(Keys.Left) || key.IsKeyDown(Keys.Right) ||
                key.IsKeyDown(Keys.LeftAlt))
            {
                Utility.DrawLineX(2, 200, -200, 0, Color.White, 1, true);
                Utility.DrawLineY(2, 200, -200, 0, Color.White, 1, true);
                SpriteBatch.DrawString(DefaultFont, CurrentCamera.Scale.ToString(), GraphicsDevice.Viewport.Size() / 2 + (0, 20), Color.Black  );
                SpriteBatch.DrawString(DefaultFont, CurrentCamera.Position.ToString(), GraphicsDevice.Viewport.Size() / 2 + (0, 50), Color.Black);

            }



            SpriteBatch.DrawString(DefaultFont, $"Frame: {FrameCounter.FrameCount}", new(50, 50), Color.Black);
            SpriteBatch.DrawString(DefaultFont, $"FPS: {FrameCounter.AverageFPS}", new(50, 150), Color.Black);

            SpriteBatch.DrawString(DefaultFont, "Test", new(150, 280), Color.Black);
            
        }
    }
}
