using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    public abstract class TexturePainterBase : INamed
    {
        public string Name { get; private set; }

        private TextureBase Texture;
        private MVector Displacement;

        public MAngle Direction;

        public TexturePainterBase(string name, TextureBase texture, MVector? displacement = null)
        {
            Name = name;
            Texture = texture;
            Displacement = displacement ?? new(0);
        }

        public abstract void Draw(SpriteBatch spriteBatch, MVector position, MVector scale, Color color, MAngle rotation, SpriteEffects flip, float layer);

        protected void Draw(int frameNo, SpriteBatch spriteBatch, MVector position, MVector scale, Color color, MAngle rotation, SpriteEffects flip, float layer)
        {
            MAngle direction = rotation + Direction;
            MVector displacement = Displacement * scale;
            spriteBatch.Draw(
                Texture.GetSourceTexture(),
                ((rotation != 0 && displacement != 0) ? position + rotation.Rotation(displacement) : position),
                Texture.GetSourceRange(frameNo),
                color,
                direction,
                -displacement,
                scale,
                flip,
                layer
            );
            
            
        }
    }
}
