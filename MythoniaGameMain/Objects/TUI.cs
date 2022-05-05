


namespace Mythonia.Framework.Game.Objects
{
    internal class TUI : UIObject
    {
        public TUI(MGame game, string name, MVector align, string layer, float layerWeight) : base(game, name, align, new(50,30))
        {
            this.LogConstruct(true);
            SpriteObject = 
                new Sprite(
                    MGame, "TUISprite", 
                    game.ContentsManager["RECTANGLE"], 
                    () => ScreenPosition, false, 
                    new LayerInfo(layer, layerWeight, name), new(2));
        }
    }
}
