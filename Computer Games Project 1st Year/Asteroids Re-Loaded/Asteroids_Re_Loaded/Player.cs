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


/* Worked on by Bartosz Zych
 * Total Spent time
 * --- 17 hours---
 */ 
namespace Asteroids_Re_Loaded
{
    class Player
    {
        #region Variables
        Vector2 position, origin, shipHeading, velocity; // represents the position of the players ship(centre of the image)
        Texture2D texture, opheliaTexture, othelloTexture, oberonTexture, fuelTexture, shieldTexture, shieldOn; // the sprite of the players ship
        SoundEffect fire;
       


        public enum ShipState { Ophelia, Othello, Oberon };
        public static ShipState currentShip = ShipState.Ophelia;

        int health; // abount of health
        int shipsize; // size of the ship
        public int currentHold;
        public Int64 credits = 90000;
        float turnRate; // how fast can the ship turn
        float thrust; // the speed of the players ship
        float fuel; // amount of fuel
        float shieldAmount;
        float friction; // gradually slowing down when moving and not speeding up
        public float decFuel = 0.3f;
        public float decShiled = 0.3f;
        bool alive; // either alive or dead
        bool shield; // if the shield is on or of
        SpriteFont font;

        public int holdCapacity = 10;
        const int MaxBullet = 10;
        public int maxSpeed = 5;
        public float speedingUp = 0.01f;
        public float addTurning = 0.05f;

        //these variables are used for animation of the flame
        Rectangle destinationRect, sourceRect, fuelRectangle, shieldRect, playerRectangle;
        float elapsed; 
        float delay = 200f;
        int frames = 0;
        float newElapsed;
        float deadElapsed;

        

        //bullets
        public Bullet[] bulletArray;
        #endregion

        #region Initialzie, Load, Update, Draw
        public void Initialize()
        {
            turnRate = 0;
            position = new Vector2(300, 200);
            velocity.X = 0;
            velocity.Y = 0;
            friction = 0.005f;
            thrust = 0.1f;
            health = 100;
            fuel = 100;
            shieldAmount = 100;

            bulletArray = new Bullet[MaxBullet];
            for (int i = 0; i < bulletArray.Length; i++)
            {
                bulletArray[i] = new Bullet();
            }
            alive = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager content, string Image)
        {
            opheliaTexture = content.Load<Texture2D>("Ophelia");
            othelloTexture = content.Load<Texture2D>("Othello");
            oberonTexture = content.Load<Texture2D>("OberonMovement");
            fuelTexture = content.Load<Texture2D>("FuelBar");
            health = 200;
            shieldOn = content.Load<Texture2D>("Shield");
            shieldTexture = content.Load<Texture2D>("ShiledBar");
            font = content.Load<SpriteFont>("SpriteFont1");
           
            fire = content.Load<SoundEffect>("ShotEffect");
           
            for (int i = 0; i < bulletArray.Length; i++)
            {
                bulletArray[i].LoadContent(content, "bullet");
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime,int screenWidth, int scrrenHeight)
        {
          
            switch (currentShip)
            {
                case ShipState.Ophelia:
                    OpheliaUpdate(gameTime, screenWidth, scrrenHeight, opheliaTexture);
                    break;
                case ShipState.Othello:
                    OthelloUpdate(gameTime, screenWidth, scrrenHeight, othelloTexture);
                    break;
                case ShipState.Oberon:
                    OberonUpdate(gameTime, screenWidth, scrrenHeight, oberonTexture);
                    break;
                default:
                    break;
            }
        }
       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch sp, GameTime time)
        {
            switch (currentShip)
            {
                case ShipState.Ophelia:
                    AlwaysDraw(sp, opheliaTexture, time);
                    break;
                case ShipState.Othello:
                    AlwaysDraw(sp, othelloTexture, time);
                    break;
                case ShipState.Oberon:
                    AlwaysDraw(sp, oberonTexture, time);
                    break;
                default:
                    break;
            }
            sp.DrawString(font, "You Curently Have: " + currentHold + " Gmes",new Vector2(500,30), Color.White);
        }
        #endregion

        #region Methods

        #region ShipUpdates
        private void OpheliaUpdate(GameTime gameTime, int width, int height, Texture2D opTexture)
        {
            if (alive)
            {
                
                ActivateShield();
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    bulletArray[i].Update(gameTime, this);
                }
                texture = opTexture;
                playerRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width / 5, texture.Height);

