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
   class FastEnemy
    {

        Vector2 position = new Vector2();
        int direction = 0;
        int health = 0;
        int damage = 0;
        float speed = 0;
        bool alive = false;

        
        const int North = 1;
        const int East = 2;
        const int South = 3;
        const int West = 4;

        SpriteFont font;
       //starting sprites
        Texture2D enemyTexture;
        Texture2D imageUp;
        Texture2D imageDown;
        Texture2D imageRight;
        Texture2D imageLeft;
       //level 2 sprites
        Texture2D imageUp2;
        Texture2D imageDown2;
        Texture2D imageRight2;
        Texture2D imageLeft2;
       //level 3 sprites 
        Texture2D imageUp3;
        Texture2D imageDown3;
        Texture2D imageRight3;
        Texture2D imageLeft3;
       //sprites used for reseting
        Texture2D imageUpReset;
        Texture2D imageDownReset;
        Texture2D imageRightReset;
        Texture2D imageLeftReset;

        float elapsed; //the elapsed time
        float delay = 2f;

       /// <summary>
       /// Animation variables
       /// </summary>
        Rectangle destRect;
        Rectangle sourceRect;
        int frames = 0;
        float delayAnimation = 200f;
        float elapsedAnimation;

        public void Initialize()
        {
            position = new Vector2(400, -48);
            health = 300;
            speed = 3f;
            damage = 40;
            alive = true;
        }
        /// <summary>
        /// Load all the content
        /// </summary>
        /// <param name="theContentManager"></param>
        /// <param name="Image"></param>
        public void LoadContent(ContentManager theContentManager, string Image)
        {
            //sprites that the draw method always draws
            enemyTexture = theContentManager.Load<Texture2D>("FastDown");
            imageDown = theContentManager.Load<Texture2D>("FastDown");
            imageLeft = theContentManager.Load<Texture2D>("FastLeft");
            imageRight = theContentManager.Load<Texture2D>("FastRight");
            imageUp = theContentManager.Load<Texture2D>("FastUp");
            //images for level 2
            imageDown2 = theContentManager.Load<Texture2D>("FastLevel2Down");
            imageLeft2 = theContentManager.Load<Texture2D>("FastLevel2Left");
            imageRight2 = theContentManager.Load<Texture2D>("FastLevel2Right");
            imageUp2 = theContentManager.Load<Texture2D>("FastLevel2Up");
            //sprites for level 3
            imageDown3 = theContentManager.Load<Texture2D>("FastLevel3Down");
            imageLeft3 = theContentManager.Load<Texture2D>("FastLevel3Left");
            imageRight3 = theContentManager.Load<Texture2D>("FastLevel3Right");
            imageUp3 = theContentManager.Load<Texture2D>("FastLevel3Up");
            //reset the game sprites
            imageDownReset = theContentManager.Load<Texture2D>("FastDown");
            imageLeftReset = theContentManager.Load<Texture2D>("FastLeft");
            imageRightReset = theContentManager.Load<Texture2D>("FastRight");
            imageUpReset = theContentManager.Load<Texture2D>("FastUp");
            // font
            font = theContentManager.Load<SpriteFont>("SpriteFont1");
        }

        /// <summary>
        /// update every thing in this method 
        /// eg health, position,lives etc. 
        /// </summary>
        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            //This if statement
            //makes the movement of the sprite for 2 seconds
            //and then stoping for 1 second
            if (alive == true)
            {
                elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
               
                if (elapsed <= delay)
                {
                    Follow(playerPosition);
                    
                }
                
                //makes the movement stop for 1 second
                if(elapsed >=3)
                {
                    elapsed = 0;
                }
               
            }
            if (!alive)
            {
                position = new Vector2(-100, -100);
            }
            Animate(gameTime);
            
        }

        /// <summary>
        /// draw the immage of the bullet and player sprites.
        /// </summary>
        public void Draw(SpriteBatch theSpriteBatch, int viewportWidth)
        {
            if (alive)
            {
                theSpriteBatch.Draw(enemyTexture, destRect, sourceRect, Color.White);
              //  theSpriteBatch.DrawString(font, "Enemy Heath: " + health, new Vector2((viewportWidth - 200), 3), Color.Red);
            }
        }
       /// <summary>
       /// There are 4 Images on a sprite.(imitates movement)
       /// sets each image of the big sprite to frames
       /// starts at 0 frames and ends at 3th frame
       /// source rectangle is set depending on THE FRAME
       /// </summary>
       /// <param name="gameTime"></param>
        private void Animate(GameTime gameTime)
        {

            elapsedAnimation += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedAnimation >= delayAnimation)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsedAnimation = 0;
            }
            sourceRect = new Rectangle(32 * frames, 0, 32, 48);
        }
       /// <summary>
       /// the fast enemy follows but can go in one direction at once 
       /// </summary>
       /// <param name="playerPosition"></param>
        public void Follow(Vector2 playerPosition )
        {
           

            destRect = new Rectangle((int)position.X, (int)position.Y, 32, 48);
            if (position.X < playerPosition.X - 2)
            {
                position.X = position.X + speed;
                direction = East;
                enemyTexture = imageRight;
            }


            else if (position.Y < playerPosition.Y - 2 )
            {
                position.Y = position.Y + speed;
                direction = South;
                enemyTexture = imageDown;
            }
            else if (position.X > playerPosition.X + 2)
            {
                position.X = position.X - speed;
                direction = West;
                enemyTexture = imageLeft;
            }
            else if (position.Y > playerPosition.Y + 2)
            {
                position.Y = position.Y - speed;
                direction = North;
                enemyTexture = imageUp;
            }
            
            
        }
       /// <summary>
       /// Depending on what direction the enemy is facing it will move 
       /// him back 80 units in the opposite direction
       /// </summary>
        public void MoveBackWhenHit()
        {
            if (direction == West)
            {
                position = new Vector2(position.X + 80, position.Y);
            }
            if (direction == North)
            {
                position = new Vector2(position.X, position.Y + 80);
            }
            if (direction == East)
            {
                position = new Vector2(position.X - 80, position.Y);
            }
            if (direction == South)
            {
                position = new Vector2(position.X, position.Y - 80);
            }

        }
       /// <summary>
       /// set the images of the sprites to be diferent when level 1 is complete
       /// </summary>
        public void SetTextureLevel2()
        {
            imageDown = imageDown2;
            imageLeft = imageLeft2;
            imageRight = imageRight2;
            imageUp = imageUp2;
        }
        public void SetTextureLevel3()
        {
            imageDown = imageDown3;
            imageLeft = imageLeft3;
            imageRight = imageRight3;
            imageUp = imageUp3;
        }
       /// <summary>
       /// when game is reseted the game will draw the very first sprite images(level 1)
       /// </summary>
        public void ResetTextures()
        {
            imageDown = imageDownReset;
            imageLeft = imageLeftReset;
            imageRight = imageRightReset;
            imageUp = imageUpReset;

        }
      
        public int DecHealth(int damageTaken)
        {
            health = health - damageTaken;
            return health;
        }


        #region Properties
        public int Damage
        {
            get
            {
                return damage;
            }
        }
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
                return enemyTexture;
            }
        }
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        public bool Alive
        {
            get
            {
                return alive;
            }
            set
            {
                alive = value;
            }
        }
        #endregion
    }
}

