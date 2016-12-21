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
 * Total time spent
 * ---4 hours---
 */
namespace Asteroids_Re_Loaded
{
    class Bullet
    {
        #region Variables
        bool alive = false;
        Vector2 position, velocity;
        Texture2D texture;
        public float speed = 2;
        float turnRate, elapsed;
        int fireSnapShot = 0;
        Rectangle bulletRect = new Rectangle();
        SoundEffect collide;
        #endregion

        #region Load, Update, Draw
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager theContentManager, string Image)
        {
            texture = theContentManager.Load<Texture2D>(Image);
            collide = theContentManager.Load<SoundEffect>("Shot_hits_asteroid");
            bulletRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, Player player)
        {
           
            elapsed += 1;
            if (elapsed > fireSnapShot)
            {
                alive = false;
            }

            if (!alive) 
            {
                turnRate = player.TurnRate;
                position = player.Position;
            }
            else
            {
                Travel();
            }
        }
        public void Update(GameTime gameTime, Enemy enemy)
        {

            elapsed += 1;
            if (elapsed > fireSnapShot)
            {
                alive = false;
            }

            if (!alive)
            {
                turnRate = enemy.TurnRate;
                position = enemy.Position;
            }
            else
            {
                Travel();
            }
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch sp)
        {
            
            if (alive)
            {
                sp.Draw(texture, position, null, Color.White, turnRate, new Vector2(), 1f, SpriteEffects.None, 0);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Comutes the collision between an asteroid and bullet
        /// </summary>
        /// <param name="asteroid"></param>
        /// <param name="gem"></param>
        /// <param name="chanceForGem"></param>
        public void BulletAsteroidCollision(Asteroids asteroid,Asteroids nextAsteroids, Gem gem, int chanceForGem,
                                             Gem.TypeOfGem type, ref int numOfAsteroids, ContentManager content)
        {

            int chance = Asteroids.rnd.Next(1, 101);
            switch (asteroid.currentAsteroid)
            {
                case Asteroids.AsteroidType.Small:
                    #region SmallCollision
                    if (asteroid.Alive && alive)
                    {
                        int width = texture.Width;
                        int height = texture.Height;
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
                             collide.Play();
                             alive = false;
                             asteroid.Alive = false;
                             gem.MakeGem(asteroid, Asteroids.rnd.Next(1, 101), type, chanceForGem);
                        }
                    }
                    #endregion
                    break;
                case Asteroids.AsteroidType.Medium:
                    #region MediumCollision
                    if (asteroid.Alive && alive)
                    {
                        int width = texture.Width;
                        int height = texture.Height;
                        Vector2 asteroidPos = asteroid.Position;

                        if (position.X + width < asteroidPos.X
                        ||
                        position.Y + height < asteroidPos.Y
                        ||
                        position.X > asteroidPos.X + asteroid.Texture.Width-25
                        ||
                        position.Y > asteroidPos.Y + asteroid.Texture.Height-23)
                        {
                        }
                        else
                        {
                            collide.Play();
                            alive = false;
                            nextAsteroids.MakeAsteroid(Asteroids.AsteroidType.Small, asteroid.Position, content);
                            asteroid.currentAsteroid = Asteroids.AsteroidType.Small;
                            numOfAsteroids++;
                        }
                    }
                    #endregion
                    break;
                case Asteroids.AsteroidType.Big:
                    #region BigCollision
                    if (asteroid.Alive && alive)
                    {
                        int width =24;
                        int height = 10;
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
                            collide.Play();
                            alive = false;
                            nextAsteroids.MakeAsteroid(Asteroids.AsteroidType.Medium , asteroid.Position, content);
                            asteroid.currentAsteroid = Asteroids.AsteroidType.Medium;
                            numOfAsteroids++;
                        }
                    }
                    #endregion
                    break;
                default:
                    break;
            }
           
        }

       


        public void CollisionWithShips(Enemy enemy)
        {
            if (enemy.EnemyAlive)
            {
                int width = texture.Width;
                int height = texture.Height;
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
                    enemy.EnemyAlive = false;
                }
            }
        }

        public void CollisionWithShips(Player player, ref float deadElap)
        {
            if (player.Alive && !player.Shield)
            {
                int width = texture.Width;
                int height = texture.Height;
                Vector2 playerPos = player.Position;

                if (position.X + width < playerPos.X
                ||
                position.Y + height < playerPos.Y
                ||
                position.X > playerPos.X + player.Texture.Width
                ||
                position.Y > playerPos.Y + player.Texture.Height)
                {
                }
                else
                {
                    alive = false;
                    player.Alive = false;
                    deadElap = 0;

                }
            }
        }
        /// <summary>
        /// setting alive to true 
        /// and giving a snapshot(time of the bullet to be alive)
        /// so that the bullet will travel 
        /// </summary>
        /// <param name="time"></param>
        public void Fire(GameTime time)
        {
            alive = true;
            fireSnapShot = time.TotalGameTime.Seconds + 200;
            elapsed = time.TotalGameTime.Seconds;
        }

        /// <summary>
        /// movement of the bullet
        /// </summary>
        public void Travel()
        {
            if (alive)
            {
                position = velocity + position;
                velocity.X = (float)Math.Cos(turnRate) * speed;
                velocity.Y = (float)Math.Sin(turnRate) * speed;
            }
        }

        /// <summary>
        /// boundary collisinions
        /// </summary>
        /// <param name="viewportWidth"></param>
        /// <param name="viewportHeight"></param>
        public void Boundary(int viewportWidth, int viewportHeight)
        {
            if (position.X + texture.Width <= 0)
            {
                position.X = viewportWidth-1;
            }
            if (position.X  >= (viewportWidth))
            {
                position.X = 0 - texture.Width;
            }
            if (position.Y + texture.Height <= 0)
            {
                position.Y = viewportHeight - 1;
            }
            if (position.Y  >= viewportHeight)
            {
                position.Y = 0 - texture.Height;
            }
        }


        #endregion

        #region Properties
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

        public float TurnRate
        {
            get { return turnRate; }
            set { turnRate = value; }
        }
        #endregion
    }

}
