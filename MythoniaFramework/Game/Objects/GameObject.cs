using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects
{
    public class GameObject : MObject, Draw.IDrawModule
    {
        public MPosition Position;

        public Draw.SpriteBase SpriteObject { get; set; }



        public GameObject(MGame game, string name, MVector? position) : base(game, name)
        {
            Position = Position ?? new(0);
        }
    }
}
