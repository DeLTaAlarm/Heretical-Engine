using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using GraphicsData;

namespace HInfrastructure
{
    public class ResourceManager
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager contentManager;
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteAtlas> spriteAtlasList = new Dictionary<string, SpriteAtlas>();
        private Dictionary<string, Sprite> spriteList = new Dictionary<string, Sprite>();
        private Dictionary<string, SpriteAnimation> animationList = new Dictionary<string, SpriteAnimation>();
        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

        public ResourceManager(ContentManager _contentManager, GraphicsDevice _graphicsDevice)
        {
            contentManager = _contentManager;
            graphicsDevice = _graphicsDevice;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string spriteDir = Path.Combine(baseDir, "Resources", "Sprites");

            if (Directory.Exists(spriteDir))
            {
                foreach (string file in Directory.EnumerateFiles(spriteDir, "*.png", SearchOption.AllDirectories))
                {
                    using (var fileStream = new FileStream(file, FileMode.Open))
                    {
                        Texture2D t_texture = Texture2D.FromStream(graphicsDevice, fileStream);
                        FileInfo t_file = new FileInfo(file);
                        textures.Add(Path.GetFileNameWithoutExtension(t_file.Name), t_texture);
                    }
                }
            }
            else
            {
                throw new DirectoryNotFoundException($"Папка ресурсов не найдена: {spriteDir}");
            }

            foreach (var kvp in textures)
            {
                string textureName = kvp.Key;
                Texture2D texture = kvp.Value;

                SpriteAtlas atlas = new SpriteAtlas(texture, 1, 1);
                spriteAtlasList.Add(textureName, atlas);

                Sprite sprite = new Sprite(atlas, 0);
                spriteList.Add(textureName, sprite);
            }
        }

        public Texture2D GetTexture(string name)
        {
            if (textures.TryGetValue(name, out var texture))
            {
                return texture;
            }

            throw new Exception($"Texture \"{name}\" not found.");
        }

        public SpriteAtlas GetSpriteAtlas(string name)
        {
            if (spriteAtlasList.TryGetValue(name, out var atlas))
            {
                return atlas;
            }

            throw new Exception($"Sprite atlas \"{name}\" not found.");
        }

        public Sprite GetSprite(string name)
        {
            if (spriteList.TryGetValue(name, out var sprite))
            {
                return sprite;
            }

            throw new Exception($"Sprite \"{name}\" not found.");
        }

        public SpriteAnimation GetAnimation(string name)
        {
            if (animationList.TryGetValue(name, out var animation))
            {
                return animation;
            }

            throw new Exception($"Sprite animation \"{name}\" not found.");
        }

        public void LoadTexture(string name, string path)
        {
            if (!textures.ContainsKey(name))
            {
                textures[name] = contentManager.Load<Texture2D>(path);
            }
        }

        public void LoadSpriteAtlas(string name, string path)
        {

        }

        public void LoadAnimation(string name, string path)
        {

        }

        public void LoadFont(string name, string path)
        {

        }

        public void LoadSound(string name, string path)
        {

        }

        public void AddSpriteAtlas(string atlasName, string textureName, int rows, int columns)
        {
            spriteAtlasList.Add(atlasName, new SpriteAtlas(GetTexture(textureName), rows, columns));
        }

        public void AddSprite(string spriteName, string atlasName, int frame)
        {
            spriteList.Add(spriteName, new Sprite(GetSpriteAtlas(atlasName), frame));
        }

        public void AddAnimation(string animationName, int frame, bool playOnStart, params string[] spriteNames)
        {
            List<Sprite> frames = new List<Sprite>();

            for (int i = 0; i < spriteNames.Length; i++) 
            {
                frames.Add(GetSprite(spriteNames[i]));
            }

            animationList.Add(animationName, new SpriteAnimation(frames, frame, playOnStart));
        }

        public void UnloadTexture(string name)
        {
            if (textures.ContainsKey(name))
            {
                textures[name].Dispose();
                textures.Remove(name);
            }
        }

        public void UnloadAll()
        {
            foreach (Texture texture in textures.Values)
            {
                texture.Dispose();
            }

            textures.Clear();
            spriteAtlasList.Clear();
            spriteList.Clear();
            animationList.Clear();
            fonts.Clear();
            sounds.Clear();
        }
    }
}
