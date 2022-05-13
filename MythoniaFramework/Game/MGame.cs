using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mythonia.Framework.Game
{
    public abstract class MGame : XNA.Game
    {
        public GraphicsDeviceManager Graphics { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public MContentsManager ContentsManager { get; set; }

        public Camera CurrentCamera { get; set; }
        public Screen Screen { get; set; }
        public DrawManager DrawManager { get; set; }

        public MGame()
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            ContentsManager = new(this);
            CurrentCamera = new(this, new(0));
            Screen = new(this);

            Window.AllowUserResizing = true;
        }


        protected abstract string[] _TextureLoadList { get; }
        /// <summary>
        /// <para>初始化图层时, 提供的数据, </para>
        /// <para><b>重载</b> 该字段以 <b>修改</b> 初始图层结构</para>
        /// <i>*直接以树状结构, 传递</i>
        /// (<see cref="string"/> name, <see cref="float"/> weight, <see cref="Layer.InitArgs"/>[] sublayers) 
        /// <i>即可, <br/> 将会自动转换为</i> 
        /// <see cref="Layer.InitArgs"/>, 
        /// <i>无需</i> <see langword="new"/>
        /// </summary>
        protected abstract Layer.InitArgs[] _LayerInitArgsList { get; }

        public LayerInfo _GetDefaultLayerInfo(INamed obj) => _GetDefaultLayerInfo(obj.Name);
        public virtual LayerInfo _GetDefaultLayerInfo(string name) => new("Game", 0, name);



        public SpriteFont DefaultFont;
        protected override void Initialize()
        {
            base.Initialize();
            Utility.Initialize(this);
            DefaultFont = Content.Load<SpriteFont>("Default");
        }

        protected override void LoadContent()
        {
            ContentsManager.LoadTextures(_TextureLoadList);

            DrawManager = new(this, _LayerInitArgsList);

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
        //    DrawMiddle(gameTime);
        //    DrawAfter(gameTime);
        //}

        //protected void DrawMiddle(GameTime gameTime)
        //{

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
                SpriteBatch.DrawString(DefaultFont, CurrentCamera.Scale.ToString(), Screen.Size / 2 + (0, 20), Color.Black  );
                SpriteBatch.DrawString(DefaultFont, CurrentCamera.Position.ToString(), Screen.Size / 2 + (0, 50), Color.Black);

            }
            if (key.IsKeyDown(Keys.LeftAlt))
            {
                this.Log(true, "MGame", "---------Alt--------");
            }



            SpriteBatch.DrawString(DefaultFont, $"Frame: {FrameCounter.FrameCount}", new(50, 50), Color.Black);
            SpriteBatch.DrawString(DefaultFont, $"FPS: {FrameCounter.AverageFPS}", new(50, 80), Color.Black);
            SpriteBatch.DrawString(DefaultFont, $"Mouse: {Mouse.GetState().Position}", new(50, 110), Color.Black);


            SpriteBatch.DrawString(DefaultFont, "Test", new(150, 280), Color.Black);


            //DrawManager.AddSpritesToDrawList();
            DrawManager.DrawAll(SpriteBatch);
        }

        //protected void DrawAfter(GameTime gameTime) { }


    }
}
