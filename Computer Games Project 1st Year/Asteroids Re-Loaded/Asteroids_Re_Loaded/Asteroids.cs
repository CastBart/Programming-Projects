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
 * Total Time Spent on this class
 * ----17 hours----
 */ 
namespace Asteroids_Re_Loaded
{
    class Asteroids
    {

        #region Veriables
        bool alive;
        Vector2 position = new Vector2(0,400), origin;
        Texture2D texture, bigTexture, medTexture, smallTexture;
        Rectangle asteroidRectangle;
        float speedX, speedY;
        float turnRate;
        public static Random rnd = new Random();
       
        int gemChance;
        bool bigAsteroid;
        bool medAsteroid;
        bool smallAsteroid;

        public enum AsteroidType { Small, Medium, Big };
        public  AsteroidType currentAsteroid;

        #endregion

        #region Constructor

        public Asteroids()
        {
            alive = false;
        }
        public Asteroids(int height, int width)
        {
            Spawn(height, width);
            speedX = rnd.Next(-4,5);
            speedY = rnd.Next(-4,5);
            gemChance = rnd.Next(1, 101);
           // currentAsteroid = AsteroidType.Big;
            alive = false;
        }

        #endregion

        #region Load, Update, Draw
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("whiteAsteroid1");
            bigTexture = content.Load<Texture2D>("whiteAsteroid1");
            medTexture = content.Load<Texture2D>("whiteAsteroid2");
            smallTexture = content.Load<Texture2D>("whiteAsteroid3");

           
            asteroidRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(int width, int height)
        {
           
            if (smallAsteroid)
            {
                currentAsteroid = AsteroidType.Small;
            }
            if (medAsteroid)
            {
                  currentAsteroid = AsteroidType.Medium;
            }
            if (bigAsteroid)
            {
                currentAsteroid = AsteroidType.Big;
            }
            if (alive)
            {
                turnRate += 0.05f;
                position += new Vector2(speedX, speedY);
                BoundryChecking(width, height, texture);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch sp, int width, int height)
        {

            switch (currentAsteroid)
            {
                case AsteroidType.Small:
                    if (alive)
                    {
                        texture = smallTexture;
                        origin.X = texture.Width / 2;
                        origin.Y = texture.Height / 2;
                        sp.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height),
                            new Rectangle(0, 0, width-55, height-52), Color.White, turnRate, origin, SpriteEffects.None, 0);
                    }
                    break;
                case AsteroidType.Medium:
                    if (alive)
                    {
                        texture = medTexture;
                        origin.X = texture.Width / 2;
                        origin.Y = texture.Height / 2;
                        sp.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height),
                            new Rectangle(0, 0, width-25, height-23), Color.White, turnRate, origin, SpriteEffects.None, 0);
                    }
                    break;
                case AsteroidType.Big:
                    if (alive)
                    {
                        texture = bigTexture;
                        origin.X = texture.Width / 2;
                        origin.Y = texture.Height / 2;
                        sp.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height),
                            new Rectangle(0, 0, width, height), Color.White, turnRate, origin, SpriteEffects.None, 0);
                    }
                    break;
                default:
                    break;
            }
           
        }

        public void MakeAsteroid(Asteroids.AsteroidType astType, Vector2 pos, ContentManager content)
        {
            switch (astType)
            {
                case AsteroidType.Small:

                    LoadContent(content);
                    alive = true;
                    smallAsteroid = true;
                    Spawn(pos);
                    break;
                case AsteroidType.Medium:

                    LoadContent(content);
                    alive = true;
                    medAsteroid = true;
                    Spawn(pos);
                    break;
                case AsteroidType.Big:

                    LoadContent(content);
                    alive = true;
                    bigAsteroid = true;
                    Spawn(800,480);

                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Methods
        /// <summary>
        ///  at the beginning of each level it spawns asteroids randomlly on the screen
        /// </summary>
        /// <param name="viewPortHeight"></param>
        /// <param name="viewPortWidth"></param>
        public void Spawn(int viewPortHeight, int viewPortWidth)
        {
            speedX = rnd.Next(-4, 5);
            speedY = rnd.Next(-4, 5);
            int side = rnd.Next(1, 5);
            if (side == 1)
            {
                position = new Vector2(0, rnd.Next(0, viewPortHeight));
            }
            if (side == 2)
            {
                position = new Vector2(rnd.Next(0, viewPortWidth), 0);
            }
            if (side == 3)
            {
                position = new Vector2(viewPortWidth , rnd.Next(0, viewPortHeight));
            }
            if (side == 4)
            {
                position = new Vector2(rnd.Next(0, viewPortWidth), viewPortHeight );
            }
        }

        /// <summary>
        /// spawn a new asteroid at the position of the previous asteroid
        /// </summary>
        /// <param name="astPosition"></param>
        public void Spawn(Vector2 astPosition)
        {
            position = astPosition;
            speedX = rnd.Next(-4, 5);
            speedY = rnd.Next(-4, 5);
        }

        public void BoundryChecking(int viewportWidth, int viewportHeight, Texture2D texture)
        {
            if (position.X + (texture.Width) <= 0)
            {
                position.X = viewportWidth;
            }
            if (position.X - (texture.Width/2)>= (viewportWidth))
            {
                position.X = 0 - texture.Width;
            }
            if (position.Y + texture.Height <= 0)
            {
                position.Y = viewportHeight - 1;
            }
            if (position.Y - (texture.Height/2)  >= viewportHeight)
            {
                position.Y = 0 - texture.Height;
            }
        }

        #endregion

        #region Properties
        public Rectangle AsteroidRect
        {
            get { return asteroidRectangle; }
            set { asteroidRectangle = value; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 Origin
        {
            get { return origin; }
        }
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
        public bool BigAlive
        {
            get { return bigAsteroid; }
            set { bigAsteroid = value; }
        }
        public bool SmallAlive
        {
            get { return smallAsteroid; }
            set { smallAsteroid = value; }
        }
        public bool MedAlive
        {
            get { return medAsteroid; }
            set { medAsteroid = value; }
        }
        public Texture2D Texture
        {
            get { return texture; }
        }

        public int GemChance
        {
            get { return gemChance; }
            set { gemChance = value; }
        }

        #endregion

    }
       
}
