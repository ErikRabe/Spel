using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace nicegame
{
   abstract class Enemy : PhysicalObject
    {
        int frame = 0;
        int frameMax = 0;
        double frameBuffer = 0;
        public double animationTime = 0.2;
        public double timeSinceLastPenguinshot = 0;
        public bool direction = false;
        SpriteEffects fL = SpriteEffects.FlipHorizontally;
        Vector2 frameSize;

       public float Health = 100;

        public Enemy(Texture2D texture, float X, float Y, float speedX, float speedY, Vector2 fSize, int max)
            : base(texture, X, Y, speedX, speedY)
        {
            frameSize = fSize;
            frameMax = max;
        }

        public virtual void Update(GameWindow window, GameTime gameTime) 
        {
            if (speed.X == 0 && speed.Y == 0)
            {
                frame = 0;
            }
            else
            {
                if (frameBuffer >= animationTime)
                {
                    frameBuffer -= animationTime;
                    frame++;

                    if (frame >= frameMax+1)
                    {
                        frame = 0;
                    }
                }
                frameBuffer += gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (frameMax == 0)
                base.Draw(spriteBatch);
            else
            {
                Rectangle rekt = new Rectangle((int)frameSize.X * frame, 0, (int)frameSize.X, (int)frameSize.Y);
                spriteBatch.Draw(texture, vector, rekt, Color.White, 0, Vector2.Zero, Vector2.One, direction ? fL : SpriteEffects.None, 0);

            }
            //base.Draw(spriteBatch);
        }
          
        
    }
   class Fedora : Enemy
   {
       public Fedora(Texture2D texture, float X, float Y)
           : base(texture, X, Y, 3f, 0f, new Vector2(0, 0), 0)
       {
       }

       public override void Update(GameWindow window, GameTime gameTime)
       {
           base.Update(window, gameTime);
           //ÄNDRA DET HÄR
           vector.X += speed.X;
           if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
               speed.X *= -1;
       }
   }


   class NOMNOM : Enemy
   {
       public NOMNOM(Texture2D texture, float X, float Y)
           : base(texture, X, Y, -1f, 0f, new Vector2(64, 64), 1)
       {

           size = new Vector2(texture.Width / 2, texture.Height);
       }

       public override void Update(GameWindow window, GameTime gameTime)
       {
           base.Update(window, gameTime);

           vector.X += speed.X;
           if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
           {
               direction = true;
               speed.X *= -1;
           }
       }
   }
   class Rabbit : Enemy
   {
       public Rabbit(Texture2D texture, float X, float Y)
           : base(texture, X, Y, -1f, 0f, new Vector2(132, 159), 3)
       {
           animationTime = 0.09;
           Health = 500;
           size = new Vector2(texture.Width / 4, texture.Height);
       }

       public override void Update(GameWindow window, GameTime gameTime)
       {
           base.Update(window, gameTime);

           vector.X += speed.X;
           if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
           {
               direction = true;
               speed.X *= -1;
           }
       }
       
   }
    class Penguin : Enemy
    {
        public List<Penguinshot> penguinshots;


        public List<Penguinshot> Penguinshots { get { return penguinshots; } }
        public Texture2D PenguinshotTexture;
        


        public Penguin(Texture2D texture, float X, float Y, Texture2D coolShotTexture)
            : base(texture, X, Y, -1f, 0f, new Vector2(119, 181), 1)
        {
            penguinshots = new List<Penguinshot>();
            this.PenguinshotTexture = coolShotTexture;
            size = new Vector2(texture.Width / 2, texture.Height);
            Health = 2000;
        }

        public override void Update(GameWindow window, GameTime gameTime)
        {
            base.Update(window, gameTime);
           
           vector.X += speed.X;
           if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 0)
           {
               
               speed.X *= -1;
               direction = !direction;
           }
           
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastPenguinshot + 1000)
           {
               if (direction == false)
               {
                   Penguinshot temp = new Penguinshot(PenguinshotTexture, vector.X, vector.Y + 100, !direction);
                   penguinshots.Add(temp);
               }
               if (direction == true)
               {
                   Penguinshot temp = new Penguinshot(PenguinshotTexture, vector.X + 50, vector.Y + 100, !direction);
                   penguinshots.Add(temp);
               }




               timeSinceLastPenguinshot = gameTime.TotalGameTime.TotalMilliseconds;
           }


            foreach (Penguinshot ps in penguinshots.ToList())
            {
                ps.Update();
                if (!ps.IsAlive)
                    penguinshots.Remove(ps);
            }
            
      
       
       }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Penguinshot ps in penguinshots.ToList())
            {
                if (ps.IsAlive)
                    ps.Draw(spriteBatch);
            }
        }

    }
    class Penguinshot : PhysicalObject
    {
        static int pspeed = -16;
        public float pDamage = 30;




        public Penguinshot(Texture2D texture, float X, float Y, bool dir)
            : base(texture, X, Y, dir ? pspeed : -pspeed, 0)
        {

        }

        public void Update()
        {
            
          
            vector.X += speed.X;
            if (vector.X < 0)
                isAlive = false;

        }
    }
            
}
