using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpingJoe
{
    class AnimationSprite
    {
        private Texture2D _texture;
        private Rectangle _size;
        public Vector2 Position;
        public float Speed = 2f;
        public float Scale = 1;
        public bool IsLoop = false;
        public float _animationTime = 1f;
        private int _currentFrame = 0;
        private int _framesCount = 1;
        private float _timer = 0;
        private float _frameTime = 0;
        private bool _animationEnd = false;

        public AnimationSprite(Texture2D texture, Rectangle size, int frameCount, float animationTime)
        {
            _texture = texture;
            _size = size;
            _framesCount = frameCount;
            _currentFrame = 0;            
            _animationTime = animationTime;
            _frameTime = _animationTime / _framesCount;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!_animationEnd)
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_timer > _frameTime)
                {
                    _timer = 0;
                    _currentFrame += 1;

                    if (_currentFrame == _framesCount)
                    {
                        if (IsLoop) {
                            _currentFrame = 0;
                        } else
                        {
                            _animationEnd = true;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, new Rectangle(_currentFrame * _size.Width, 0, _size.Width, _size.Height), Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 1);
        }

        public void Start()
        {
            _animationEnd = false;
            _currentFrame = 0;
        }

        public int CurrentFrame
        {
            get { return _currentFrame; }
        }
    }
}
