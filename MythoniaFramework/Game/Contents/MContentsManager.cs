using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game.Contents
{
    public class MContentsManager : IMClass
    {
        //---------- Implement - IMClass ----------

        private readonly string _name;
        public string Name => _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        //----------------------------------------



        //------------- Props ---------------

        private Dictionary<string, TextureBase> Textures { get; set; } = new();


        /// <summary>
        /// 利用 [<see cref="string"/> <paramref name="textureName"/>] 索引器,<br/>
        /// <inheritdoc cref="GetTexture(string)"/>
        /// <para><i>
        /// 等效于 <seealso cref="GetTexture(string)"/>
        /// </i></para>
        /// </summary>
        /// <param name="textureName"></param>
        /// <returns></returns>
        public ITexture this[string textureName]
        {
            get => GetTexture(textureName);
        }



        //--------------- Constructor ---------------

        public MContentsManager(MGame game)
        {
            _game = game;
            _name = "ContentsManager";
        }



        //--------------- Methods ---------------

        /// <summary>
        /// 给定材质名称, 获取指定 <seealso cref="ITexture"/> 类型材质对象
        /// </summary>
        /// <param name="textureName">材质的名称</param>
        /// <returns>
        /// <list type="table">
        /// <item><term><seealso cref="TextureMono"/></term> <description><i>自身</i></description><br/>
        /// <i>其他继承 <seealso cref="ITexture"/> 的材质类型 同<seealso cref="TextureMono"/></i></item>
        /// <item><term><seealso cref="TextureSet"/></term> <description><i>包含自身的<seealso cref="AnimationPlayer"/>对象</i></description></item>
        /// </list>
        /// </returns>
        /// <exception cref="IncorrectTypeException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public ITexture GetTexture(string textureName, string aniName = null, float aniSpeed = 1) => Textures[textureName] switch
        {
            ITexture texture => texture,
            TextureSet textureSet => new AnimationPlayer(textureSet, aniName, aniSpeed),
            _ => throw new IncorrectTypeException($"The texture named \"{textureName}\"", Textures[textureName], 
                new Type[]{typeof(TextureMono), typeof(TextureSet), typeof(ITexture) })
        };

        public AnimationPlayer GetAnimatedTexture(string textureName, string aniName = null, float aniSpeed = 1) => Textures[textureName] switch
        {
            TextureSet textureSet => new AnimationPlayer(textureSet, aniName, aniSpeed),
            _ => throw new IncorrectTypeException($"The texture named \"{textureName}\"", Textures[textureName],
                new Type[] { typeof(TextureSet) })
        };



        /// <summary>
        /// 从 <i>游戏 Content 资源文件</i> 中, 读取加载指定材质
        /// </summary>
        /// <param name="textureFileNames">需要加载的 <b>材质名称</b> 数组 <br/>
        /// <i>(会加上<paramref name="basePath"/>, 无需重复添加路径)</i>
        /// </param>
        /// <param name="basePath">上述材质的 <b>路径</b> <br/>
        /// <i>(统一设置, 无需在<paramref name="textureFileNames"/>中重复添加路径)</i>
        /// <para>默认路径: "Images\"</para>
        /// </param>
        public void LoadTextures(string[] textureFileNames, string basePath = @"Images\")
        {
            foreach(string textureFileName in textureFileNames)
            {
                TextureBase texture;
                try
                {
                    texture = MGame.Content.Load<TextureBase>(basePath + @"Json\" + textureFileName);
                }
                catch
                {
                    texture = new TextureMono(textureFileName);
                }

                Texture2D textureSource = MGame.Content.Load<Texture2D>(basePath + (texture.ImagePath ?? texture.Name));
                texture.LoadInitialize(MGame, textureSource);
                Textures.Add(texture.Name, texture);
            }
        }



    }
}
