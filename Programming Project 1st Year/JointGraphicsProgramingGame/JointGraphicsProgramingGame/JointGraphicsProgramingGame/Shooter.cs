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

namespace JointGraphicsProgramingGame
{
    class Shooter
    {
        Vector2 position;
        
        float speed;
        bool alive;
        int direction;
        Bullet bullet = new Bullet();

        const int North = 1;
        const int East = 2;
        const int South = 3;
        const int West = 4;

        Texture2D mainTexture;
        Texture2D imageRight;
        Texture2D imageLeft;
        float elapsed;

        static Random rnd = new Random();
       
        public void Initialize(int viewportWidth)
        {
            position = new Vector2(400,10);
            Spawn(1,3);
            speed = 3f;
           
            alive = true;
            bullet.Position = position;
        }

        public void LoadContent(ContentManager theContentManager, string Image)
        {
            //sprites that the draw method always draws
            mainTexture = theContentManager.Load<Texture2D>("LeftShooter");
            imageLeft = theContentManager.Load<Texture2D>("LeftShooter");
            imageRight = theContentManager.Load<Texture2D>("RightShooter");
            bullet.LoadContent(theContentManager, Image);
        }

        public void Update(GameTime gameTime, int viewportWidth, int viewportHight)
        {
            if (alive)
            {
                elapsed = gameTime.TotalGameTime.Seconds;
                if (elapsed >= 1.5f)
                {
                    bullet.Alive = true;
                }
               
                if(elapsed >= 4)
                {
                    elapsed = 0;
                }
                if (bullet.Alive)
                {
                    bullet.Position = bullet.Position + new Vector2(0, 5); 
                }


                bullet.BulletBoundry(viewportHight, viewportWidth, position); 
                Move();
                ChangeDirection(viewportWidth);
                
            }
            if (!alive)
            {
                position = new Vector2(-100, -100);
            }
        }

        public void CollisionWithPlayer(Player enemy, Vector2 playerPos)
        {
            bullet.BulletCollision(enemy, playerPos);
        }


        public void Draw(SpriteBatch theSpriteBatch)
        {
            if (alive)
            {
                theSpriteBatch.Draw(mainTexture, position, Color.White);
                bullet.Draw(theSpriteBatch);
            }
        }

        public void ChangeDirection(int viewportWidth)
        {
            if (position.X >= viewportWidth - mainTexture.Width)
            {
                direction = West;
                mainTexture = imageLeft;
            }
            if(position.X <= 0)
            {
                direction = East;
                mainTexture = imageRight;
            }
        }
       
        public void Spawn(int x, int y)
        {



            int side = rnd.Next(1, 3);
            if (side == 1)
            {
                direction = East;
            }
            if (side == 2)
            {
                direction = West;
            }
            if (side == 3)
            {
                direction = North;
            }
            if (side == 4)
            {
                direction = South;
            }
        }

        public void Move()
        {
            if(direction == East)
            {
                position.X += speed;
            }

            if (direction == West)
            {
                position.X -= speed;
            }

        }

        public Vector2 Position
        {
            get { return position; }
        }

    }
}
