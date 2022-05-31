


namespace Mythonia.Game.Objects.Draw
{
    public class DrawManager : Tree<Layer, Sprite>
    {
        MGame Game { get; set; }

        public LayerNodeRoot Layers => (LayerNodeRoot)Root;


        public DrawManager(MGame game, Layer.InitArgs[] layers) : base((LayerNodeRoot) new Layer(game, layers).Node)
        {
            Game = game;
            //new Layer(game, layers, out _layers);
        }

        /// <summary>
        /// 把全部 Sprite 的 DrawSprite函数 添加到 DrawList
        /// </summary>
        public List<Action<SpriteBatch>> AddSpritesToDrawList()
        {
            List<Action<SpriteBatch>> drawList = new();
            int count = Layers.LeavesCount;
            float i = 0;
            ICollection<NodeLeave<Layer, Sprite>> sprites = Layers.GetAllLeaves();
            foreach (LayerNodeLeave sprite in sprites)
            {
                //sprite.DrawSprite(Game.CurrentCamera, Game.SpriteBatch, 0.5f);// (count - i) / count);
                drawList.Add(
                    new Action<SpriteBatch>(spriteBatch =>
                        sprite.LeaveObj.DrawSprite(Game.CurrentCamera, spriteBatch, (count - i) / count)
                    )
                );
                //this.Log(true, "DrawManager", $"{sprite.Name} - layer depth: {(count - i) / count}");
                i++;
            }
            return drawList;
        }

        /// <summary>
        /// 绘制 DrawList 内的全部 Sprite对象
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawAll(SpriteBatch spriteBatch)
        {
            var list = AddSpritesToDrawList();
            foreach (Action<SpriteBatch> draw in list)
            {
                draw.Invoke(spriteBatch);
            }
        }


       
    }
}
