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


            greyWalls = new List<Wall>();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public static void LoadContent(ContentManager content, GameWindow window)
        {
            // Create a new SpriteBatch, which can be used to draw textures.



            player = new Player(content.Load<Texture2D>("Sprites/Player/Player"), 0, 0, 4f, 4f);
            background = new Background(content.Load<Texture2D>("Sprites/Background/Background"), window);
            wall = new Wall(content.Load<Texture2D>("Sprites/Wall/Wall1"), 150, 150, 0f, 0f);
          



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



            if (wall.CheckCollision(player) && player.dir == 1)
            {
                player.collisionR = true;
            }
            if (wall.CheckCollision(player) && player.dir == 2)
            {
                player.collisionL = true;
            }
            if (wall.CheckCollision(player) && player.dir == 3)
            {
                player.collisionU = true;
            }
            if (wall.CheckCollision(player) && player.dir == 4)
            {
                player.collisionD = true;
            }
            if (!wall.CheckCollision(player))
            {
                player.collisionR = false;
                player.collisionD = false;
                player.collisionU = false;
                player.collisionL = false;
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


            if (lastTime == null)
                return;





        }
    }
}
