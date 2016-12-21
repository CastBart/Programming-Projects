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
namespace Asteroids_Re_Loaded
{
    class MainMenu
    {

        #region Properties Constants and Variables

        /// <summary>
        /// 
        /// </summary>
        private Texture2D mainMenuTex;
        private Texture2D playHoveredTex;
        private Texture2D creditsHoveredTex;
        private Texture2D optionsHoveredTex;
        private Texture2D exitHoveredTex;
        Rectangle playButton = new Rectangle(595, 35, 146, 91);
        Rectangle creditsButton = new Rectangle(595, 138, 146, 91);
        Rectangle optionsButton = new Rectangle(595, 242, 146, 91);
        Rectangle exitButton = new Rectangle(595, 350, 146, 91);

        #endregion



        public void LoadContent(ContentManager theContentManager)
        {
            mainMenuTex = theContentManager.Load<Texture2D>("Start Screen");
            playHoveredTex = theContentManager.Load<Texture2D>("Start Screen Play");
            creditsHoveredTex = theContentManager.Load<Texture2D>("Start ScreenCredits");
            optionsHoveredTex = theContentManager.Load<Texture2D>("Start ScreenOptions");
            exitHoveredTex = theContentManager.Load<Texture2D>("Start ScreenExit");

        }

        public void Update(GameTime gameTime, Game theGame)
        {

            //Get the mouse's movements
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);


            //Check if the mouse position is inside the rectangle of the buttons
            if (playButton.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                   
                    Game.gameState = Game.GameMode.OuterMap;
                   
                }
            }

            if (creditsButton.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.gameState = Game.GameMode.Credits;
                }
            }
            if (optionsButton.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Game.gameState = Game.GameMode.Options;
                }
            }
            if (exitButton.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    theGame.Exit();
                }
            }
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(mainMenuTex, new Vector2(0, 0), Color.White);
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            Rectangle playDest = new Rectangle(580, 19, 179, 119);
            Rectangle credDest = new Rectangle(581, 126, 176, 116);
            Rectangle optiDest = new Rectangle(581, 232, 177, 115);
            Rectangle exitDest = new Rectangle(577, 335, 187, 122);

            if (playButton.Contains(mousePosition))
            {
                theSpriteBatch.Draw(playHoveredTex, playDest, Color.White);
            }
            if (creditsButton.Contains(mousePosition))
            {
                theSpriteBatch.Draw(creditsHoveredTex, credDest, Color.White);
            }
            if (optionsButton.Contains(mousePosition))
            {
                theSpriteBatch.Draw(optionsHoveredTex, optiDest, Color.White);
            }
            if (exitButton.Contains(mousePosition))
            {
                theSpriteBatch.Draw(exitHoveredTex, exitDest, Color.White);
            }
        }

    }
}
