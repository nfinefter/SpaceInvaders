using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        Texture2D bulletTexture;
        Texture2D invaderTexture;
        Texture2D shipTexture;
        Ship ship;
        List<Invaders> invaders = new List<Invaders>();
        List<Bullet> bullets = new List<Bullet>();
        TimeSpan spaceInvaderSpawnDelay = TimeSpan.FromSeconds(2);
        TimeSpan elapsedTime = TimeSpan.Zero;
        KeyboardState prevks;
        Random rand = new Random();

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here\

            graphics.PreferredBackBufferWidth = 400;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bulletTexture = Content.Load<Texture2D>("bullet");
            invaderTexture = Content.Load<Texture2D>("invader");
            shipTexture = Content.Load<Texture2D>("ship");

            ship = new Ship(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 50), shipTexture, new Vector2(0.3f, 0.3f), Color.White, 0, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), 0);
            
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Left) == true && prevks.IsKeyDown(Keys.Right) == false)
            {
                ship.Position = new Vector2 (MathHelper.Max(ship.Position.X + -5, ship.ScaledWidth - 25), ship.Position.Y + 0);
            }
            else if (ks.IsKeyDown(Keys.Right) == true && prevks.IsKeyDown(Keys.Left) == false)
            {
                ship.Position = new Vector2(MathHelper.Min(ship.Position.X + 5, GraphicsDevice.Viewport.Width - ship.ScaledWidth + 25), ship.Position.Y + 0);
            }

            if (ks.IsKeyDown(Keys.Space) == true && prevks.IsKeyDown(Keys.Space) == false)
            {
                bullets.Add(new Bullet(new Vector2(ship.Position.X, ship.Position.Y), bulletTexture, new Vector2(0.1f, 0.1f), Color.White, ship.Rotation, new Vector2(bulletTexture.Width / 2, bulletTexture.Height / 2), new Vector2(0, -5)));
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Position.X + bullets[i].ScaledWidth >= GraphicsDevice.Viewport.Width)
                {
                    bullets.RemoveAt(i);
                    i--;
                    continue;
                }
                else if (bullets[i].Position.X <= 0)
                {
                    bullets.RemoveAt(i);
                    i--;
                    continue;
                }
                else if (bullets[i].Position.Y + bullets[i].ScaledHeight >= GraphicsDevice.Viewport.Height)
                {
                    bullets.RemoveAt(i);
                    i--;
                    continue;
                }
                else if (bullets[i].Position.Y <= 0)
                {
                    bullets.RemoveAt(i);
                    i--;
                    continue;
                }

                bullets[i].Update();
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                for (int a = 0; a < invaders.Count; a++)
                {
                    if (bullets[i].HitBox.Intersects(invaders[a].HitBox))
                    {
                        bullets.RemoveAt(i);
                        invaders.RemoveAt(a);
                        break;
                    }
                }
            }
            if (elapsedTime >= spaceInvaderSpawnDelay)
            {
                elapsedTime = TimeSpan.Zero;

                invaders.Add(new Invaders(new Vector2(rand.Next(0, GraphicsDevice.Viewport.Width), 50), invaderTexture, new Vector2(0.2f, 0.2f), Color.White, 0, new Vector2(0, 0)));
            }
                prevks = ks;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            ship.Draw(spriteBatch);
           
            for (int i = 0; i < invaders.Count; i++)
            {
                invaders[i].Draw(spriteBatch);
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
