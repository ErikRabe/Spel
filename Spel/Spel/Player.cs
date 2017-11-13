using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Spel
{
    class Player : PhysicalObject
    {
        

        int frame = 0;
        double frameBuffer = 0;
        double animationTime = 0.05;
        bool direction = false;
        public bool collisionR = false;
        public bool collisionL = false;
        public bool collisionU = false;
        public bool collisionD = false;
        public float playerSpeedX = 0;
        public float playerSpeedY = 0;

        public int dir;

        SpriteEffects fL = SpriteEffects.FlipHorizontally;


        public override void Draw(SpriteBatch spriteBatch)
        {

            Rectangle rekt = new Rectangle((texture.Width / 2) * frame, 0, texture.Width / 2, texture.Height);
            spriteBatch.Draw(texture, vector, rekt, Color.White, 0, Vector2.Zero, Vector2.One, direction ? fL : SpriteEffects.None, 0);
            //spriteBatch.Draw(texture, vector, Color.White);
     









        }


        public void Reset(float X, float Y, float speedX, float speedY)
        {
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;
        
            isAlive = true;


        }



        public Player(Texture2D texture, float X, float Y, float speedX, float speedY ) : base(texture, X, Y, speedX, speedY)
        {

            size = new Vector2(texture.Width / 2, texture.Height);



        }

        public void Update(GameWindow window, GameTime gameTime, SoundEffect effect)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 velocity = Vector2.Zero;

            if (vector.X <= window.ClientBounds.Width - texture.Width / 2 && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Right) && collisionR == false || keyboardState.IsKeyDown(Keys.D) && collisionR == false)
                {

                    velocity.X += speed.X;
                    vector.X += speed.X;
                    dir = 1;
                    
           



                }

                if (keyboardState.IsKeyDown(Keys.Left) && collisionL == false || keyboardState.IsKeyDown(Keys.A) && collisionL == false)
                {

                    vector.X -= speed.X;
                    velocity.X -= speed.X;
                    dir = 2;


                }
            }

            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {

                if (keyboardState.IsKeyDown(Keys.Up) && collisionU == false || keyboardState.IsKeyDown(Keys.W) && collisionU == false)
                {
                    velocity.Y += speed.Y;
                    vector.Y -= speed.Y;
                    dir = 3;

                }
            }
            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Down) && collisionD == false || keyboardState.IsKeyDown(Keys.S) && collisionD == false)
                {
                    vector.Y += speed.Y;
                    velocity.Y += speed.Y;
                    dir = 4;


                }
            }

            if (vector.X < 0)
                vector.X = 0;
            if (vector.X > window.ClientBounds.Width - texture.Width / 2)
            {
                vector.X = window.ClientBounds.Width - texture.Width / 2;
            }
            if (vector.Y < 0)
                vector.Y = 0;
            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
            }






            
                if (frameBuffer >= animationTime)
                {
                    frameBuffer -= animationTime;
                    frame++;

                    if (frame >= 2)
                    {
                        frame = 0;
                    }
                }
                frameBuffer += gameTime.ElapsedGameTime.TotalSeconds;
            

        
        }



    }
   




}
