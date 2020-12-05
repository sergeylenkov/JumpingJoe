using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace JumpingJoe
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class JumpingJoe : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        List<Sprite> obstacles = new List<Sprite>();
        List<Sprite> cloudsLayer1 = new List<Sprite>();
        List<Sprite> cloudsLayer2 = new List<Sprite>();
        List<Sprite> cloudsLayer3 = new List<Sprite>();
        Player joe;
        double spawnTimer = 0;
        double spawnTime = 2.0;
        double nextSpawnTime = 0.0;
        double cloudLayer1Timer = 0;
        double spawnCloudTime = 5.0;
        double nextSpawnCloudTime = 0;
        double cloudLayer2Timer = 0;
        double spawnCloud2Time = 8.0;
        double nextSpawnCloud2Time = 0;
        double cloudLayer3Timer = 0;
        double spawnCloud3Time = 8.0;
        double nextSpawnCloud3Time = 0;

        public JumpingJoe()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("Background");

            joe = new Player(Content);
            joe.Position = new Vector2(50, graphics.PreferredBackBufferHeight - 155);
            joe.Scale = 0.8f;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            BeforeUpdate(gameTime);

            foreach (var obstacle in obstacles)
            {
                obstacle.Position.X = obstacle.Position.X - ((float)gameTime.ElapsedGameTime.TotalSeconds * obstacle.Speed);
            }

            foreach (var cloud in cloudsLayer1)
            {
                cloud.Position.X = cloud.Position.X - ((float)gameTime.ElapsedGameTime.TotalSeconds * cloud.Speed);
            }

            foreach (var cloud in cloudsLayer2)
            {
                cloud.Position.X = cloud.Position.X - ((float)gameTime.ElapsedGameTime.TotalSeconds * cloud.Speed);
            }

            foreach (var cloud in cloudsLayer3)
            {
                cloud.Position.X = cloud.Position.X - ((float)gameTime.ElapsedGameTime.TotalSeconds * cloud.Speed);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                joe.Jump();
            }

            joe.Update(gameTime);

            AfterUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);           

            foreach (var obstacle in obstacles)
            {
                obstacle.Draw(spriteBatch);
            }

            foreach (var cloud in cloudsLayer3)
            {
                cloud.Draw(spriteBatch);
            }

            foreach (var cloud in cloudsLayer2)
            {
                cloud.Draw(spriteBatch);
            }           

            foreach (var cloud in cloudsLayer1)
            {
                cloud.Draw(spriteBatch);
            }

            joe.Draw(spriteBatch);
            spriteBatch.End();            

            base.Draw(gameTime);
        }

        private void Spawn()
        {
            Random random = new Random();

            int type = random.Next(0, 4);
            Sprite obstacle;

            switch (type)
            {
                case 0:
                    obstacle = new Sprite(Content.Load<Texture2D>("Cactus1"), new Rectangle(0, 0, 150, 150));
                    break;

                case 1:
                    obstacle = new Sprite(Content.Load<Texture2D>("Cactus2"), new Rectangle(0, 0, 150, 150));
                    break;

                case 2:
                    obstacle = new Sprite(Content.Load<Texture2D>("Stone1"), new Rectangle(0, 0, 150, 150));
                    break;

                case 3:
                    obstacle = new Sprite(Content.Load<Texture2D>("Stone2"), new Rectangle(0, 0, 150, 150));
                    break;

                default:
                    obstacle = new Sprite(Content.Load<Texture2D>("Cactus1"), new Rectangle(0, 0, 150, 150));
                    break;
            }

            obstacle.Position = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight - 110);
            obstacle.Scale = 0.5f;
            obstacle.Speed = 120.0f;

            obstacles.Add(obstacle);
        }

        private void SpawnCloud(int layer)
        {
            Random random = new Random();

            if (layer == 0)
            {
                int type = random.Next(1, 4);

                Sprite cloud = new Sprite(Content.Load<Texture2D>($"Cloud1_{type}"), new Rectangle(0, 0, 150, 150));
                cloud.Position = new Vector2(graphics.PreferredBackBufferWidth, 100 + random.Next(0, 10));
                cloud.Scale = 0.8f;
                cloud.Speed = 60.0f + random.Next(0, 10);

                cloudsLayer1.Add(cloud);
            }

            if (layer == 1)
            {
                int type = random.Next(1, 3);

                Sprite cloud = new Sprite(Content.Load<Texture2D>($"Cloud2_{type}"), new Rectangle(0, 0, 150, 150));
                cloud.Position = new Vector2(graphics.PreferredBackBufferWidth, 80 + random.Next(0, 10));
                cloud.Scale = 0.8f;
                cloud.Speed = 40.0f + random.Next(0, 10); ;

                cloudsLayer2.Add(cloud);
            }

            if (layer == 2)
            {
                int type = random.Next(1, 2);

                Sprite cloud = new Sprite(Content.Load<Texture2D>($"Cloud3_{type}"), new Rectangle(0, 0, 150, 150));
                cloud.Position = new Vector2(graphics.PreferredBackBufferWidth, 60 + random.Next(0, 10));
                cloud.Scale = 0.8f;
                cloud.Speed = 20.0f + random.Next(0, 10);

                cloudsLayer3.Add(cloud);
            }
        }

        private void BeforeUpdate(GameTime gameTime)
        {
            spawnTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnTimer > nextSpawnTime)
            {
                Spawn();
                spawnTimer = 0.0;

                Random random = new Random();
                nextSpawnTime = spawnTime + random.Next(1, 20) / 10.0f;
            }

            cloudLayer1Timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (cloudLayer1Timer > nextSpawnCloudTime)
            {
                SpawnCloud(0);
                cloudLayer1Timer = 0.0;

                Random random = new Random();
                nextSpawnCloudTime = spawnCloudTime + random.Next(1, 20) / 10.0f;
            }

            cloudLayer2Timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (cloudLayer2Timer > nextSpawnCloud2Time)
            {
                SpawnCloud(1);
                cloudLayer2Timer = 0.0;

                Random random = new Random();
                nextSpawnCloud2Time = spawnCloud2Time + random.Next(1, 20) / 10.0f;
            }

            cloudLayer3Timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (cloudLayer3Timer > nextSpawnCloud3Time)
            {
                SpawnCloud(2);
                cloudLayer3Timer = 0.0;

                Random random = new Random();
                nextSpawnCloud3Time = spawnCloud3Time + random.Next(1, 20) / 10.0f;
            }
        }

        private void AfterUpdate()
        {
            obstacles.RemoveAll(obstacle => obstacle.Position.Y < -100);
            cloudsLayer1.RemoveAll(cloud => cloud.Position.Y < -100);
            cloudsLayer2.RemoveAll(cloud => cloud.Position.Y < -100);
            cloudsLayer3.RemoveAll(cloud => cloud.Position.Y < -100);
        }
    }
}
