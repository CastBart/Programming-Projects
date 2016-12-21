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

namespace JointGraphicsProgramingGame
{
    class Bullet
    {
        Vector2 position;
        Texture2D texture;
        Texture2D texture2;
        float speed;
        bool alive;
        int direction = 0;
        const int Up = 1;
        const int Right = 2;
        const int Down = 3;
        const int Left = 4;
        KeyboardState aCurrentKeyboardState, previousKeyboardState;
       
        public void Initialize()
        {
            alive = false;
            speed = 4;
            
        }
        public void Update(GameTime gameTime, Vector2 playerPosition, Texture2D leftWalk, Texture2D rightWalk, Texture2D upWalk, Texture2D downWalk, Texture2D startingPlayer)
        {
            previousKeyboardState = aCurrentKeyboardState;
            aCurrentKeyboardState = Keyboard.GetState();

            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) && alive == false && previousKeyboardState.IsKeyUp(Keys.Space))
            {
                Fire(playerPosition, leftWalk, rightWalk, upWalk, downWalk, startingPlayer);
            }

            Move();
        }

        public void LoadContent(ContentManager theContentManager, string Image)
        {
            texture = theContentManager.Load<Texture2D>("shuriken");
            texture2 = theContentManager.Load<Texture2D>("shuriken");
        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            if(alive)
            {
                theSpriteBatch.Draw(texture, position, Color.White);
            }
        }

        public void Fire(Vector2 playerPosition, Texture2D leftWalk, Texture2D rightWalk, Texture2D upWalk, Texture2D downWalk, Texture2D startingPlayer)
        {


            position = playerPosition;
            alive = true;

            if (alive == true)
            {
                //checking which direction the player 
                //is facing and then shoots the bullet in that direction
                if (startingPlayer == rightWalk)
                {
                    direction = Right;
                }
                if (startingPlayer == leftWalk)
                {
                    direction = Left;
                }
                if (startingPlayer == upWalk)
                {
                    direction = Up;
                }
                if (startingPlayer == downWalk)
                {
                    direction = Down;
                }
            }
        }

        /// <summary>
        /// moves the bullet depending on the facing of the player
        /// </summary>
        public void Move()
        {
            if (alive == true)
            {
                //moves West
                if (direction == Left)
                {
                    position.X = position.X - speed;
                }
                //moves East
                if (direction == Right)
                {
                    position.X = position.X + speed;
                }
                //moves North
                if (direction == Up)
                {
                    position.Y = position.Y - speed;
                }
                //moves South
                if (direction == Down)
                {
                    position.Y = position.Y + speed;
                }

            }
        }
        /// <summary>
        /// bullet resets when it hits the boundries of the screen
        /// </summary>
        /// <param name="viewportHeight"></param>
        /// <param name="viewportWidth"></param>
        /// <param name="playerPosition"></param>
        public void BulletBoundry(int viewportHeight, int viewportWidth, Vector2 playerPosition)
        {
            if (position.X <= 0)
            {

                position = playerPosition;
                alive = false;
                
            }
            if ((position.X + texture.Width) >= viewportWidth)
            {

                position = playerPosition;
                alive = false;
                
            }
            if (position.Y <= 0)
            {

                position = playerPosition;
                alive = false;
                
            }
            if ((position.Y + texture.Height) >= viewportHeight)
            {

                position = playerPosition;
                alive = false;
               
            }
        }
        /// <summary>
        /// collision with enemy and the bullet
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="damage"></param>
        /// <param name="playerPos"></param>
        public void BulletCollision(TankyEnemy enemy, int damage, Vector2 playerPos)
        {

            if (enemy.Alive == true)
            {
                if (alive)
                {
                    int width = enemy.Texture.Width - 100;
                    int height = enemy.Texture.Height;

                    Vector2 enemyPos = enemy.Position;

                    if (position.X + texture.Width < enemyPos.X
                    ||
                    position.Y + texture.Height < enemyPos.Y
                    ||
                    position.X > enemyPos.X + width
                    ||
                    position.Y > enemyPos.Y + height)
                    {

                    }
                    else
                    {
                        if (alive == true)
                        {
                            enemy.Alive = false;
                        }
                        position = playerPos;
                        alive = false;

                    }
                }
            }
        }
        /// <summary>
        /// bullet-boss collision
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="damage"></param>
        /// <param name="playerPos"></param>
        public void BulletCollision(Boss enemy, int damage , Vector2 playerPos)
        {
            if (enemy.Alive == true)
            {
                int width = enemy.Texture.Width - 300;
                int height = enemy.Texture.Height;

                Vector2 enemyPos = enemy.Position;

                if (position.X + texture.Width < enemyPos.X
                ||
                position.Y + texture.Height < enemyPos.Y
                ||
                position.X > enemyPos.X + width
                ||
                position.Y > enemyPos.Y + height)
                {

                }
                else
                {
                    if (alive == true)
                    {
                        enemy.DecHealth(damage);
                    }
                    position = playerPos;
                    alive = false;
                   
                }
            }
        }

        //bullets collision with the fast enemy
        public void BulletCollision(FastEnemy enemy,int damage, Vector2 playerPos)
        {
            if (enemy.Alive == true)
            {
                int width = enemy.Texture.Width - 100;
                int height = enemy.Texture.Height;

                Vector2 enemyPos = enemy.Position;

                if (position.X + texture.Width < enemyPos.X
                ||
                position.Y + texture.Height < enemyPos.Y
                ||
                position.X > enemyPos.X + width
                ||
                position.Y > enemyPos.Y + height)
                {

                }
                else
                {
                    if (alive == true)
                    {
                        enemy.Alive = false;
                    }
                    position = playerPos;
                    alive = false;
                  
                }
            }
        }
        public void BulletCollision(Player enemy, Vector2 playerPos)
        {
            if (enemy.Lives >=0)
            {
                int width = enemy.Texture.Width - 100;
                int height = enemy.Texture.Height;

                Vector2 enemyPos = enemy.Position;

                if (position.X + texture.Width < enemyPos.X
                ||
                position.Y + texture.Height < enemyPos.Y
                ||
                position.X > enemyPos.X + width
                ||
                position.Y > enemyPos.Y + height)
                {

                }
                else
                {
                    if (enemy.Lives >= 0)
                    {
                        enemy.Lives--;
                    }
                    position = playerPos;
                    alive = false;

                }
            }
        }




        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public  int LeftDirection
        {
            get { return Left; }
            
        }
        public int RightDirection
        {
            get { return Right; }
            
        }
        public int UpDirection
        {
            get { return Up; }
            
        }
        public int DownDirection
        {
            get { return Down; }
            
        }
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


    }
}
