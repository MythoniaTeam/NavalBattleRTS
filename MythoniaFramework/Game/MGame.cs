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

        public MGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            ContentsManager = new(this);
        }


        protected abstract string[] TextureLoadList();


        protected override void Initialize()
        {

            base.Initialize();
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

            FrameCounter.UpdateNewFrame();
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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
