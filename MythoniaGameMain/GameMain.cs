


namespace MythoniaGameMain
{
    public class GameMain : MGame
    {


        protected override string[] _TextureLoadList
            => new string[]
            {
                "RECTANGLE",
                "RECTANGLE_RED",
                "BouncingBomb"
            };

        protected override Layer.InitArgs[] _LayerInitArgsList
            => new Layer.InitArgs[]
            {
                ("Background", 0),
                ("Game", 5, new Layer.InitArgs[] 
                {
                    ("Scenery", 0),
                    ("Entity", 5),
                    ("Projectile", 10)
                }),
                ("UI", 10),
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

            if (FrameCounter.FrameCount == 60)
            {
                Components.Add(new TObject(this, "Test1", new(-20), "Game.Projectile", 10));
                Components.Add(new TObject(this, "Test2", new(0), "Game.Entity", 10));
                Components.Add(new TObject(this, "Test3", new(20), "Game.Projectile", 10));

                Components.Add(new TOutputObject(this, "Test1", 2));

                Components.Add(new TUI(this, "Test", VecDir.BottomRight, "UI", 10));
                Components.Add(new TUI(this, "Test", VecDir.Bottom, "UI", 10));
                Components.Add(new TUI(this, "Test", VecDir.BottomLeft, "UI", 10));
                Components.Add(new TUI(this, "Test", VecDir.Right, "UI", 10));
                Components.Add(new TUI(this, "Test", VecDir.Center, "UI", 10));
                Components.Add(new TUI(this, "Test", VecDir.Left, "UI", 10));
                Components.Add(new TUI(this, "Test", VecDir.TopRight, "UI", 10));
                Components.Add(new TUI(this, "Test", VecDir.Top, "UI", 10));
                Components.Add(new TUI(this, "Test", VecDir.TopLeft, "UI", 10));
                Components.Add(new TUI(this, "Test", (0.5f, 0.5f), "UI", 10));


                //Components[0].Initialize();
            }


        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);

            base.Draw(gameTime);


            if (FrameCounter.FrameCount > 60)
                SpriteBatch.DrawString(DefaultFont, ((TObject)Components[0]).Position.ToString(), new(50, 300), Color.Black);
            //tSprite = Content.Load<Texture2D>(@"Images\BouncingBomb");

            SpriteBatch.Draw(tSprite, new Vector2(150, 400), Color.White);




            SpriteBatch.End();

        }
    }
}
