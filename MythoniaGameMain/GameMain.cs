

namespace MythoniaGameMain
{
    public class GameMain : MGame
    {

        protected override string[] TextureLoadList()
            => new string[]
            {
                "RECTANGLE",
                "RECTANGLE_RED",
                "BouncingBomb"
            };

        public GameMain()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        
        Texture2D tSprite;
        protected override void Initialize()
        {
            
            //IsFixedTimeStep = false;
            base.Initialize();

            
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

            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);

            base.Draw(gameTime);


            if (FrameCounter.FrameCount > 60)
                SpriteBatch.DrawString(DefaultFont, ((TObject)Components[0]).Position.X.ToString(), new(50, 72), Color.Black);
            SpriteBatch.Draw(tSprite, new Vector2(150, 400), Color.White);




            SpriteBatch.End();


            // TODO: Add your drawing code here

        }
    }
}
