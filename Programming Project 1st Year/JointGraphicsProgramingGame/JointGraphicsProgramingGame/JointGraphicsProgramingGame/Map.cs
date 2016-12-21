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
    class Map
    {
        const int viewportHeight = 480;
        const int viewportWidth = 800;
        Rectangle position = new Rectangle(0, 0, viewportWidth, viewportHeight);

      
        /// <summary>
        /// draws a map for each level
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        /// <param name="map"></param>
        public void Draw(SpriteBatch theSpriteBatch, Texture2D map)
        {
            theSpriteBatch.Draw(map, position, Color.White);
        }

    }
}
