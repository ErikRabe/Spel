using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace nicegame
{
    class Player : PhysicalObject
    {
        List<Bullet> bullets;
        
        
        Texture2D bulletTexture;
        
        SpriteEffects fL = SpriteEffects.FlipHorizontally;
       
        double timeSinceLastBullet = 0;
        public double timeSinceLastTP = 0;
      
        int points = 0;
        public int Points { get { return points; } set { points = value; } }

        int cash = 0;
        public int Cash { get { return cash; } set { cash = value; } }
        int level = 1;
       
        
        
        int moregold = 1;
        public int Level { get { return level; } set { level = value; } }
        public float Health { get { return health; } set { health = value; } }
        public float health;
        
        
        
        
        public int Moregold { get { return moregold; } set { moregold = value; } }
        
        public List<Bullet> Bullets { get { return bullets; } }
       
        int frame = 0;
        double frameBuffer = 0;
        double animationTime = 0.2;
        bool direction = false;
       
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            Rectangle rekt = new Rectangle((texture.Width/3) * frame, 0, texture.Width/3, texture.Height);
            spriteBatch.Draw(texture, vector, rekt, Color.White, 0, Vector2.Zero, Vector2.One, direction ? fL : SpriteEffects.None, 0);
            //spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bullet b in bullets)
                b.Draw(spriteBatch);

            







        }
  

               public void Reset(float X, float Y, float speedX, float speedY)
        {
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;
            bullets.Clear();
            
            timeSinceLastBullet = 0;
            timeSinceLastTP = 0;
            points = 0;
            Cash = 0;
            isAlive = true;
           

        }

     

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture) : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;

            size = new Vector2(texture.Width/3, texture.Height);

          

        }

        public void Update(GameWindow window, GameTime gameTime, SoundEffect effect)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 velocity = Vector2.Zero;

             if (vector.X <= window.ClientBounds.Width - texture.Width/3 && vector.X >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                {
              
                    velocity.X += speed.X;
                    vector.X += speed.X;

                }
                if (keyboardState.IsKeyDown(Keys.Left))
                {
             
                    vector.X -= speed.X;
                    velocity.X -= speed.X;
                }
            }

            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 130)
            {
           
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    velocity.Y += speed.Y;
                    vector.Y -= speed.Y;
                }
            }
            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    vector.Y += speed.Y;
                    velocity.Y += speed.Y;

                }
            }

            if (vector.X < 0)
                vector.X = 0;
            if (vector.X > window.ClientBounds.Width - texture.Width/3) 
            {
               vector.X = window.ClientBounds.Width - texture.Width/3;
            }
            if (vector.Y < 0)
                vector.Y = 0;
            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
               vector.Y = window.ClientBounds.Height - texture.Height;
            }

          

            if (velocity.X > 0)
                direction = false;

            if (keyboardState.IsKeyDown(Keys.Z))
            {
                direction = true;
               
            }
            else
            {
                direction = false;
            }
       

            if (keyboardState.IsKeyDown(Keys.Escape))
                isAlive = false;
                

            if (keyboardState.IsKeyDown(Keys.X) && direction == false)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 180)
                {
                    Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width/3.5f , vector.Y + texture.Height/2);
                    bullets.Add(temp);
                    /*effect.Play();*/
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }

            }
            if (keyboardState.IsKeyDown(Keys.X) && direction == true)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 180)
                {
                    Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 15f, vector.Y + texture.Height / 2);
                    bullets.Add(temp);
                    /*effect.Play();*/
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }

            }
            if (keyboardState.IsKeyDown(Keys.Space) && direction == false)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastTP + 4000 )
                {
                    vector.X = X + 400;
                   
                      
                    timeSinceLastTP = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
        
            if (keyboardState.IsKeyDown(Keys.Space) && direction == true)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastTP + 4000)
                {
                    vector.X = X - 400;

                    timeSinceLastTP = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            
        
          
            if (velocity.X == 0 && velocity.Y == 0)
            {
                frame = 0;
            }
            else
            {
                if (frameBuffer >= animationTime)
                {
                    frameBuffer -= animationTime;
                    frame++;

                    if (frame >= 3)
                    {
                        frame = 0;
                    }
                }
                frameBuffer += gameTime.ElapsedGameTime.TotalSeconds;
            }

            foreach (Bullet b in bullets.ToList())
            {
                b.Update();
                if (!b.IsAlive)
                    bullets.Remove(b);
            }
        }
       
    

    }
    class Bullet : PhysicalObject
    {
        public static int bspeed = 12;
        public int Bspeed { get { return bspeed; } set { bspeed = value; } }
        public static int aspeed1 = 1;
        public int Aspeed1 { get { return aspeed1; } set { aspeed1 = value; } }
        public float Damage = 30;
        
          


       
        public Bullet(Texture2D texture, float X, float Y)
            : base(texture, X, Y, bspeed, 0)
        {
           
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
        
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                if (Aspeed1 == 1)
                {

                    bspeed = 12;
                }
                if (Aspeed1 == 2)
                {
                    bspeed = 20;
                }
            }
            if (!keyboardState.IsKeyDown(Keys.Z))
            {
                if (Aspeed1 == 1)
                {

                    bspeed = -12;
                }
                if (Aspeed1 == 2)
                {
                    bspeed = -20;
                }
            }
           
       
            vector.X -= speed.X;
            if (vector.X < 0)
                isAlive = false;
        }
    }
    

    
       
}
