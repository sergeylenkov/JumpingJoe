using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpingJoe
{
    class Sprite
    {
        private Texture2D _texture;
        private Rectangle _size;
        public Vector2 Position;
        public float Speed = 1f;
        public float Scale = 1;

        public Sprite(Texture2D texture, Rectangle size)
        {
            _texture = texture;
            _size = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _size, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 1);
        }
    }
}
