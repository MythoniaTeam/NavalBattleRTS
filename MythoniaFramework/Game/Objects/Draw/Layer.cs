



namespace Mythonia.Game.Objects.Draw
{
    public class Layer : IBranchObject<Layer, Sprite>
    {

        #region Implement - IMClass 

        private readonly string _name;
        public string Name => _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        #if DEBUG
        #nullable enable
        Type? IMClass.TypeRecord { get; set; }
        #endif

        #endregion



        #region Implement - ILeaveObject <BranchType, LeaveType>

        private readonly NodeBranch<Layer, Sprite> _node;
        public NodeBranch<Layer, Sprite> Node => _node;

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



        #region Constructors
        private Layer(MGame game, string name)
        {
            _game = game;
            _name = name;
        }

        /// <summary>
        /// 初始化根图层用
        /// </summary>
        /// <param name="game"></param>
        /// <param name="name"></param>
        /// <param name="layers"></param>
        public Layer(MGame game, InitArgs[] layers/*, out LayerNodeRoot nodeRoot*/) : this(game, "#ROOTLAYER#")
        {
            //nodeRoot = new LayerNodeRoot(game, this);
            //_node = nodeRoot;
            _node = new LayerNodeRoot(game, this);
            ConstructSublayers(game, layers);
        }

        public Layer(MGame game, string name, LayerInfo? layerInfo = null, InitArgs[] sublayers = null) : this(game, name)
        {

            LayerInfo layer = layerInfo ?? game._GetDefaultLayerInfo(name);
            _node = new LayerNodeBranch(this, layer.Weight);            
            game.DrawManager.Layers.TryAdd(_node, layer.Path);

            ConstructSublayers(game, sublayers);
        }

        /// <summary>
        /// 被父亲调用生成新的实例, 无需按照LayerInfo 将 Node 添加到 <see cref="DrawManager"/>, 父亲会自动添加Sublayer.Node
        /// </summary>
        /// <param name="game"></param>
        /// <param name="name"></param>
        /// <param name="weight"></param>
        /// <param name="sublayers"></param>
        private Layer(MGame game, string name, float? weight, InitArgs[] sublayers = null) : this(game, name)
        {
            _game = game;
            _name = name;

            float weight2 = weight ?? game._GetDefaultLayerInfo(name).Weight;
            _node = new LayerNodeBranch(this, weight2);

            ConstructSublayers(game, sublayers);
        }

        private void ConstructSublayers(MGame game, InitArgs[] sublayers)
        {
            if (sublayers is null) return;
            foreach (InitArgs sublayer in sublayers)
            {
                Layer newlayer = new(game, sublayer.Name, sublayer.Weight, sublayer.SubLayers);
                Node.Add(newlayer.Node);
            }
        }

        


        #endregion

    }
}
