using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Mythonia.Framework
{
    internal class TestObject : GameComponent
    {
        string _name;
        public TestObject(Game game, string name) : base(game)
        {
            _name = name;
        }

        int a = 0;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Debug.WriteLine($"{_name}: {a++}, {UpdateOrder}");
        }

        
    }
}
