



namespace Mythonia.Game.Objects.Interfaces
{
    public interface ITouchCheck
    {

        /// <summary>
        /// 检查点是否触碰到自身
        /// </summary>
        /// <param name="point"></param>
        /// <returns>
        /// <list type="table">
        /// <item><term><see langword="true"/></term> <description><paramref name="point"/> 触碰到自身</description></item>
        /// <item><term><see langword="false"/></term> <description><paramref name="point"/> 在自身之外</description></item>
        /// </list>
        /// </returns>
        public bool TouchCheck(MVector point);

    }
}
