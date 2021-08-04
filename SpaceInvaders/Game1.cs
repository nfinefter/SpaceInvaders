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
        Texture2D shieldTexture;
        SpriteFont Font;
        Ship ship;
        List<Bullet> bullets = new List<Bullet>();
        List<Bullet> evilBullets = new List<Bullet>();
        List<Shield> shields = new List<Shield>();
        bool gameOver = false;
        bool allInvadersDead = false;

        Group group;

        TimeSpan spaceInvaderMoveDelay = TimeSpan.FromSeconds(2);
        TimeSpan elapsedTime = TimeSpan.Zero;
        KeyboardState prevks;
        Random rand = new Random();

        public void endGame()
        {
            gameOver = true;

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets.RemoveAt(i);
            }
            for (int i = 0; i < evilBullets.Count; i++)
            {
                evilBullets.RemoveAt(i);
            }
            for (int a = 0; a < group.WidthCount; a++)
            {
                for (int b = 0; b < group.HeightCount; b++)
                {
                    group.Invaders[a, b] = null;
                }
            }
            for (int i = 0; i < shields.Count; i++)
            {
                shields.RemoveAt(i);
            } 
            GraphicsDevice.Clear(Color.CornflowerBlue);

        }
        public void Reset()
        {
            gameOver = false;
            for (int i = 0; i < shields.Count; i++)
            {
                shields[i].Health = 50;            
            }
            shields.Add(new Shield(new Vector2(100, GraphicsDevice.Viewport.Height - 130), shieldTexture, new Vector2(0.3f, 0.3f), Color.White, 0, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), 50));
            shields.Add(new Shield(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 130), shieldTexture, new Vector2(0.3f, 0.3f), Color.White, 0, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), 50));
            shields.Add(new Shield(new Vector2(GraphicsDevice.Viewport.Width - 100, GraphicsDevice.Viewport.Height - 130), shieldTexture, new Vector2(0.3f, 0.3f), Color.White, 0, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), 50));
            
            ship.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 50);
            group = new Group(new Vector2(0, 0), 4, 8, invaderTexture);
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here\

            graphics.PreferredBackBufferWidth = 500;
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
            shieldTexture = Content.Load<Texture2D>("shield");
            Font = Content.Load<SpriteFont>("Font");

            ship = new Ship(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 50), shipTexture, new Vector2(0.3f, 0.3f), Color.White, 0, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), 0);

            shields.Add(new Shield(new Vector2(100, GraphicsDevice.Viewport.Height - 130), shieldTexture, new Vector2(0.3f, 0.3f), Color.White, 0, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), 50));
            shields.Add(new Shield(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 130), shieldTexture, new Vector2(0.3f, 0.3f), Color.White, 0, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), 50));
            shields.Add(new Shield(new Vector2(GraphicsDevice.Viewport.Width - 100, GraphicsDevice.Viewport.Height - 130), shieldTexture, new Vector2(0.3f, 0.3f), Color.White, 0, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), 50));

            group = new Group(new Vector2(0, 0), 4, 8, invaderTexture);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState ks = Keyboard.GetState();

            elapsedTime += gameTime.ElapsedGameTime;


            if (ks.IsKeyDown(Keys.Left) == true && prevks.IsKeyDown(Keys.Right) == false)
            {
                ship.Position = new Vector2(MathHelper.Max(ship.Position.X + -5, ship.ScaledWidth - 25), ship.Position.Y + 0);
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
            for (int i = 0; i < evilBullets.Count; i++)
            {
                if (evilBullets[i].Position.X + evilBullets[i].ScaledWidth >= GraphicsDevice.Viewport.Width)
                {
                    evilBullets.RemoveAt(i);
                    i--;
                    continue;
                }
                else if (evilBullets[i].Position.X <= 0)
                {
                    evilBullets.RemoveAt(i);
                    i--;
                    continue;
                }
                else if (evilBullets[i].Position.Y + evilBullets[i].ScaledHeight >= GraphicsDevice.Viewport.Height)
                {
                    evilBullets.RemoveAt(i);
                    i--;
                    continue;
                }
                else if (evilBullets[i].Position.Y <= 0)
                {
                    evilBullets.RemoveAt(i);
                    i--;
                    continue;
                }


                evilBullets[i].Update();
            }

            for (int a = 0; a < group.WidthCount; a++)
            {
                for (int b = 0; b < group.HeightCount; b++)
                {
                    for (int i = 0; i < bullets.Count; i++)
                    {
                        bool twoBreak = false;

                        if (group.Invaders[a, b] != null)
                        {
                            if (bullets[i].HitBox.Intersects(group.Invaders[a, b].HitBox))
                            {
                                bullets.RemoveAt(i);
                                i -= 1;
                                group.Invaders[a, b] = null;
                                allInvadersDead = true;

                                for (int c = 0; c < group.WidthCount; c++)
                                {
                                    for (int d = 0; d < group.HeightCount; d++)
                                    {
                                        if (group.Invaders[c, d] != null)
                                        {
                                            allInvadersDead = false;
                                        }
                                    }
                                }
                                if (allInvadersDead == true)
                                {

                                    endGame();
                                    int? result = MessageBox.Show("You Win!", "Play Again?", new string[] { "yes", "no" }).GetAwaiter().GetResult();

                                    if (result == 0)
                                    {
                                        Reset();
                                    }
                                    if (result == 1)
                                    {
                                        Exit();
                                    }
                                }
                            }
                        }
                        if (twoBreak == true)
                        {
                            twoBreak = false;
                            break;
                        }
                    }
                }

            }


            for (int z = 0; z < shields.Count; z++)
            {


                bool twoBreak = false;
                for (int a = 0; a < group.WidthCount; a++)
                {
                    for (int b = 0; b < group.HeightCount; b++)
                    {
                        for (int i = 0; i < evilBullets.Count; i++)
                        {
                            //make sure this works look at the bullets loops above to double check
                            if (group.Invaders[a, b] != null)
                            {
                                if (evilBullets[i].HitBox.Intersects(shields[z].HitBox))
                                {
                                    evilBullets.RemoveAt(i);
                                    i -= 1;
                                    shields[z].Health -= 10;
                                    if (shields[z].Health <= 0)
                                    {
                                        shields[z].Position = new Vector2(-10, -10);
                                    }
                                    twoBreak = true;
                                    break;
                                }
                                if (evilBullets[i].HitBox.Intersects(ship.HitBox))
                                {
                                    endGame();
                                    int? result = MessageBox.Show("You Lose!", "Play Again?", new string[] { "yes", "no" }).GetAwaiter().GetResult();

                                    if (result == 0)
                                    {
                                        Reset();
                                    }
                                    if (result == 1)
                                    {
                                        Exit();
                                    }
                                }
                            }
                        }
                    }
                    if (twoBreak == true)
                    {
                        twoBreak = false;
                        break;
                    }

                }
            }
            if (elapsedTime >= spaceInvaderMoveDelay)
            {
                bool exit = false;
                bool wallHit = false;
                for (int a = 0; a < group.WidthCount && exit == false; a++)
                {

                    for (int b = 0; b < group.HeightCount && exit == false; b++)
                    {
                        if (group.Invaders[a, b] != null)
                        {

                            if (group.Invaders[a, b].Position.X + group.Invaders[a, b].ScaledWidth + group.Invaders[a,b].Speed.X >= GraphicsDevice.Viewport.Width)
                            {
                                for (int c = 0; c < group.WidthCount; c++)
                                {
                                    for (int d = 0; d < group.HeightCount; d++)
                                    {
                                        if (group.Invaders[c, d] != null)
                                        {
                                            group.Invaders[c, d].Speed = new Vector2(0, 20);
                                            group.Invaders[c, d].Update();
                                            group.Invaders[c, d].Speed = new Vector2(-20, 0);
                                            wallHit = true;
                                            
                                        }
                                        exit = true;
                                    }

                                }
                            }
                            else if (group.Invaders[a, b].Position.X + group.Invaders[a,b].Speed.X <= 0)
                            {
                                for (int c = 0; c < group.WidthCount; c++)
                                {
                                    for (int d = 0; d < group.HeightCount; d++)
                                    {
                                        if (group.Invaders[c, d] != null)
                                        {
                                            group.Invaders[c, d].Speed = new Vector2(0, 20);
                                            group.Invaders[c, d].Update();
                                            group.Invaders[c, d].Speed = new Vector2(20, 0);
                                            wallHit = true;
                                           
                                        }
                                        exit = true;
                                    }

                                }

                            }

                        }
                    }
                }
                for (int a = 0; a < group.WidthCount; a++)
                {
                    for (int b = 0; b < group.HeightCount; b++)
                    {
                        if (wallHit == false)
                        {
                            if (group.Invaders[a, b] != null)
                            {
                                group.Invaders[a, b].Update();
                            }
                        }
                        
                    }
                }
                for (int a = 0; a < group.WidthCount; a++)
                {

                    for (int b = 0; b < group.HeightCount; b++)
                    {
                        if (group.Invaders[a, b] != null)
                        {
                            if (rand.Next(0, 10) == 1)
                            {
                                evilBullets.Add(new Bullet(new Vector2(group.Invaders[a, b].Position.X, group.Invaders[a, b].Position.Y), bulletTexture, new Vector2(0.1f, 0.1f), Color.White, ship.Rotation, new Vector2(bulletTexture.Width / 2, bulletTexture.Height / 2), new Vector2(0, 2)));
                            }
                        }
                    }
                }
                elapsedTime = TimeSpan.Zero;
            }

            prevks = ks;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();


            if (gameOver == false)
            {
                ship.Draw(spriteBatch);

                for (int a = 0; a < group.WidthCount; a++)
                {
                    for (int b = 0; b < group.HeightCount; b++)
                    {
                        if (group.Invaders[a, b] != null)
                        {
                            group.Invaders[a, b].Draw(spriteBatch);
                        }
                    }
                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Draw(spriteBatch);
                }
                for (int i = 0; i < evilBullets.Count; i++)
                {
                    evilBullets[i].Draw(spriteBatch);
                }
                for (int i = 0; i < shields.Count; i++)
                {
                    if (shields[i].Health > 0)
                    {
                        shields[i].Draw(spriteBatch);
                        spriteBatch.DrawString(Font, $"{shields[i].Health}", new Vector2(shields[i].Position.X - 10, shields[i].Position.Y - 5), Color.Red);
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
