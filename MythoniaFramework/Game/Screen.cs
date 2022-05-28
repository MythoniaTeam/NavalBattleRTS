


namespace Mythonia.Game
{
    public class Screen : IRectangle, IMClass
    {

        #region Implement - IMClass 
        public string Name => "Screen";
        private readonly MGame _game;
        public MGame MGame => _game;

        #endregion

        //123abcd321

        #region Props 
        
        private readonly GraphicsDevice _graphics;
        private Viewport Viewport => _graphics.Viewport;


        public MVector Size => new(Viewport.Width, Viewport.Height);
        public IVector Position => new MVector(0);

        #endregion



        #region Constructors

        public Screen(MGame game)
        {
            _game = game;
            _graphics = game.GraphicsDevice;
        }

        #endregion

        

        #region Implement - IRectangle 

        public IRectangle AsRect => this;

        float IRectangle.Width => Viewport.Width;
        float IRectangle.Height => Viewport.Height;

        float IRectangle.CenterX => 0;
        float IRectangle.CenterY => 0;

        #endregion
    }
}
