using System;
using System.Collections.Generic;
using System.Text;


namespace Mythonia.Game.Objects
{
    public abstract class UIObject : MObject, IDrawModule, IRectangle
    {

        public MVector ScreenPosition => AsRect.Position;

        public Sprite SpriteObject { get; set; }


        public UIObject(MGame game, string name,
            MVector? align = null, MVector? offset = null, MVector? origin = null) :base(game, name)
        {
            //if (this is not IRectangle) throw new NotImplementedException("UIObject should implementing IRectangle");
            
        }

        public override void Update(GameTime gameTime)
        {
            UpdateBefore(gameTime);
            UpdateAfter(gameTime);
        }

        protected override void UpdateAfter(GameTime gameTime)
        {
            SpriteObject.UpdateSprite(gameTime);
            base.UpdateAfter(gameTime);
        }


        public override string ToString() => $"UIObject \"{Name}\"";



        //---------- Implement - IRectangle ----------

        public IRectangle AsRect => this;

        //float? IRectangle.WidthSource => 100;
    }
}
