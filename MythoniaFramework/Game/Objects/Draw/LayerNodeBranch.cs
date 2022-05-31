


namespace Mythonia.Game.Objects.Draw
{
    /// <summary>
    /// 继承于 <seealso cref="NodeBranch{BranchType, LeaveType}"/> 
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
    public class LayerNodeBranch : NodeBranch<Layer, Sprite>
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
        /// <param name="weight"></param>
        public LayerNodeBranch(Layer layerObj, float weight) : base(layerObj, weight)
        {
            _game = layerObj.MGame;
        }

        #endregion

        

        #region Operators 

        /*public static implicit operator Layer
            ((string name, float weight) v)
            => new(v.name, v.weight);
        //public static implicit operator Layer
        //    ((string name, string path, float weight) v)
        //    => new(v.name, v.path, v.weight);
        public static implicit operator Layer
            ((string name, float weight, object sublayers) v) 
            => new(v.name, v.weight, (InitArgs[])v.sublayers);
        //public static implicit operator Layer
        //    ((string name, string path, float weight, object sublayers) v) 
        //    => new(v.name, v.path, v.weight, (InitArgs[])v.sublayers);*/



        //public static implicit operator Layer((string name, float weight) v) => new(v.name, new("", v.weight));
        //public static implicit operator Layer((string name, float weight, Layer[] layers) v) => new(v.name, new("", v.weight), v.layers);

        #endregion



        #region Override Methods 

        public override string ToString() => $"Layer \"{FullPath}\"";

        #endregion
    }
}
