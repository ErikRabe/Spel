using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Spel
{
    class Wall : PhysicalObject
    {

        int frame = 0;
        double frameBuffer = 0;
        double animationTime = 0.05;
     
       


        public override void Draw(SpriteBatch spriteBatch)
        {

            Rectangle rekt = new Rectangle((texture.Width) * frame, 0, texture.Width, texture.Height);
            spriteBatch.Draw(texture, vector, Color.White);
            //spriteBatch.Draw(texture, vector, Color.White);

        }


        public Wall(Texture2D texture, float X, float Y, float speedX, float speedY) : base(texture, X, Y, speedX, speedY)
        {

            size = new Vector2(texture.Width + 3, texture.Height + 3);



        }
        public void Update(GameWindow window, GameTime gameTime, SoundEffect effect)
        {

        }

    }
}
