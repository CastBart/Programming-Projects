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
 * total spent time
 * ---5 hours---
 */ 
namespace Asteroids_Re_Loaded
{
    class Gem
    {

        #region Variables
        bool alive;
        float rotation;
        float worthMoney;
        static ContentManager content;

        Vector2 position, orignin;
        Texture2D texture;
        

        public enum TypeOfGem { None, Green, Red, Orange, Silver, Purple };
        public static TypeOfGem gemType = TypeOfGem.None;
        #endregion

        #region Constructor
        public Gem()
        {

        }

      
        #endregion

        #region Load, Update, Draw
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager content, string name)
        {
            texture = content.Load<Texture2D>(name);
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update()
        {
            if (alive)
            {
                rotation += 0.02f;    
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
                sp.Draw(texture, position, new Rectangle(0,0,32,32), Color.White, rotation, orignin,1, SpriteEffects.None,0);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// giving the gem values 
        /// to be alive and the position to spawn at
        /// after an asteroid has been destroyed
        /// </summary>
        /// <param name="gemType"></param>
        /// <param name="asteroidPos"></param>
        public void MakeGem(Asteroids asteroid, int randomPercentage, TypeOfGem gemType, int chance)
        {
           
            switch (gemType)
            {
                    
                case TypeOfGem.None:
                    break;
                case TypeOfGem.Green:

                    worthMoney = 30;
                    texture = content.Load<Texture2D>("GreenGem");
                    break;
                case TypeOfGem.Red:
                    worthMoney = 60;
                    texture = content.Load<Texture2D>("RedGem");
                    break;
                case TypeOfGem.Orange:
                    worthMoney = 100;
                    texture = content.Load<Texture2D>("OrangeGem");
                    break;
                case TypeOfGem.Silver:
                    worthMoney = 200;
                    texture = content.Load<Texture2D>("SilverGem");
                    break;
                case TypeOfGem.Purple:
                    worthMoney = 500;
                    texture = content.Load<Texture2D>("PurpleGem");
                    break;
                default:
                    break;
            }
            orignin.X = texture.Width / 2;
            orignin.Y = texture.Height / 2;
            if (randomPercentage < chance)
            {
                alive = true;
                position = asteroid.Position;
            }
           
           // orignin = asteroid.Origin;
        }

        public void PlayerCollision(Player player)
        {
            if (alive)
            {
                int width = player.Texture.Width/5;
                int height = player.Texture.Height;
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
                    player.AddToHold(1);
                }
            }
        }

        #endregion

        #region Properties
        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }
        public  Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
        #endregion
    }

}
