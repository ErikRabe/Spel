using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace nicegame
{ 
    class GoldCoin : PhysicalObject
    {
        double timeToDie;

        public GoldCoin(Texture2D texture, float X, float Y, GameTime gameTime)
            : base(texture, X, Y, 0, 2f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 8000;
        }

        public void Update(GameTime gameTime)
        {
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
                isAlive = false;
        }
    }
    class Heart : PhysicalObject
    {
        double timeToDie;

        public Heart(Texture2D texture, float X, float Y, GameTime gameTime)
            : base(texture, X, Y, 0, 2f)
        {
            timeToDie = gameTime.TotalGameTime.TotalMilliseconds + 8000;
        }

        public void Update(GameTime gameTime)
        {
            if (timeToDie < gameTime.TotalGameTime.TotalMilliseconds)
                isAlive = false;
        }
    }
   
   class Portal : PhysicalObject
   {
       int frame = 0;
       int frameMax = 0;
       double frameBuffer = 0;
       public double animationTime = 0.1;
       public bool direction = false;
       Vector2 frameSize;


       public Portal(Texture2D texture, float X, float Y)
           : base(texture, X, Y, 0f, 0)
       {
           frameSize = new Vector2(64, 256);
           frameMax = 3;
       }

       public  void Update(GameWindow window, GameTime gameTime)
       {
            if (frameBuffer >= animationTime)
            {
                frameBuffer -= animationTime;
                frame++;

                if (frame >= frameMax + 1)
                {
                    frame = 0;
                }
            }
            frameBuffer += gameTime.ElapsedGameTime.TotalSeconds;
       }

       public override void Draw(SpriteBatch spriteBatch)
       {
           if (frameMax == 0)
               base.Draw(spriteBatch);
           else
           {
               Rectangle rekt = new Rectangle((int)frameSize.X * frame, 0, (int)frameSize.X, (int)frameSize.Y);
        
               spriteBatch.Draw(texture, vector , rekt, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
         
           }
           //base.Draw(spriteBatch);
       }
          
   }

   class Purchase : PhysicalObject
   {
       public Purchase(Texture2D texture, float X, float Y)
           : base(texture, X, Y, 0f, 0)
       {
       }

       public void Update(GameWindow window, GameTime gameTime)
       {


       }
   }

       
    
}
