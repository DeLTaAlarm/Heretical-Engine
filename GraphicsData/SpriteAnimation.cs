using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsData
{
    public class SpriteAnimation
    {
        public List<Sprite> animationFrames;
        public int framesPerSecond;
        public int currentAnimationFrame;
        public bool isPaused;
        private int tickCounter;

        public SpriteAnimation(List<Sprite> animationFrames, int _framesPerSecond, bool play = true)
        {
            this.animationFrames = animationFrames;
            framesPerSecond = _framesPerSecond;
            isPaused = !play;
        }

        public void Update()
        {
            if (isPaused || framesPerSecond <= 0) return;

            tickCounter++;
            if (tickCounter >= 60 / framesPerSecond)
            {
                tickCounter = 0;
                currentAnimationFrame = (currentAnimationFrame + 1) % animationFrames.Count;
            }
        }

        public void Play() => isPaused = false;
        public void Pause() => isPaused = true;
        public void Reset()
        {
            isPaused = true;
            currentAnimationFrame = 0;
            tickCounter = 0;
        }
    }
}
