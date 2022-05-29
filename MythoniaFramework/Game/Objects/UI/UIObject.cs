



namespace Mythonia.Game.Objects.UI
{
    /// <summary>
    /// UI对象类, 负责处理 位置, 绘制UI 等属性和行为
    /// <para>
    /// 存储在 <seealso cref="UI"/> 类 当中, 可以通过其访问该对象
    /// </para>
    /// </summary>
    public abstract class UIObject : MObject, IDrawModule
    {

        public Sprite SpriteObject { get; set; }


        public UIObject(MGame game, string name) :base(game, name)
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



        
    }
}
