


namespace Mythonia.Game.Objects.Draw
{
    public class LayerRoot : NodeRoot<Sprite>, ILayer
    {

        #region Implement - IMClass 

        //Name 已经在 Node 中实现了

        private readonly MGame _game;
        public MGame MGame => _game;

        #endregion



        #region Props 

        /// <summary>
        /// <inheritdoc cref="NodeBranch{LeaveType}.TryFindChildBranch(string)"/>
        /// <para>
        /// <b>参见: </b> <seealso cref="NodeBranch{LeaveType}.TryFindChildBranch(string)"/>
        /// </para>
        /// </summary>
        /// <param name="requestName"></param>
        /// <returns></returns>
        public Layer this[string requestName] => (Layer)TryFindChildBranch(requestName);

        #endregion



        #region Constructor 

        /// <summary>
        /// 初始化一个图层
        /// </summary>
        /// <param name="name">当前图层的名字</param>
        /// <param name="path">所属图层的路径</param>
        /// <param name="weight"></param>
        /// <param name="sublayers"></param>
        public LayerRoot(MGame game, Layer.InitArgs[] sublayers = null) : base()
        {
            _game = game;

            if (sublayers != null)
            {
                foreach (Layer.InitArgs sublayer in sublayers)
                {
                    Add(new Layer(
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
