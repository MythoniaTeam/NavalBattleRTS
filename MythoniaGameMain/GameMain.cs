

namespace MythoniaGameMain
{
    public class GameMain : MGame
    {

        public GameMain()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        SpriteFont tSpriteFont;
        Texture2D tSprite;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //IsFixedTimeStep = false;
            base.Initialize();

            tSpriteFont = Content.Load<SpriteFont>("Default");
            tSprite = Content.Load<Texture2D>(@"Images\RECTANGLE");

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(FrameCounter.FrameCount == 60)
            {
                Components.Add(new TObject(this, "Test", new(0)));
                //Components[0].Initialize();
            }
            

        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            if(FrameCounter.FrameCount > 60)
                _spriteBatch.DrawString(tSpriteFont, ((TObject)Components[0]).Position.X.ToString(), new(50, 64), Color.Black);
            _spriteBatch.DrawString(tSpriteFont, $"Frame: {FrameCounter.FrameCount}", new(50, 50), Color.Black);
            _spriteBatch.DrawString(tSpriteFont, "Test", new(150, 150), Color.Black);
            _spriteBatch.Draw(tSprite, new Vector2(150, 150), Color.White);



            _spriteBatch.End();


            // TODO: Add your drawing code here

        }
    }
}
