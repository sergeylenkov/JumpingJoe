using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpingJoe
{
    class Player : Object
    {
        public bool IsJumping;
        private float startY = 0.0f;
        private float maxY = 0.0f;
        private float velocity = 300.0f;
        public Vector2 Position;
        public float Speed = 1f;
        public float Scale = 1;
        private AnimationSprite walkSprite;
        private AnimationSprite jumpUpSprite;
        private AnimationSprite jumpDownSprite;
        private bool IsJumpingDown;

        public Player(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            IsJumping = false;

            walkSprite = new AnimationSprite(content.Load<Texture2D>("WalkSheet"), new Rectangle(0, 0, 150, 150), 7, 0.4f);
            walkSprite.IsLoop = true;

            jumpUpSprite = new AnimationSprite(content.Load<Texture2D>("JumpUpSheet"), new Rectangle(0, 0, 150, 150), 7, 0.5f);
            jumpUpSprite.IsLoop = false;

            jumpDownSprite = new AnimationSprite(content.Load<Texture2D>("JumpDownSheet"), new Rectangle(0, 0, 150, 150), 5, 0.4f);
            jumpDownSprite.IsLoop = false;
        }

        public void Jump()
        {
            if (IsJumping)
            {
                maxY = Position.Y - 100.0f;
            }
            else
            {
                IsJumping = true;
                IsJumpingDown = false;

                jumpUpSprite.Start();
                jumpDownSprite.Start();

                startY = Position.Y;
                maxY = Position.Y - 100.0f;
                velocity = -200.0f;
            }
        }

        public void Update(GameTime gameTime)
        {
            walkSprite.Position = Position;
            jumpUpSprite.Position = Position;
            jumpDownSprite.Position = Position;

            walkSprite.Scale = Scale;
            jumpUpSprite.Scale = Scale;
            jumpDownSprite.Scale = Scale;

            if (IsJumping)
            {
                if (jumpUpSprite.CurrentFrame >= 2)
                {
                    float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Position.Y += velocity * time;

                    if (Position.Y <= maxY)
                    {
                        velocity = velocity * -1;
                        IsJumpingDown = true;
                    }
                    else if (Position.Y >= startY)
                    {
                        Position.Y = startY;
                        IsJumping = false;
                    }
                }

                if (IsJumpingDown)
                {
                    jumpDownSprite.Update(gameTime);
                } else
                {
                    jumpUpSprite.Update(gameTime);
                }
            }
            else
            {
                walkSprite.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsJumping)
            {
                if (IsJumpingDown)
                {
                    jumpDownSprite.Draw(spriteBatch);
                } else
                {
                    jumpUpSprite.Draw(spriteBatch);

                }
            }
            else
            {
                walkSprite.Draw(spriteBatch);
            }
        }
    }
}
