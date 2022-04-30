

namespace MythoniaGameMain
{
    public class GameMain : MGame
    {

        protected override string[] TextureLoadList()
            => new string[]
            {
                "RECTANGLE",
                "RECTANGLE_RED"
            };

        public GameMain()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        SpriteFont tSpriteFont;
        Texture2D tSprite;
        protected override void Initialize()
        {

            //IsFixedTimeStep = false;
            base.Initialize();

            tSpriteFont = Content.Load<SpriteFont>("Default");
            tSprite = Content.Load<Texture2D>(@"Images\RECTANGLE");

        }

        protected override void LoadContent()
        {
            base.LoadContent();
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

            SpriteBatch.Begin();

            base.Draw(gameTime);

            if(FrameCounter.FrameCount > 60)
                SpriteBatch.DrawString(tSpriteFont, ((TObject)Components[0]).Position.X.ToString(), new(50, 64), Color.Black);
            SpriteBatch.DrawString(tSpriteFont, $"Frame: {FrameCounter.FrameCount}", new(50, 50), Color.Black);
            SpriteBatch.DrawString(tSpriteFont, $"FPS: {FrameCounter.AverageFPS}", new(50, 150), Color.Black);

            SpriteBatch.DrawString(tSpriteFont, "Test", new(150, 350), Color.Black);
            SpriteBatch.Draw(tSprite, new Vector2(150, 350), Color.White);

            
            

            SpriteBatch.End();


            // TODO: Add your drawing code here

        }
    }
}
