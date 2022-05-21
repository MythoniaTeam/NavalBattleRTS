



namespace Mythonia.Resources.Interfaces
{
    /// <summary>
    /// 用于绝大部分会实例化的对象类
    /// <para>包含以下字段:</para>
    /// <b><see cref="Name"/> </b> - <inheritdoc cref="Name"/><br/>
    /// <b><see cref="MGame"/></b> - <inheritdoc cref="MGame"/>
    /// 
    /// </summary>
    public interface IMClass
    {
        /// <summary>
        /// 对象的名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 对象所属的 <see cref="Game.MGame"/> 游戏类实例
        /// </summary>
        MGame MGame { get; }
    }
}
