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
    class Boss
    {

        Vector2 position;
        Texture2D texture;
        Texture2D up;
        Texture2D down;
        Texture2D left;
        Texture2D right;
        int health;
        float speed;
        int damage;

        bool alive;
        int direction = 0;
        const int North = 1;
        const int East = 2;
        const int South = 3;
        const int West = 4; 


        Rectangle destRect;
        Rectangle sourceRect;
        int frames = 0;
        float delay = 200f;
        float elapsed;

        SpriteFont font;

        /// <summary>
        /// initialization of the boss class
        /// </summary>
        public void Initialize()
        {
            destRect = new Rectangle(100, 100, 95, 79);
            damage = 100;
            alive = false;
            speed = 1.4f;
            health = 1000;
        }
        /// <summary>
        /// loading the content of the boss class
        /// </summary>
        /// <param name="theContentManager"></param>
        /// <param name="Image"></param>
        public void LoadContent(ContentManager theContentManager, string Image)
        {
            up = theContentManager.Load<Texture2D>("DemonUp");
            down = theContentManager.Load<Texture2D>("DemonDown");
            right = theContentManager.Load<Texture2D>("DemonRight");
            left = theContentManager.Load<Texture2D>("DemonLeft");
            font = theContentManager.Load<SpriteFont>("SpriteFont1");
        }

        /// <summary>
        /// update of the class contains following and animation
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
            
        }

        /// <summary>
        /// drawing the boss
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        public void Draw(SpriteBatch theSpriteBatch, int viewportWidth)
        {
            if (alive == true)
            {
                theSpriteBatch.Draw(texture, destRect, sourceRect, Color.White);
                theSpriteBatch.DrawString(font, "Enemy Heath: " + health, new Vector2((viewportWidth - 200), 3), Color.Red);
            }
        }
        /// <summary>
        /// this meathod of following uses simple position checking
        /// and the boss follows the player by passing the players position
        /// </summary>
        /// <param name="playerPosition"></param>
        public void Follow(Vector2 playerPosition)
        {
            destRect = new Rectangle((int)position.X, (int)position.Y, 95, 79);
            if (position.X < playerPosition.X)
            {
                position.X = position.X + speed;
                direction = East;
                if (position.Y == playerPosition.Y)
                {
                    texture = right;
                }
            }

            if (position.Y < playerPosition.Y)
            {
                position.Y = position.Y + speed;
                direction = South;
                texture = down;
            }
            if (position.X > playerPosition.X)
            {
                position.X = position.X - speed;
                direction = West;
                texture = left;
            }
            if (position.Y > playerPosition.Y)
            {
                position.Y = position.Y - speed;
                direction = North;
                texture = up;
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
            sourceRect = new Rectangle(95 * frames, 0, 95, 79);
        }
        /// <summary>
        /// when the enemy and player collides 
        /// the enemy is moved back a certin distance in the opposite direction
        /// of which he is facing
        /// </summary>
        public void MoveBackWhenHit()
        {
            if (direction == West)
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
        /// decreasing the health of the boss when struck by a shuriken
        /// </summary>
        /// <param name="damageTaken"></param>
        /// <returns></returns>
        public int DecHealth(int damageTaken)
        {
            health = health - damageTaken;
            return health;
        }



        //properties
        #region Properties
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }

        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
       
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
    }
        #endregion
}
