


namespace Mythonia.Game.Objects
{
    internal class TUI : UIObject, IAlignedRectangle
    {

        #region Constructors

        public TUI(MGame game, string name, MVector align, string layerPath, float layerWeight) : base(game, name)
        {
            _align = align;

            this.LogConstruct(true);
            SpriteObject = 
                new Sprite(
                    MGame, "TUISprite", 
                    texture: game.ContentsManager["RECTANGLE"], 
                    refObj: this,
                    isWorldPos: false, 
                    layerInfo: new(layerPath, layerWeight),
                    scale: new(2)
                    );
        }

        #endregion



        #region Implement - IRectangle 

        public IRectangle AsRect => this;

        private readonly MVector _align;

        IRectangle IAlignedRectangle.RefObj => MGame.Screen.AsRect;
        MVector IAlignedRectangle.RefAlignPosScale => _align;
        MVector IAlignedRectangle.RefAlignPosDisplacement => (30, 20);




        float IRectangle. Width => SpriteObject.TextureDrawSize.X;
        float IRectangle.Height => SpriteObject.TextureDrawSize.Y;

        //float IRectangle.Top => PointTL.Y;
        //float IRectangle.Left => PointTL.X;

        //float IRectangle.CenterX => AsRect.Left + AsRect.Width / 2;
        //float IRectangle.CenterY => AsRect.Top  - AsRect.Height / 2;

        //private MVector PointTL => MGame.Screen.AsRect.DirectFrom(VecDir.TopLeft).To(VecDir.Center).For(30, 15);

        #endregion

    }
}
