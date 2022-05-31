



namespace Mythonia.Game.Objects.Draw
{

    /// <summary>
    /// 继承于 <seealso cref="NodeLeave{BranchType, LeaveType}"/>, 缩减程序长度
    /// <para>
    /// <see langword="where"/>
    /// <i><list type="bullet">
    /// <item>BranchType <see langword="is"/> <seealso cref="Layer"/></item>
    /// <item>LeaveType <see langword="is"/> <seealso cref="Sprite"/></item>
    /// </list></i>
    /// </para>
    /// </summary>
    public class LayerNodeLeave : NodeLeave<Layer, Sprite>
    {

        #region Constructor

        public LayerNodeLeave(Sprite leaveObj, float weight) : base(leaveObj, weight) { }

        #endregion

    }
}
