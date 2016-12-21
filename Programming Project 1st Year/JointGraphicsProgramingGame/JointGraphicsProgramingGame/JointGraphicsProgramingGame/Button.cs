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
    class Button
    {
        Vector2 position;
        Texture2D texture;
        Rectangle rectangle;
        bool clicked;

       /// <summary>
       /// sets a different texture 
       /// </summary>
       /// <param name="newTexture"></param>
         public Button(Texture2D newTexture)
         {
             texture = newTexture;
         }
        public void Initialize()
        {
            position = new Vector2(600, 140);
        }
        /// <summary>
        /// creating ta small rectangle where the mouse location is 
        /// a clicke event set to true if the rectangle of the opuse and the texture 
        /// are interestiong and the mosue is clicked
        /// </summary>
        /// <param name="mouse"></param>
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y,(int)texture.Width , (int)texture.Height);
            Rectangle mouseRect = new Rectangle(mouse.X,mouse.Y,1,1);
            if(mouseRect.Intersects(rectangle))
            {
                if(mouse.LeftButton == ButtonState.Pressed)
                {
                    clicked = true;
                }
            }
            else
            {
                clicked = false;
            }
        }
        /// <summary>
        /// draws the button
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,rectangle, Color.White);
        }
        public bool MouseClicked
        {
            get
            {
                return clicked;
            }
        }
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
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
    }
}
