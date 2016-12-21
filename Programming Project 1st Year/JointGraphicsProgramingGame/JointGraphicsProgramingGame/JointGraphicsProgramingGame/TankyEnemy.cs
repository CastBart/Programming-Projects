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
    class TankyEnemy
    {
        Vector2 position = new Vector2();
        int direction = 0;
        int health = 0;
        int damage = 0;
        float speed = 0;
        bool alive = false;
        static Random rnd = new Random();

        const int North = 1;
        const int East = 2; 
        const int South = 3;
        const int West = 4; 

        Texture2D enemyTexture; 
        Texture2D imageUp; 
        Texture2D imageDown;
        Texture2D imageRight; 
        Texture2D imageLeft;

        Texture2D imageUpReset;
        Texture2D imageDownReset;
        Texture2D imageRightReset;
        Texture2D imageLeftReset;
      
        Texture2D imageUp2;
        Texture2D imageDown2;
        Texture2D imageRight2;
        Texture2D imageLeft2;

        Texture2D imageUp3;
        Texture2D imageDown3;
        Texture2D imageRight3;
        Texture2D imageLeft3;
        


        

        SpriteFont font;

        Rectangle destRect;
        Rectangle sourceRect;
        float elapsed; //the elapsed time
        float delay = 200f;
        int frames = 0;

        
        public void Initialize(int viewPortHeight, int viewPortWidth)
        {
            destRect = new Rectangle(100, 100, 32, 48);
            Spawn(viewPortHeight, viewPortWidth);
            health = 1;
            speed = 1f;
            damage = 40;
            alive = true;
        }
        /// <summary>
        /// loads the contet
        /// </summary>
        /// <param name="theContentManager"></param>
        /// <param name="Image"></param>
        public void LoadContent(ContentManager theContentManager, string Image)
        {
            //level1 sprites
            enemyTexture = theContentManager.Load<Texture2D>("TankyDownAnim");
            imageDown = theContentManager.Load<Texture2D>("TankyDownAnim");
            imageLeft = theContentManager.Load<Texture2D>("TankyLeftAnim");
            imageRight = theContentManager.Load<Texture2D>("TankyRightAnim");
            imageUp = theContentManager.Load<Texture2D>("TankyUpAnim");
            //level2 sprites
            imageDown2 = theContentManager.Load<Texture2D>("TankLevel2Down");
            imageLeft2 = theContentManager.Load<Texture2D>("TankLevel2Left");
            imageRight2 = theContentManager.Load<Texture2D>("TankLevel2Right");
            imageUp2 = theContentManager.Load<Texture2D>("TankLevel2Up");
            //level3 sprites
            imageDown3 = theContentManager.Load<Texture2D>("TankLevel3Down");
            imageLeft3 = theContentManager.Load<Texture2D>("TankLevel3Left");
            imageRight3 = theContentManager.Load<Texture2D>("TankLevel3Right");
            imageUp3 = theContentManager.Load<Texture2D>("TankLevel3Up");
            //reseting sprites
            imageUpReset = theContentManager.Load<Texture2D>("TankyUpAnim");
            imageDownReset = theContentManager.Load<Texture2D>("TankyDownAnim");
            imageRightReset = theContentManager.Load<Texture2D>("TankyRightAnim");
            imageLeftReset =  theContentManager.Load<Texture2D>("TankyLeftAnim");
            //font      eContentManager.Load<Texture2D>("TankyUpAnim");
            font = theContentManager.Load<SpriteFont>("SpriteFont1");
        }

        /// <summary>
        /// draws the enemy spreite and 
        /// displays the health
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        /// <param name="viewportWidth"></param>
        public void Draw(SpriteBatch theSpriteBatch, int viewportWidth)
        {
            if (alive == true)
            {
                theSpriteBatch.Draw(enemyTexture, destRect, sourceRect, Color.White);
             //   theSpriteBatch.DrawString(font, "Enemy Heath: " + health, new Vector2((viewportWidth - 200), 3), Color.Red);
            }
        }
        /// <summary>
        /// upadet the following and the amiation of the sprites
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="playerPosition"></param>
        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            
            if (alive == true)
            {
                Animate(gameTime);
                Follow(playerPosition);
                if (health <= 0)
                {
                    alive = false;
                }
            }
            if (alive == false)
            {
                 position = new Vector2(-100, -100);
            }
        }
        /// <summary>
        /// Depending on what direction the enemy is facing it will move 
        /// him back 60 units in the opposite direction
        /// </summary>
        public void Follow(Vector2 playerPosition)
        {
            destRect = new Rectangle((int)position.X, (int)position.Y, 32, 48);
            if (position.X < playerPosition.X )
            {
                position.X = position.X + speed;
                direction = East; 
                if(position.Y == playerPosition.Y)
                {
                     enemyTexture = imageRight;
                }   
            }

            
            if(position.Y  < playerPosition.Y)
            {
                position.Y = position.Y + speed;
                direction = South;
                enemyTexture = imageDown;
            }
            if(position.X > playerPosition.X)
            {
                position.X = position.X - speed;
                direction = West;
                enemyTexture = imageLeft;
            }
            if(position.Y > playerPosition.Y)
            {
                position.Y = position.Y - speed;
                direction = North;
                enemyTexture = imageUp;
            }
        }
        /// <summary>
        /// changing the textures at level 2 
        /// </summary>
        public void SetTextureLevel2()
        {
            imageDown = imageDown2;
            imageLeft = imageLeft2;
            imageRight = imageRight2;
            imageUp = imageUp2; 
        }
        /// <summary>
        /// changing the textures at level 3 
        /// </summary>
        public void SetTextureLevel3()
        {
            imageDown = imageDown3;
            imageLeft = imageLeft3;
            imageRight = imageRight3;
            imageUp = imageUp3;
        }
        /// <summary>
        /// when the reset button is pressed at any time of the game 
        /// the textures must be the same
        /// </summary>
        public void ResetTextures()
        {
            imageDown = imageDownReset;
            imageLeft = imageLeftReset;
            imageRight = imageRightReset;
            imageUp = imageUpReset; 

        }

        /// <summary>
        /// when the enemy and player collides 
        /// the enemy is moved back a certin distance in the opposite direction
        /// of which he is facing
        /// </summary>
        public void MoveBackWhenHit()
        {
            if(direction == West)
            {
                position = new Vector2(position.X + 60, position.Y);
            }
            if (direction == North)
            {
                position = new Vector2(position.X, position.Y + 60);
            }
            if (direction == East)
            {
                position = new Vector2(position.X - 60, position.Y);
            }
            if (direction == South)
            {
                position = new Vector2(position.X, position.Y - 60);
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

        public int DecHealth(int damageTaken)
        {
            health = health - damageTaken;
            return health;
        }

        public void Spawn(int viewPortHeight, int viewPortWidth)
        {
           
            
           
                int side = rnd.Next(1, 5);
                if(side == 1)
                {
                    position = new Vector2(-10, rnd.Next(0, viewPortHeight));
                }
                if (side == 2)
                {
                    position = new Vector2(rnd.Next(0, viewPortWidth), -10);
                }
                if (side == 3)
                {
                    position = new Vector2(viewPortWidth + 10, rnd.Next(0, viewPortHeight));
                }
                if (side == 4)
                {
                    position = new Vector2(rnd.Next(0, viewPortWidth), viewPortHeight + 10);
                }
            
            
        }


        //properties
        #region Properties
        public int Damage
        {
            get{ return damage; }
            set { damage = value; }
        }
        public Vector2 Position
        {
            get{ return position; }
            set{ position = value; }
            
        }
        public Texture2D Texture
        {
            get{ return enemyTexture; }
            set{ enemyTexture = value; }
        }
        public int Health
        {
            get{ return health; }
            set{ health = value; }
        }
        public float Speed
        {
            get{ return speed; }
            set{ speed = value; }
        }
        public bool Alive
        {
            get{ return alive; }
            set{ alive = value; }
        }
        #endregion

    }
}

