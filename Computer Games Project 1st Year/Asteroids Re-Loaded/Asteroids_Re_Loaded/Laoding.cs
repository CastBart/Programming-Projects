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
using System.Threading;

namespace Asteroids_Re_Loaded
{
    class Loading
    {
        private Texture2D loadingTex;
        private Texture2D rotatingStarTex;
        public static SpriteFont theFont;
        private Rectangle starBox = new Rectangle(700, 400, 119, 116);
        private Vector2 loadingText = new Vector2(500, 380);
        float rotation;
        float timer = 0f;
        float elapsed;

        enum CurentState { Loading, Continue };
        CurentState currentState = CurentState.Loading;

        public void LoadContent(ContentManager theContentManager)
        {
            loadingTex = theContentManager.Load<Texture2D>("Loading");
            rotatingStarTex = theContentManager.Load<Texture2D>("starRotate");
            theFont = theContentManager.Load<SpriteFont>("SpriteFont1");
        }

        public void Update(GameTime gameTime)
        {

            Game.previousKeyBoardState = Game.aCurrentKeyboardState;
            Game.aCurrentKeyboardState = Keyboard.GetState();
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(elapsed <=5)
            {
                
            }
            else
            {
                currentState = CurentState.Continue;
            }

            if(currentState == CurentState.Continue )
            {
                if(Game.aCurrentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    Game.gameState = Game.GameMode.LicenceScreen;
                }
            }
            if (timer >= 150f)
            {
                timer = 0f;
            }
            else
                timer++;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            switch (currentState)
            {
                case CurentState.Loading:
                     rotation = MathHelper.WrapAngle(rotation + 0.05f);
                     theSpriteBatch.Draw(loadingTex, new Vector2(0, 0), Color.White);
                     theSpriteBatch.Draw(rotatingStarTex, starBox, null, Color.White, rotation, new Vector2(rotatingStarTex.Width / 2, rotatingStarTex.Height / 2), SpriteEffects.None, 0.0f);
                     if (timer >= 0f)
                     {
                         theSpriteBatch.DrawString(theFont, "Loading.  ", loadingText, Color.White);
                     }
                     if (timer >= 50f)
                     {
                         theSpriteBatch.DrawString(theFont, "        . ", loadingText, Color.White);
                     }
                     if (timer >= 100f)
                     {
                         theSpriteBatch.DrawString(theFont, "         .", loadingText, Color.White);
                     }
                    break;
                case CurentState.Continue:
                    rotation = MathHelper.WrapAngle(rotation - 0.05f);
                    theSpriteBatch.Draw(loadingTex, new Vector2(0, 0), Color.White);
                    theSpriteBatch.DrawString(theFont, "Press Enter To Continue ", new Vector2(300,380), Color.White);
                    theSpriteBatch.Draw(rotatingStarTex, starBox, null, Color.White, rotation, new Vector2(rotatingStarTex.Width / 2, rotatingStarTex.Height / 2), SpriteEffects.None, 0.0f);
                    break;
                default:
                    break;
            }
           


        }

    }
}