                for (int i = 0; i < bulletArray.Length; i++)
                {
                    if (bulletArray[i].Alive)
                    {
                        bulletArray[i].Boundary(width, height);
                    }
                }
                Move(gameTime, 80, 75);
                BoundryChecking(width, height, texture);
                
                fuelRectangle = new Rectangle(20, 20, (int)fuel, 20);
                shieldRect = new Rectangle(20, 60, (int)shieldAmount, 20);
            }
            else
            {
                Dead(gameTime);
            }
        }

        private void OthelloUpdate(GameTime gameTime, int width, int height, Texture2D otTexture)
        {
            if (alive)
            {
                ActivateShield();
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    bulletArray[i].Update(gameTime, this);
                }
                texture = otTexture;
                playerRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width / 5, texture.Height);
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    if (bulletArray[i].Alive)
                    {
                        bulletArray[i].Boundary(width, height);
                    }
                }

                Move(gameTime, 94, 80);
                BoundryChecking(width, height, texture);
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    bulletArray[i].Update(gameTime, this);
                }
                fuelRectangle = new Rectangle(20, 20, (int)fuel, 20);
            }
            else
            {
                Dead(gameTime);
            }
        }

        private void OberonUpdate(GameTime gameTime, int width, int height, Texture2D obTexture)
        {
            if (alive)
            {
                ActivateShield();
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    bulletArray[i].Update(gameTime, this);
                }
                texture = obTexture;
                playerRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width / 5, texture.Height);
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    if (bulletArray[i].Alive)
                    {
                        bulletArray[i].Boundary(width, height);
                    }
                }

                Move(gameTime, 80, 85);
                BoundryChecking(width, height, texture);
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    bulletArray[i].Update(gameTime, this);
                }
                fuelRectangle = new Rectangle(20, 20, (int)fuel, 20);
            }
            else
            {
                Dead(gameTime);
            }
        }
        #endregion

        #region DrawingMethod
        private void AlwaysDraw(SpriteBatch sp, Texture2D tempTexture, GameTime time)
        {
            texture = tempTexture;
            if (alive)
            {
                sp.Draw(texture, position, sourceRect, Color.White, turnRate, origin, 1f, SpriteEffects.None, 0);
                if(shield)
                {
                    sp.Draw(shieldOn, new Vector2(position.X - shieldOn.Width/2,position.Y-shieldOn.Height/2), Color.White);
                }
                sp.Draw(fuelTexture, fuelRectangle, Color.White);
                sp.Draw(shieldTexture, shieldRect, Color.White);
                for (int i = 0; i < bulletArray.Length; i++)
                {
                    bulletArray[i].Draw(sp);
                }
            }
           
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// Allows players ship to be moved by pressing the forward arrow key
        /// The ship will move forward depending where ths hip is pointing
        /// </summary>
        public void Move(GameTime gameTime,int frameWidth, int frameHeight)
        {
            Game.previousKeyBoardState = Game.aCurrentKeyboardState;
            Game.aCurrentKeyboardState = Keyboard.GetState();

            destinationRect = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
            position = velocity + position;
            origin = new Vector2(destinationRect.Width / 2, destinationRect.Height / 2);

            if (Game.aCurrentKeyboardState.IsKeyDown(Keys.Left))
            {
                turnRate -= addTurning; 
            }

            if (Game.aCurrentKeyboardState.IsKeyDown(Keys.Right))
            {
                turnRate += addTurning; 
            }

            //add thrust power when up key is pressed.
            if(Game.aCurrentKeyboardState.IsKeyDown(Keys.Up) && fuel >=0)
            {
               
                thrust += speedingUp;
                Animate(gameTime, frameWidth, frameHeight);
                if (thrust >= maxSpeed)   thrust = maxSpeed;
                TakeFuel(gameTime);
                velocity.X = (float)Math.Cos(turnRate) * thrust; //veclocity follows the same path as the turnRtae and multiplies it by the thrust power
                velocity.Y = (float)Math.Sin(turnRate) * thrust; //by using turnRate, the ship will always fly where its pointng(works with the turning above) 
            }
            //when the up key is not pressed and the position is not 0
            // then friction is applied
            else if(position != Vector2.Zero)
            {
                frames = 0;
                Vector2 i = velocity;
                velocity = i - friction * i;
                sourceRect = new Rectangle(0, 0, frameWidth, frameHeight);
                if (thrust >= 0)   thrust -= 0.001f;
            }
            Shoot(gameTime);
        }

        private void TakeFuel(GameTime time)
        {
            fuel-=decFuel;
        }

        /// <summary>
        /// There are 5 Images on a sprite.(imitates movement)
        /// sets each image of the big sprite to frames
        /// starts at 0 frames and ends at 4th frame
        /// source rectangle is set depending on THE FRAME
        /// </summary>
        /// <param name="gameTime"></param>
        private void Animate(GameTime gameTime, int width, int height)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames > 4)
                {
                    frames = 0;
                }
                else
                {
                    if (frames == 4) { }
                    else
                    {
                        frames++;
                    }
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(width * frames, 0, width, height);
        }

        /// <summary>
        /// Allows the player ship to shoot in the direction ifs facing
        /// </summary>
        public void Shoot(GameTime gameTime)
        {
            
            if (Game.aCurrentKeyboardState.IsKeyDown(Keys.Space) && Game.previousKeyBoardState.IsKeyUp(Keys.Space))
            {
                fire.Play();
                for (int i = 0; i < MaxBullet; i++)
                {
                    if (!(bulletArray[i].Alive))
                    {
                        
                        bulletArray[i].Fire(gameTime);
                        break;
                    }
                }
            }
        }

        public void Dead(GameTime time)
        {
           
            deadElapsed += (float)time.ElapsedGameTime.TotalSeconds;
            if (deadElapsed >=3)
            {
                Game.gameState = Game.GameMode.GameOver;
            }
        }
      


        /// <summary>
        /// activates shield by pressing dwn arrow key
        /// </summary>
        private void ActivateShield()
        {
           if(Game.aCurrentKeyboardState.IsKeyDown(Keys.Down) && shieldAmount > 0)
           {
               shield = true;
               shieldAmount -= decShiled;
           }
           else
           {
               shield = false;
           }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        /// <summary>
        /// by passing an int which represents the items storage capacity
        /// it'll add that item to the current hold
        /// eg.gem takes 2 hold capacity
        /// </summary>
        /// <param name="item"></param>
        public void AddToHold(int item)
        {
            if (currentHold < holdCapacity)
            {
                currentHold += item;
            }
            if (currentHold > holdCapacity)
            {
                currentHold = holdCapacity;
            }
        }

        public void RemoveFromHold()
        {

        }

        public void BoundryChecking(int viewportWidth , int viewportHeight, Texture2D texture)
        {
            if (position.X + (texture.Width / 5) <= 0)
            {
                position.X = viewportWidth;
            }
            if (position.X - 40 >= (viewportWidth)) 
            {
                position.X = 0 - texture.Width / 5; 
            }
            if (position.Y + texture.Height <= 0)     
            {
                position.Y = viewportHeight - 1;
            }
            if (position.Y -40 >= viewportHeight )      
            {
                position.Y = 0 - texture.Height;
            }
        }

        public void AsteroidCollision(Asteroids asteroid, Texture2D shipTexture, ref float elap)
        {
            if (alive && !shield && asteroid.Alive)
            {
                int width = shipTexture.Width /5;
                int height = shipTexture.Height;
                Vector2 asteroidPos = asteroid.Position;
               
                if (position.X + width < asteroidPos.X
                ||
                position.Y + height < asteroidPos.Y
                ||
                position.X > asteroidPos.X + asteroid.Texture.Width
                ||
                position.Y > asteroidPos.Y + asteroid.Texture.Height)
                {

                }
                else
                {
                    alive = false;
                    elap = 0;
                }
            }
        }


        public void CollisionEnemy(Enemy enemy ,ref float deadElap)
        {
            if(enemy.EnemyAlive && !shield)
            {
                int width = enemy.Texture.Width;
                int height = enemy.Texture.Height;
                Vector2 enemyPos = enemy.Position;

                if (position.X + width < enemyPos.X
                ||
                position.Y + height < enemyPos.Y
                ||
                position.X > enemyPos.X + enemy.Texture.Width
                ||
                position.Y > enemyPos.Y + enemy.Texture.Height)
                {

                }
                else
                {
                    alive = false;
                    deadElap = 0;
                    enemy.EnemyAlive = false;
                }
            }
           
        }
      
        #endregion

        #endregion

        #region Properties
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

        public float Fuel
        {
            get { return fuel; }
        }

        public Rectangle PlayerRect
        {
            get { return playerRectangle; }
            set { playerRectangle = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }


        public bool Shield
        {
            get { return shield; }
            set { shield = value; }
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
       
        #endregion
    }
}
