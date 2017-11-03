using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace nicegame
{
    class GameObject
    {
        protected Texture2D texture;
        public Vector2 vector;
        protected Vector2 size;
 

        public GameObject(Texture2D texture, float X, float Y)
        {
            this.texture = texture;
            this.vector.X = X;
            this.vector.Y = Y;
            size = new Vector2(texture.Width, texture.Height);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(texture, vector, Color.White);
        
        }

        public float X { get { return vector.X; } set { vector.X = value; } }
        public float Y { get { return vector.Y; } set { vector.Y = value; } }
        public float Width { get { return size.X; } }
        public float Height { get { return size.Y; } }
    }
    abstract class MovingObject : GameObject
    {
        protected Vector2 speed;
        public MovingObject(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y)
        {
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }
    }
   
    abstract class PhysicalObject : MovingObject
    {
        
        public bool isAlive = true;


        public PhysicalObject(Texture2D texture, float X, float Y, float speedX, float speedY)
            : base(texture, X, Y, speedX, speedY)
        {
        }

        public bool CheckCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), Convert.ToInt32(Width), Convert.ToInt32(Height));
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X), Convert.ToInt32(other.Y), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height));
            return myRect.Intersects(otherRect);
        }
        public bool IsAlive { get { return isAlive; }
            set { isAlive = value; } }
        
           
         
        

  
        }
        

        
    }


