



namespace Mythonia.Game.Objects.UI
{
    /// <summary>
    /// 作为 UINode 中, BranchType 的接口.
    /// <para>
    /// <b>继承: </b> 
    /// <seealso cref="IRectangle"/> 和 
    /// <seealso cref="IBranchObject{BranchType, LeaveType}"/> (<see cref="IUIBranchObject"/>, <see cref="UIObject"/>)<br/>
    /// </para> <br/>
    /// <para>
    /// <b>被继承: </b><seealso cref="Screen"/> 和 <seealso cref="UIObject"/>
    /// </para>
    /// </summary>
    public interface IUIBranchObject : IRectangle, IBranchObject<IUIBranchObject, UIObject>, IUIObject
    {
        

    }

}
