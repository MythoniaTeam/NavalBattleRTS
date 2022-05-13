using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    public class SpriteAnimated : Sprite
    {
        private TextureSet TextureAnimated => (TextureSet)Texture;

        private float _timeCount;
        /// <summary>记录当前循环时长的计时器(以标准帧F为单位)</summary>
        public float TimeCount
        {
            get => _timeCount;
            set
            {
                //计时器超过一次循环时, 取余, 开启新循环
                _timeCount = value;
                _timeCount %= CurrentAnimation.CycleDuration;
            }
        }



        /// <summary>当前应该绘制的帧</summary>
        public int CurrentFrame
        {
            // 计时器 / 每帧时长(已变速)  => 当前动画帧编号
            get => (int)(TimeCount / FrameDurationChanged);
            // 输入动画帧编号 => 计时器设为 编号 * 每帧时长(已变速)
            set => TimeCount = FrameDurationChanged * value;
        }
        private Animation CurrentAnimation;



        /// <summary>乘以速率后的 动画帧时长</summary>
        private float FrameDurationChanged => CurrentAnimation.FrameDuration * PlaySpeed;
        private float _playSpeed = 1;
        /// <summary>播放速度</summary>
        public float PlaySpeed
        {
            get => _playSpeed;
            set
            {
                //记录下当前帧, 在速度改变后, 再把计时器恢复到 = 当前帧的值
                //(需要是float, 来确保修改计时器的时候算上超出的部分，如原Duration = 4F, 计时器6F, 需要 算上多出来的那2F)
                float currentFrame = TimeCount / FrameDurationChanged;
                _playSpeed = value;
                TimeCount = FrameDurationChanged * currentFrame;
            }
        }

        public SpriteAnimated(MGame game, string name, TextureBase texture,
            string aniName, float playSpeed = 1,
            MPosition originPos = null, /*MVector? originDP = null,*/ bool isWorldPos = true, LayerInfo? layerInfo = null,
            MVector? scale = null, MAngle? rotation = null)
            : base(game, name, texture, originPos, /*originDP,*/ isWorldPos, layerInfo, scale, rotation)
        {
            _Constructor(texture, aniName, playSpeed);
        }

        public SpriteAnimated(MGame game, string name, TextureBase texture,
            string aniName, float playSpeed = 1,
            Func<MVector> getOriginPosMethod = null, /*MVector? originDP = null,*/ bool isWorldPos = true, LayerInfo? layerInfo = null,
            MVector? scale = null, MAngle? rotation = null)
            : base(game, name, texture, getOriginPosMethod,/* originDP,*/ isWorldPos, layerInfo, scale, rotation)
        {
            _Constructor(texture, aniName, playSpeed);
        }


        private void _Constructor(TextureBase texture, string aniName, float playSpeed)
        {
            if (texture is not TextureSet) throw new Exception($"The Given Texture to SpriteAnimated \"{Name}\" is not TextureSet");
            aniName ??= TextureAnimated.DefaultAnimation;
            CurrentAnimation = (aniName is not null) ? TextureAnimated.GetAnimation(aniName) : TextureAnimated.Animations[0];
            PlaySpeed = playSpeed;
        }



        public override void UpdateSprite(GameTime gameTime)
        {
            TimeCount += gameTime.ElapsedGameTime.ToStandardFrame();
        }



        public override void DrawSprite(Camera cam, SpriteBatch spriteBatch, float layer) => DrawFrame(cam, spriteBatch, CurrentFrame, layer);
        


        public void SetAnimation(string aniName, float playSpeed = 1)
        {
            CurrentAnimation = ((TextureSet)Texture).GetAnimation(aniName);
            TimeCount = 0;
            PlaySpeed = playSpeed;
        }
    }
}
