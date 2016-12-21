using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//Worked on by Cathal Ryan C00201715
//
namespace Asteroids_Re_Loaded
{
    class Enemy
    {
        bool enemyAlive; //Bool for if enemy is Alive or not
        Vector2 position; //Centre of the enemy ship
        Vector2 origin; //Heading of the enemy ship
        Vector2 velocity; //Velocity of the enemy ship
        Vector2 distance; //distance between enemy and player
        int health; // enemies health
        float turnRate; // how fast can the ship turn
        float thrust; // the speed of the enemy ship
        float friction; // gradually slowing down when moving and not speeding up
        int shipSize; // size of the ship
        Texture2D texture; //Enemy Ship Texture

        //these variables are used for animation of the enemies thruster
        Rectangle destinationRectangle;
        Rectangle sourceRectangle;
        Rectangle enemyRectangle;
        float elapsed; //the elapsed time
     

        public Bullet[] bulletArray; //Array of bullets so enemy can fire several consecutive shots
        const int MaxBullet = 10; //Amount of bullets ship is allowed to have on screen

        //Initialize the variables
        public void Initialize()
        {
            turnRate = 0;
            Spawn(Game.viewportWidth, Game.viewportHeight);
            friction = 0.002f;
            thrust = 0.1f;
            health = 100;
            bulletArray = new Bullet[MaxBullet];
            for (int i = 0; i < bulletArray.Length; i++)
            {
                bulletArray[i] = new Bullet();
            }
            enemyAlive = true;
        }
        //Load in enemy ship texture and bullet array
        public void LoadContent(ContentManager theContentManager)
        {
            texture = theContentManager.Load<Texture2D>("EnemyTexture");
           
            for (int i = 0; i < bulletArray.Length; i++)
            {
                bulletArray[i].LoadContent(theContentManager, "enemybullet");
            }

        }
        public void Update(GameTime gameTime, int width, int height, Player player,Enemy enemy)
        {
            if (enemyAlive && player.Alive)
            {
                enemyRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                origin = new Vector2(enemyRectangle.Width / 2, enemyRectangle.Height / 2);
                BoundryChecking(width, height, texture);
                Move(gameTime, 80, 75, player, enemy);
                Shoot(gameTime, enemy);
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    if (bulletArray[i].Alive)
                    {
                        bulletArray[i].Update(gameTime, this);
                        bulletArray[i].Boundary(width, height);
                    }
                }
            }
            

        }
        public void Spawn(int viewPortHeight, int viewPortWidth)
        {
           // speedX = rnd.Next(-4, 5);
           // speedY = rnd.Next(-4, 5);
            int side = Asteroids.rnd.Next(1, 5);
            if (side == 1)
            {
                position = new Vector2(0, Asteroids.rnd.Next(0, viewPortHeight));
            }
            if (side == 2)
            {
                position = new Vector2(Asteroids.rnd.Next(0, viewPortWidth), 0);
            }
            if (side == 3)
            {
                position = new Vector2(viewPortWidth, Asteroids.rnd.Next(0, viewPortHeight));
            }
            if (side == 4)
            {
                position = new Vector2(Asteroids.rnd.Next(0, viewPortWidth), viewPortHeight);
            }
        }





        //Method for moving enemy ship randomly around the screen
        public void Move(GameTime gameTime, int frameWidth, int frameHeight, Player player, Enemy enemy)
        {
            distance.X = -(position.X - player.Position.X);
            distance.Y = -(position.Y - player.Position.Y);

            turnRate = (float)Math.Atan2(distance.Y, distance.X);
            
            Vector2 direction = player.Position - position;
            float angleToRotate = (float)Math.Atan2(direction.Y, direction.X);

            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            position = velocity + position;
            origin = new Vector2(destinationRectangle.Width / 2, destinationRectangle.Height / 2);

            //add thrust power
            if (enemyAlive == true)
            {
                thrust += 0.1f;
               // Animate(gameTime, frameWidth, frameHeight);
                if (thrust >= 1) thrust = 1;
              
                velocity.X = (float)Math.Cos(turnRate) * thrust; //veclocity follows the same path as the turnRtae and multiplies it by the thrust power
                velocity.Y = (float)Math.Sin(turnRate) * thrust; //by using turnRate, the ship will always fly where its pointng(works with the turning above) 
            }

            else if (position != Vector2.Zero)
            {
               
                Vector2 i = velocity;
                velocity = i - friction * i;
                sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
                if (thrust >= 0) thrust -= 0.1f;
            }
           
            
        }

        public void Shoot(GameTime gameTime, Enemy enemy)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < bulletArray.Length; i++)
            {
                if (elapsed < 2)
                {

                }
                else
                {
                    bulletArray[i].Position = position;
                    bulletArray[i].TurnRate = turnRate;
                    bulletArray[i].Fire(gameTime);
                    elapsed = 0;
                }
            }
        }
        public void BoundryChecking(int viewportWidth, int viewportHeight, Texture2D texture)
        {
            if (position.X + (texture.Width) <= 0)
            {
                position.X = viewportWidth;
            }
            if (position.X - 40 >= (viewportWidth))
            {
                position.X = 0 - texture.Width;
            }
            if (position.Y + texture.Height <= 0)
            {
                position.Y = viewportHeight - 1;
            }
            if (position.Y - 40 >= viewportHeight)
            {
                position.Y = 0 - texture.Height;
            }
        }

       
        //When shot once by player, enemy should despawn 
        public void Die()
        {

        }

        public void Draw(SpriteBatch sp, Player player)
        {
            if (enemyAlive && player.Alive)
            {
                sp.Draw(texture, position, new Rectangle(0,0,80,75), Color.White, turnRate, origin, 1f, SpriteEffects.None, 0);
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    bulletArray[i].Draw(sp);
                }
            }
        }

        public float TurnRate
        {
            get { return turnRate; }
            set { turnRate = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle EnemyRect
        {
            get { return enemyRectangle; }
            set { enemyRectangle = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public bool EnemyAlive
        {
            get { return enemyAlive; }
            set { enemyAlive = value; }
        }
    }

}
