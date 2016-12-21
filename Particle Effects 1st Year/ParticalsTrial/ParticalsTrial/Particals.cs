using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace ParticalsTrial
{
    class Particals
    {

        Vector2 position, velocity;
        Texture2D texture;
        float lifeSpam;
        static ContentManager content;
        enum Direction { Up, Left, Right, Down , Normal};
        Direction currentDirection = Direction.Normal;
        public Particals()
        {
           
        }

        public Particals(Vector2 newVeclocity, float newLifeSpam, Vector2 newPosition, int newDirection)
        {
            texture = content.Load < Texture2D>("GreenPartical");
            position = newPosition;
            velocity = newVeclocity;
            lifeSpam = newLifeSpam;
            if (newDirection == 1) currentDirection = Direction.Up;
            if (newDirection == 2) currentDirection = Direction.Left;
            if (newDirection == 3) currentDirection = Direction.Right;
            if (newDirection == 4) currentDirection = Direction.Down;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("GreenPartical");
        }
        

        public void Update()
        {
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
