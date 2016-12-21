using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

/* Worked on by Bartosz Zych
 *  Total spent time 
 *  ---2 Hour min---
 */
namespace Asteroids_Re_Loaded
{
    class Level
    {
        
        bool active;
        bool complete;
        int gemChance;
        int numOfEnemy;

        public Level(int newGem, int newEnemy, bool newActive)
        {
            gemChance = newGem;
            numOfEnemy = newEnemy;
            active = newActive;
        }

        public void Initialize()
        {



        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {

        }



        #region Properties

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public bool Complete
        {
            get { return complete; }
            set { complete = value; }
        }

        public int GemChance
        {
            get { return gemChance; }
            set { gemChance = value; }
        }
        public int NumOfEnemy
        {
            get { return numOfEnemy; }
            set { numOfEnemy = value; }
        }

        #endregion

    }

}
