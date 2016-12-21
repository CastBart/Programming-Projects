
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
    class Player
    {
        Vector2 position;
        Vector2 bulletPosition = new Vector2();
        Bullet bullet;
       
        int health = 0;
        int lives = 0;
        int damage = 0;
        int speed = 0;
       
        int direction = 0;
        bool bulletLive = false;
        bool bulletMoving = false;

        Rectangle destRect;
        Rectangle sourceRect;
        float elapsed; //the elapsed time
        float delay = 200f;
        int frames = 0;
        SpriteFont font;
        /// <summary>
        /// 4 different direction which will be used
        /// to move the player
        /// </summary>
        const int North = 1;
        const int East = 2;
        const int South = 3;
        const int West = 4;

        
        const int NorthBullet = 5;
        const int EastBullet = 6;
        const int SouthBullet = 7;
        const int WestBullet = 8;

        /// <summary>
        /// Immages for bullet facing each direction
        /// </summary>
        Texture2D bulletTexture;


        /// <summary>
        /// Images for the player facing each direction
        /// </summary>
        Texture2D startingPlayer;
        Texture2D rightWalk;
        Texture2D leftWalk;
        Texture2D upWalk;
        Texture2D downWalk;

        KeyboardState aCurrentKeyboardState, previousKeyboardState;


        /// <summary>
        /// upload every thing i need eg images sound etc.
        /// </summary>
        /// 
        public void Initialize()
        {
            bullet = new Bullet();
            destRect = new Rectangle(100, 100, 32, 48);
            speed = 3;
           
            bullet.Initialize();
            
            health = 100;
            damage = 40;
            lives = 10;
            position = new Vector2(400, 300);
        }
        /// <summary>
        /// load all the content of the player
        /// </summary>
        /// <param name="theContentManager"></param>
        /// <param name="Image"></param>
        public void LoadContent(ContentManager theContentManager, string Image)
        {
            startingPlayer = theContentManager.Load<Texture2D>("DownAnimation");
            bulletTexture = theContentManager.Load<Texture2D>("shuriken");
            bullet.LoadContent(theContentManager, Image);

            rightWalk = theContentManager.Load<Texture2D>("RightAnimation");
            leftWalk = theContentManager.Load<Texture2D>("LeftAnimation");
            upWalk = theContentManager.Load<Texture2D>("UpAnmiation");
            downWalk = theContentManager.Load<Texture2D>("DownAnimation");

            font = theContentManager.Load<SpriteFont>("SpriteFont1");

        }

        /// <summary>
        /// update every thing in this method 
        /// eg health, position,lives etc. 
        /// </summary>
        public void Update(GameTime gameTime, int viewportHeight, int viewportWidth)
        {
            Animate(gameTime);
            BoundryChecking(viewportHeight, viewportWidth);
            bullet.BulletBoundry(viewportHeight, viewportWidth, position);
            Move();
            

            previousKeyboardState = aCurrentKeyboardState;
            aCurrentKeyboardState = Keyboard.GetState();

            bullet.Update(gameTime, position, leftWalk, rightWalk, upWalk, downWalk, startingPlayer);

            if (health <= 0)
            {
                lives--;
                health = 100;
            }


        }

        /// <summary>
        /// draw the immage of the bullet and player sprites.
        /// </summary>
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(startingPlayer, destRect, sourceRect, Color.White);
            theSpriteBatch.DrawString(font, "Health: " + health, new Vector2(0, 3), Color.White);
            theSpriteBatch.DrawString(font, "Lives: " + lives, new Vector2(0, 20), Color.White);
            if (bullet.Alive)
            {
                bullet.Draw(theSpriteBatch);
            }
        }
        /// <summary>
        /// this method allows the player to fire 
        /// by pressing space
        /// </summary>
       
        /// <summary>
        /// the movement of the bullet
        /// depending where the player is facing
        /// </summary>
       
        /// <summary>
        /// There are 4 Images on a sprite.(imitates movement)
        /// sets each image of the big sprite to frames
        /// starts at 0 frames and ends at 3th frame
        /// source rectangle is set depending on THE FRAME
        /// </summary>
        /// <param name="gameTime"></param>
        private void Animate(GameTime gameTime)
        {

            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(32 * frames, 0, 32, 48);
        }

        #region Move methods
        /// <summary>
        /// changes the direction to west
        /// </summary>
        public void MoveLeft()
        {
            if (direction == West)
            {
                position.X = position.X - speed;


            }

            startingPlayer = leftWalk;
        }
        /// <summary>
        /// changes the direction to East
        /// </summary>
        public void MoveRight()
        {
            if (direction == East)
            {
                position.X = position.X + speed;
            }
            startingPlayer = rightWalk;
        }
        /// <summary>
        /// changes the direction to north
        /// </summary>
        public void MoveUp()
        {
            if (direction == North)
            {
                position.Y = position.Y - speed;
            }
            startingPlayer = upWalk;
        }
        /// <summary>
        /// changes the direction to south
        /// </summary>
        public void MoveDown()
        {
            if (direction == South)
            {
                position.Y = position.Y + speed;
            }
            startingPlayer = downWalk;
        }
        #endregion

        /// <summary>
        /// uses keyboard state depending on what keys are pressed
        /// movvign the player on a ceritn key into a certin direction
        /// </summary>
        public void Move()
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            destRect = new Rectangle((int)position.X, (int)position.Y, 32, 48);
            //on left arrow key the player will move left
            if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
            {

                direction = West;
                MoveLeft();

            }
            //on right arrow key the player will move right
            else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
            {

                direction = East;
                MoveRight();

            }
            //on up arrow key the player will move up
            else if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
            {

                direction = North;
                MoveUp();

            }
            //on down arrow key the player will move down
            else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
            {

                direction = South;
                MoveDown();

            }
            //the image stays dows not animate if no keys are pressed
            else
            {
                sourceRect = new Rectangle(0, 0, 32, 48);
            }

        }


        /// <summary>
        /// checks the position of the player and makes him
        /// not go out of the screen height and width
        /// </summary>
        /// <param name="viewportHeight"></param>
        /// <param name="viewportWidth"></param>
        public void BoundryChecking(int viewportHeight, int viewportWidth)
        {
            if (position.X <= 10)
            {
                position.X = 10;
            }
            if (position.X + (startingPlayer.Width - 100) >= (viewportWidth - 10))
            {
                position.X = viewportWidth - (startingPlayer.Width - 90);
            }
            if (position.Y <= 10)
            {
                position.Y = 10;
            }
            if ((position.Y + startingPlayer.Height) >= (viewportHeight - 10))
            {
                position.Y = viewportHeight - (startingPlayer.Height + 10);

            }

        }

        /// <summary>
        /// checks the position of the bullet and makes it
        /// respawn at the players position
        /// </summary>
        /// <param name="viewportHeight"></param>
        /// <param name="viewportWidth"></param>
       
        //players collision with the enemy tank
        public void EnemyCollision(TankyEnemy enemy)
        {
            if (enemy.Alive == true)
            {
                int width = (enemy.Texture.Width - 100);
                int height = enemy.Texture.Height;
                int damage = enemy.Damage;
                Vector2 enemyPos = enemy.Position;

                if (position.X + (startingPlayer.Width - 100) < enemyPos.X
                ||
                position.Y + startingPlayer.Height < enemyPos.Y
                ||
                position.X > enemyPos.X + width
                ||
                position.Y > enemyPos.Y + height)
                {

                }
                else
                {
                    enemy.MoveBackWhenHit();
                    DecHealth(damage);
                }
            }
        }
        //bullets collision with the enemy tank
        public void BulletCollision(TankyEnemy enemy)
        {
            bullet.BulletCollision(enemy, damage, position);
        }

        public void BulletCollision(Boss enemy)
        {
            bullet.BulletCollision(enemy, damage, position);
        }

        //bullets collision with the fast enemy
        public void BulletCollision(FastEnemy enemy)
        {
            bullet.BulletCollision(enemy, damage, position);
        }
        //players collision with the fast enemy
        public void EnemyCollision(FastEnemy enemy)
        {
            if (enemy.Alive)
            {
                int width = (enemy.Texture.Width - 100);
                int height = enemy.Texture.Height;
                int damage = enemy.Damage;
                Vector2 enemyPos = enemy.Position;

                if (position.X + (startingPlayer.Width - 100) < enemyPos.X
                ||
                position.Y + startingPlayer.Height < enemyPos.Y
                ||
                position.X > enemyPos.X + width
                ||
                position.Y > enemyPos.Y + height)
                {

                }
                else
                {
                    enemy.MoveBackWhenHit();
                    //DecHealth(damage);
                }
            }
        }
       //player collidiong with the boss
        public void EnemyCollision(Boss enemy)
        {
            if (enemy.Alive == true)
            {
                int width = (enemy.Texture.Width - 300);
                int height = enemy.Texture.Height;
                int damage = enemy.Damage;
                Vector2 enemyPos = enemy.Position;

                if (position.X + (startingPlayer.Width - 100) < enemyPos.X
                ||
                position.Y + startingPlayer.Height < enemyPos.Y
                ||
                position.X > enemyPos.X + width
                ||
                position.Y > enemyPos.Y + height)
                {

                }
                else
                {
                    enemy.MoveBackWhenHit();
                    DecHealth(damage);
                }
            }
        }

        //bullets collision with the boss
       

        /// <summary>
        /// decreasing of lives
        /// </summary>
        /// <returns></returns>
        public int DecLives()
        {
            lives--;
            return lives;
        }
        /// <summary>
        /// decreasing of health depending on enemies damage
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <returns></returns>
        public int DecHealth(int damageTaken)
        {
            health = health - damageTaken;
            return health;
        }

        /// <summary>
        /// Properties begin here
        /// </summary>
        #region Properies
       
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public Texture2D Texture
        {
            get
            {
                return startingPlayer;
            }
            set
            {
                startingPlayer = value;
            }
        }

        public Vector2 PositionBullet
        {
            get
            {
                return bulletPosition;
            }
            set
            {
                bulletPosition = value;
            }
        }
        public Texture2D PlayerTexture
        {
            get
            {
                return startingPlayer;
               
            }
        }
        public Texture2D Left
        {
            get
            {
                return leftWalk;
                //startingPlayer;
                //rightWalk;
                //leftWalk;
                //upWalk;
                //downWalk;
            }
        }
        public Texture2D Right
        {
            get
            {
                return rightWalk;
            }
        }
        public Texture2D Up
        {
            get
            {
                return upWalk;
            }
        }
        public Texture2D Down
        {
            get
            {
                return downWalk;
            }
        }
       
        public Texture2D BulletTexture
        {
            get
            {
                return bulletTexture;
            }
        }
        public int Health
        {
            get
            {
                return health;
            }
        }
        public int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                lives = value;
            }
        }
        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }
        public bool BulletLive
        {
            get
            {
                return bulletLive;
            }
            set
            {
                bulletLive = value;
            }
        }
        public bool BulletMove
        {
            get
            {
                return bulletMoving;
            }
            set
            {
                bulletMoving = value;
            }
        }
        public int Direction
        {
            get { return direction; }
        }
      

        #endregion
    }
}


