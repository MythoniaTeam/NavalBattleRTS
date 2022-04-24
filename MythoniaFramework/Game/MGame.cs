using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mythonia.Framework.Game
{
    public class MGame : XNA.Game
    {
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        public MGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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
