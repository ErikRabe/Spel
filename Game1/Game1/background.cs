using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace nicegame
{
    class BackgroundSprite : GameObject
      
    {
        public BackgroundSprite(Texture2D texture, float X, float Y)
            : base(texture, X, Y)
        {
        }

        public void Update(GameWindow window, int nrBackgroundsX)
        {
            vector.X += 1f;
            if (vector.X > window.ClientBounds.Width)
            {
                vector.X = vector.X - nrBackgroundsX * texture.Width;
            }
        }
    }
    class Background
       
    {
        BackgroundSprite[,] background;
        int nrBackgroundsX, nrBackgroundsY;

        public Background(Texture2D texture, GameWindow window)
        {
            double tmpX = (double)window.ClientBounds.Width / texture.Width;
            nrBackgroundsX = (int)Math.Ceiling(tmpX) + 1;

            double tmpY = (double)window.ClientBounds.Height / texture.Height;
            nrBackgroundsY = (int)Math.Ceiling(tmpY);

            background = new BackgroundSprite[nrBackgroundsX, nrBackgroundsY];
            for (int i = 0; i < nrBackgroundsX; i++)
            {
                for (int j = 0; j < nrBackgroundsY; j++)
                {
                    int posY = j * texture.Height;
                    int posX = i * texture.Width - texture.Width;
                    background[i, j] = new BackgroundSprite(texture, posX, posY);
                }
            }
                   
        }

        public void Update(GameWindow window)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
                for (int j = 0; j < nrBackgroundsY; j++)
                    background[i, j].Update(window, nrBackgroundsX);
        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nrBackgroundsX; i++)
                for (int j = 0; j < nrBackgroundsY; j++)
                    background[i, j].Draw(spriteBatch);
        }
    }
 
}
