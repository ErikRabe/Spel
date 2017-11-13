using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Spel
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    static class GameElements
    {
       

        public enum State { Run, Quit };
        static GameTime lastTime;
        static Background background;
        static Player player;
        static Wall wall;
        static List<Wall> greyWalls;
        public static SoundEffect effect;
        public static State currentState;
       




        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public static void Initialize()
        {
            // TODO: Add your initialization logic here


          

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public static void LoadContent(ContentManager content, GameWindow window)
        {
            // Create a new SpriteBatch, which can be used to draw textures.



            player = new Player(content.Load<Texture2D>("Sprites/Player/Player"), 150, 150, 4f, 4f);
            background = new Background(content.Load<Texture2D>("Sprites/Background/Background"), window);
            wall = new Wall(content.Load<Texture2D>("Sprites/Wall/Wall1"), 250, 250, 0f, 0f);
            

            greyWalls = new List<Wall>();
            Texture2D wallSprite = (content.Load<Texture2D>("Sprites/Wall/Wall1"));

            int wallRightY = 0;
            int wallLeftY = 0;
            int wallUpX = 0;
            int wallDownX = 0;

            Wall newWall = new Wall(wallSprite, 250, 250, 0f, 0f);
            greyWalls.Add(newWall);


            for (int i = 0; i < 100; i++)
            {
                if (wallRightY <= window.ClientBounds.Height)
                {
                    Wall temp = new Wall(wallSprite, 992, wallRightY, 0f, 0f);
                    greyWalls.Add(temp);
                    wallRightY += 32;
                }
                if (wallLeftY <= window.ClientBounds.Height)
                {
                    Wall temp = new Wall(wallSprite, 0, wallLeftY, 0f, 0f);
                    greyWalls.Add(temp);
                    wallLeftY += 32;
                }
                if (wallUpX <= window.ClientBounds.Width)
                {
                    Wall temp = new Wall(wallSprite, wallUpX, 0,  0f, 0f);
                    greyWalls.Add(temp);
                    wallUpX += 32;
                }
                if (wallDownX <= window.ClientBounds.Width)
                {
                    Wall temp = new Wall(wallSprite, wallDownX, 480, 0f, 0f);
                    greyWalls.Add(temp);
                    wallDownX += 32;
                }



            }









            // TODO: use this.Content to load your game content here
        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {
            lastTime = gameTime;
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                
                return State.Quit;
            }


            foreach (Wall gw in greyWalls.ToList())
            {
                if (gw.CheckCollision(player) && player.dir == 1)
                {
                    player.vector.X = gw.vector.X - 35;
                }
                if (gw.CheckCollision(player) && player.dir == 2)
                {
                    player.vector.X = gw.vector.X + 35;
                }
                if (gw.CheckCollision(player) && player.dir == 3)
                {
                    player.vector.Y = gw.vector.Y + 35;
                }

                if (gw.CheckCollision(player) && player.dir == 4)
                {
                    player.vector.Y = gw.vector.Y - 35;
                }

                if (gw.CheckCollision(player) && player.dir == 23)
                {
                    if (player.vector.Y - 35 < window.ClientBounds.Height - window.ClientBounds.Height)
                    {
                        player.vector.Y = gw.vector.Y + 35;
                    }
                    else
                    {
                        player.vector.X = gw.vector.X + 35;

                    }
                }

                if (gw.CheckCollision(player) && player.dir == 13)
                {
                    if (player.vector.Y - 35 < window.ClientBounds.Height - window.ClientBounds.Height)
                    {
                        player.vector.Y = gw.vector.Y + 35;
                    }
                    else
                    {
                        player.vector.X = gw.vector.X - 35;

                    }
                }

                if (gw.CheckCollision(player) && player.dir == 24)
                {
                    if (player.vector.Y + 64 > window.ClientBounds.Height)
                    {
                        player.vector.Y = gw.vector.Y - 35;
                    }
                    else
                    {
                        player.vector.X = gw.vector.X + 35;

                    }
                }
                if (gw.CheckCollision(player) && player.dir == 14)
                {
                    if (player.vector.Y + 64 > window.ClientBounds.Height)
                    {
                        player.vector.Y = gw.vector.Y - 35;
                    }
                    else
                    {
                        player.vector.X = gw.vector.X - 35;

                    }

                }



                gw.Update(window, gameTime, effect);
            }







            background.Update(window);
            player.Update(window, gameTime, effect);
            wall.Update(window, gameTime, effect);

            return State.Run;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            wall.Draw(spriteBatch);
            foreach (Wall gw in greyWalls)
                gw.Draw(spriteBatch);


            if (lastTime == null)
                return;





        }
    }
}
