using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game
{
    public class Screen : IRectangle
    {
        private readonly MGame _game;
        private readonly GraphicsDevice _graphics;
        private Viewport Viewport => _graphics.Viewport;


        public MVector Size => new(Viewport.Width, Viewport.Height);
        public IVector Position => new MVector(0);

        public IRectangle AsRect => this;
        


        public Screen(MGame game)
        {
            _game = game;
            _graphics = game.GraphicsDevice;
        }

    }
}
