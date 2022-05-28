


namespace Mythonia.Game.Objects.Draw
{
    public interface ILayer : IMClass
    {
        #region Props 

        /// <summary>
        /// <inheritdoc cref="NodeBranch{LeaveType}.TryFindChildBranch(string)"/>
        /// <para>
        /// <b>参见: </b> <seealso cref="NodeBranch{LeaveType}.TryFindChildBranch(string)"/>
        /// </para>
        /// </summary>
        /// <param name="requestName"></param>
        /// <returns></returns>
        public Layer this[string requestName] { get; }

        #endregion
    }
}
