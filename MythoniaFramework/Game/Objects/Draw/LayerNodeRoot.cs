


namespace Mythonia.Game.Objects.Draw
{
    public class LayerNodeRoot : NodeRoot<LayerObject, Sprite>
    {

        #region Implement - IMClass 

        //Name 已经在 Node 中实现了

        private readonly MGame _game;
        public MGame MGame => _game;

        #endregion



        #region Constructor 

        /// <summary>
        /// 初始化一个图层
        /// </summary>
        /// <param name="name">当前图层的名字</param>
        /// <param name="path">所属图层的路径</param>
        /// <param name="weight"></param>
        /// <param name="sublayers"></param>
        public LayerNodeRoot(MGame game, LayerNodeBranch.InitArgs[] sublayers = null) : base(new(game, "#ROOT#"))
        {
            _game = game;

            if (sublayers != null)
            {
                foreach (LayerNodeBranch.InitArgs sublayer in sublayers)
                {
                    Add(new LayerNodeBranch(
                        game,
                        sublayer.Name,
                        sublayer.Weight,
                        sublayer.SubLayers));
                }
            }
        }

        #endregion



        #region Override Methods 

        public override string ToString() => $"Layer \"{FullPath}\"";

        #endregion
    }
}
