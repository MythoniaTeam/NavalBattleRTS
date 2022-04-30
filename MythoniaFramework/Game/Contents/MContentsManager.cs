using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Contents
{
    public class MContentsManager
    {
        private Dictionary<string, TextureBase> Textures = new();

        public TextureBase this[string textureName]
        {
            get => Textures[textureName];
        }


        public void LoadTextures(string[] textureFileNames, string basePath = @"Images\")
        {
            foreach(string textureFileName in textureFileNames)
            {
                TextureBase texture;
                try
                {
                    texture = Game.Content.Load<TextureBase>(basePath + @"Json\" + textureFileName);
                }
                catch (Exception ex)
                {
                    texture = new TextureMono(textureFileName);
                }

                Texture2D textureSource = Game.Content.Load<Texture2D>(basePath + (texture.ImagePath ?? texture.Name));
                texture.InitializeTexture(textureSource);
                Textures.Add(texture.Name, texture);
            }
        }


        MGame Game;
        public MContentsManager(MGame game)
        {
            Game = game;
        }

    }
}
