using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game
{
    public class Screen : IRectangle
    {
        private readonly MGame _game;
        private readonly GraphicsDevice _graphics;
        private Viewport Viewport => _graphics.Viewport;


        public MVector Size => new(Viewport.Width, Viewport.Height);
        public IVector Position => new MVector(0);

        


        public Screen(MGame game)
        {
            _game = game;
            _graphics = game.GraphicsDevice;
        }


        //---------- Implement - IRectangle ----------

        public IRectangle AsRect => this;

        float? IRectangle.WidthSource => Viewport.Width;
        float? IRectangle.HeightSource => Viewport.Height;

        float? IRectangle.XCenterSource => 0;
        float? IRectangle.YCenterSource => 0;

    }
}
