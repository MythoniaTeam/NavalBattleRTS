


namespace Mythonia.Game.Objects.Draw
{
    /// <summary>
    /// 继承于 <seealso cref="NodeRoot{BranchType, LeaveType}"/>, 缩减程序长度
    /// <para>
    /// <see langword="where"/>
    /// <i><list type="bullet">
    /// <item>BranchType <see langword="is"/> <seealso cref="Layer"/></item>
    /// <item>LeaveType <see langword="is"/> <seealso cref="Sprite"/></item>
    /// </list></i>
    /// </para>
    /// <para>
    /// 构造函数中 包含 <seealso cref="InitArgs"/>[] 类型参数, <br/>
    /// 会自动按照参数列表构建下一级 <see cref="LayerNodeBranch"/>
    /// </para>
    /// </summary>
    public class LayerNodeRoot : NodeRoot<Layer, Sprite>
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
        public LayerNodeRoot(MGame game, Layer layerObj) : base(layerObj)
        {
            _game = game;
        }

        #endregion



        #region Override Methods 

        public override string ToString() => $"Layer \"{FullPath}\"";

        #endregion
    }
}
