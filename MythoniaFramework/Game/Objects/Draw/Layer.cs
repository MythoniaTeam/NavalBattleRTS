using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game.Objects.Draw
{
    public class Layer : List<ILayerItem>, ILayerItem
    {
        //---------- Implement - IMClass ----------

        private readonly string _name;
        /// <summary><inheritdoc/><para><i>Layer的名称前, 会加上 <b>字符 "#"</b></i></para></summary>
        public string Name => "#" + _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        //----------------------------------------



        //--------------- Props ---------------

        public LayerInfo LayerInfo { get; set; }


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


        protected ILayerItem this[string requestName]
        {
            get => Find(layer => layer.Name == requestName);
        }



        //--------------- Constructor ---------------

        /// <summary>
        /// 初始化一个图层
        /// </summary>
        /// <param name="name">当前图层的名字</param>
        /// <param name="path">所属图层的路径</param>
        /// <param name="weight"></param>
        /// <param name="sublayers"></param>
        public Layer(string name, string path, float weight, InitArgs[] sublayers = null)
        {
            if (path is null) path = "";
            _name = name;
            LayerInfo = new(path, weight, name);
            if (sublayers != null)
                foreach (InitArgs sublayer in sublayers)
                {
                    Add(new Layer(
                        sublayer.Name,
                        name != "#" ? ((path != "" ?  path + '.': "") + name) : "",
                        sublayer.Weight,
                        sublayer.SubLayers));
                }
        }



        //--------------- Methods ---------------

        /// <summary>
        /// 给定图层名称, 返回对应的子图层.
        /// </summary>
        /// <param name="requestName"></param>
        /// <returns>Layer对象 <b>名字符合</b> 的子图层, <br/>返回 <see langword="null"/> , 如果没有符合的对象.<br/>
        /// <i>注: 仅包含子图层, 不包括后代图层</i></returns>
        public Layer FindLayer(string requestName)
        {
            ILayerItem item = this['#' + requestName];
            if (item is Layer layer) return layer;
            return null;
        }


        /// <summary>
        /// 添加一个对象 (作为后代对象) , 会根据对象 LayerInfo 中的路径, 寻找到目标Layer并添加对象
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ILayerItem item)
        {
            FindLayerByPath(new(item.LayerInfo.Path.Split('.'))).AddToThis(item);
        }
        /// <summary>
        /// 添加一个对象到当前图层 (作为子对象)
        /// </summary>
        /// <param name="item"></param>
        private void AddToThis(ILayerItem item)
        {
            int index = FindIndex(v => v.LayerInfo.Weight > item.LayerInfo.Weight);
            Insert(index != -1 ? index : Count, item);
        }

        /// <summary>
        /// 给定路径 (String类型数组), 返回对应后代图层
        /// </summary>
        /// <param name="path"></param>
        /// <returns>路径所代表的 后代图层</returns>
        public Layer FindLayerByPath(List<string> path)
        {
            //如果路径Count == 0, 表示搜索到了尽头, 返回自己
            if (path.Count == 0) return this;

            //根据路径的第一项, 寻找对应子图层
            Layer layer = FindLayer(path[0]);
            if (layer is null) throw new NullReferenceException($"Layer \"{path[0]}\" is Not Found, Current Layer: {LayerInfo.FullPath}, Require Layer Path: {path}");

            //移除掉已经搜索过的 路径第一项
            path.RemoveAt(0);

            return layer.FindLayerByPath(path);
            
        }



        //---------- Implement - ILayerItem ----------

        public int ItemsCount()
        {
            int count = 0;
            foreach(ILayerItem item in this)
            {
                count += item.ItemsCount();
            }
            return count;
        }


        public ICollection<Sprite> GetLayerSprites()
        {
            List<Sprite> itemsList = new();
            foreach(ILayerItem item in this)
            {
                itemsList.AddRange(item.GetLayerSprites());
            }
            return itemsList;
        }



        //--------------- Operators ---------------

        public static implicit operator Layer
            ((string name, float weight) v)
            => new(v.name, "", v.weight);
        public static implicit operator Layer
            ((string name, string path, float weight) v)
            => new(v.name, v.path, v.weight);
        public static implicit operator Layer
            ((string name, float weight, object sublayers) v) 
            => new(v.name, "", v.weight, (InitArgs[])v.sublayers);
        public static implicit operator Layer
            ((string name, string path, float weight, object sublayers) v) 
            => new(v.name, v.path, v.weight, (InitArgs[])v.sublayers);



        //public static implicit operator Layer((string name, float weight) v) => new(v.name, new("", v.weight));
        //public static implicit operator Layer((string name, float weight, Layer[] layers) v) => new(v.name, new("", v.weight), v.layers);


        //---------- Override Methods ----------

        public override string ToString() => $"Layer, {LayerInfo}";
    }
}
