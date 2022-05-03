


namespace Mythonia.Framework.Game.Objects
{
    internal class TUI : UIObject
    {
        public TUI(MGame game, string name, MVector align) : base(game, name, align, new(50,30))
        {
            this.LogConstruct(true);
            SpriteObject = new Sprite(MGame, "TUISprite", game.ContentsManager["RECTANGLE"], null, null, false, new(2));
        }
    }
}
