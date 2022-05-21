using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game.Objects
{
    public class GameObject : MObject, IDrawModule
    {
        public MPosition Position;

        public Sprite SpriteObject { get; set; }



        public GameObject(MGame game, string name, MVector? position) : base(game, name)
        {
            Position = position ?? new(0);
        }


        


        public override string ToString() => $"GameObject \"{Name}\"";
    }
}
