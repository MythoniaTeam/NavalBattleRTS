
using Mythonia.Game.Shapes;


namespace Mythonia.Game.Objects
{
    internal class TUI : UIObject, IRectangle
    {

        public TUI(MGame game, string name, MVector align, string layer, float layerWeight) : base(game, name, align, new(50,30))
        {
            this.LogConstruct(true);
            SpriteObject = 
                new Sprite(
                    MGame, "TUISprite", 
                    texture: game.ContentsManager["RECTANGLE"], 
                    getOriginPosMethod: () => ScreenPosition,
                    isWorldPos: false, 
                    layerInfo: new(layer, layerWeight, name),
                    scale: new(2)
                    );
        }



        //---------- Implement - IRectangle ----------

        float? IRectangle.WidthSource => SpriteObject.TextureDrawSize.X;
        float? IRectangle.HeightSource => SpriteObject.TextureDrawSize.Y;

        private MVector PointTL => MGame.Screen.AsRect.DirectFrom(VecDir.TopLeft).To(VecDir.Center).For(30, 15);

        float? IRectangle.YTopSource => PointTL.Y;
        float? IRectangle.XLeftSource => PointTL.X;

    }
}
