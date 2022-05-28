


namespace Mythonia.Game.Objects.Draw
{
    public class Layer : NodeBranch<Sprite>, ILayer
    {

        #region Implement - IMClass 

        //Name 已经在 Node 中实现了

        private readonly MGame _game;
        public MGame MGame => _game;

        #endregion



        #region Struct - InitArgs
        public struct InitArgs
        {
            public string Name;
            public float Weight;
            public InitArgs[] SubLayers;

            public InitArgs(string name, float weight, InitArgs[] sublayers)
            {
                Name = name;
                Weight = weight;
                SubLayers = sublayers;
            }

            public static implicit operator InitArgs
                ((string name, float weight, InitArgs[] sublayers) v) => new(v.name, v.weight, v.sublayers);
            public static implicit operator InitArgs
                ((string name, float weight) v) => new(v.name, v.weight, null);
        }

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
        public Layer(MGame game, string name, float weight, InitArgs[] sublayers = null) : base(name, weight)
        {
            _game = game;

            if (sublayers != null)
            {
                foreach (InitArgs sublayer in sublayers)
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
