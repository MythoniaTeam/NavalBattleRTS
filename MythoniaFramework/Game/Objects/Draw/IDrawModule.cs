using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    public interface IDrawModule : XNA.IDrawable
    {
        Sprite SpriteObject { get; }
    }
}
