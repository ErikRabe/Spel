using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
/*using Microsoft.Xna.Framework.GamerServices;*/
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace nicegame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D player;
   
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            GameElements.currentState = GameElements.State.Menu;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 512;
            graphics.ApplyChanges();
            GameElements.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);
            player = Content.Load<Texture2D>("images/player/player_right");
           
         


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
         

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (GameElements.currentState)
            {
                case GameElements.State.Run:
                    GameElements.currentState =
                     GameElements.RunUpdate(Content, Window, gameTime);
                    break;
                case GameElements.State.Run2:
                    GameElements.currentState =
                     GameElements.Run2Update(Content, Window, gameTime);
                    break;
                case GameElements.State.As1:
                    GameElements.currentState =
                    GameElements.currentState = GameElements.As1Update(gameTime);
                    break;
                case GameElements.State.Purchase:
                    GameElements.currentState =
                    GameElements.currentState = GameElements.PurchaseUpdate(gameTime);
                    break;
                case GameElements.State.moregold:
                    GameElements.currentState =
                    GameElements.currentState = GameElements.moregoldUpdate(gameTime);
                    break;
                case GameElements.State.Menu2:
                    GameElements.currentState = GameElements.Menu2Update(gameTime);
                    break;
                case GameElements.State.Highscore:
                    GameElements.currentState = GameElements.HighScoreUpdate();
                    break;
                case GameElements.State.Store:
                    GameElements.currentState = GameElements.StoreUpdate(gameTime);
                    break;
                case GameElements.State.Controls:
                    GameElements.currentState = GameElements.ControlsUpdate(gameTime);
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
                default:
                    GameElements.currentState = GameElements.MenuUpdate(gameTime);
                    break;

            }




            base.Update(gameTime);
            
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            

          

            switch (GameElements.currentState)
            {
                case GameElements.State.Run:
                    GameElements.RunDraw(spriteBatch);
                    break;
                case GameElements.State.Run2:
                    GameElements.Run2Draw(spriteBatch);
                    break;
                case GameElements.State.Store:
                case GameElements.State.As1:
                case GameElements.State.moregold:
                    GameElements.StoreDraw(spriteBatch);
                    break;
                case GameElements.State.Purchase:
                    GameElements.StoreDraw(spriteBatch);
                    break;
                case GameElements.State.Menu2:
                    GameElements.Menu2Draw(spriteBatch);
                    break;
                case GameElements.State.Controls:
                    GameElements.ControlsDraw(spriteBatch);
                    break;
                case GameElements.State.Highscore:
                    GameElements.HighScoreDraw(spriteBatch);
                    break;
                default:
                    GameElements.MenuDraw(spriteBatch);
                    break;

            }
            spriteBatch.End();

            base.Draw(gameTime);






        }
    }
}
