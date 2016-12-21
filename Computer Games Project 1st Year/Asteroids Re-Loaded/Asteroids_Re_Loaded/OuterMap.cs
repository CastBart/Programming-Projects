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
    class OuterMap
    {


        #region Properties Constants and Variables

        private Texture2D backgroundTex;
        private Texture2D pathwayTex;
        private Texture2D levelstarTex;
        private Texture2D highlightTex;
        private Texture2D menuButtonTex;
        private SpriteFont theFont;
        private string lvlNo = "";
        private string gemChanceNo = "";
        private string astNo = "";
        private string pirateNo = ""; 
        private string messageString;

        Rectangle levelOne = new Rectangle(54, 159, 62, 66);
        Rectangle levelTwo = new Rectangle(62, 3, 62, 66);
        Rectangle levelThree = new Rectangle(190, 94, 62, 66);
        Rectangle levelFour = new Rectangle(337, 239, 62, 66);
        Rectangle levelFive = new Rectangle(349, 387, 62, 66);
        Rectangle levelSix = new Rectangle(483, 302, 62, 66);
        Rectangle levelSeven = new Rectangle(591, 155, 62, 66);
        Rectangle levelEight = new Rectangle(603, 7, 62, 66);
        Rectangle levelNine = new Rectangle(737, 92, 62, 66);
        Rectangle levelStation = new Rectangle(42, 342, 62, 66);
        Rectangle menuBox = new Rectangle(150, 400, 129, 67);

        Vector2 messageBox = new Vector2(300, 50);

        Vector2 texturePosition = new Vector2(0, 0);

        Level[] levels = new Level[9];
        

        #endregion

        public OuterMap(Level[] newlevels)
        {
            levels = newlevels;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            backgroundTex = theContentManager.Load<Texture2D>("SampleScreen");
            pathwayTex = theContentManager.Load<Texture2D>("Pathways");
            levelstarTex = theContentManager.Load<Texture2D>("Levels");
            highlightTex = theContentManager.Load<Texture2D>("Highlight");
            menuButtonTex = theContentManager.Load<Texture2D>("Box");
            theFont = theContentManager.Load<SpriteFont>("SpriteFont1");
        }

        public void Update(GameTime gameTime, Game theGame )
        {
            
            //Get the mouse's movements
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            if (menuBox.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Game.gameState = Game.GameMode.MainMenu;
            }

            if (levelOne.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[0].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                
               
                Game.currentLevel = Game.CurrentLevel.Level1;
                theGame.LoadLevel(2, levels[0].NumOfEnemy);
            }
            if (levelTwo.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[1].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                Game.currentLevel = Game.CurrentLevel.Level2;
                theGame.LoadLevel(3, levels[1].NumOfEnemy);
            }
            if (levelThree.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[2].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                Game.currentLevel = Game.CurrentLevel.Level3;
                theGame.LoadLevel(5, levels[2].NumOfEnemy);
            }
            if (levelFour.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[3].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                Game.currentLevel = Game.CurrentLevel.Level4;
                theGame.LoadLevel(6, levels[3].NumOfEnemy);
            }
            if (levelFive.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[4].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                Game.currentLevel = Game.CurrentLevel.Level5;
                theGame.LoadLevel(7, levels[4].NumOfEnemy);
            }
            if (levelSix.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[5].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                Game.currentLevel = Game.CurrentLevel.Level6;
                theGame.LoadLevel(8, levels[5].NumOfEnemy);
            }
            if (levelSeven.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[6].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                Game.currentLevel = Game.CurrentLevel.Level7;
                theGame.LoadLevel(8, levels[6].NumOfEnemy);
            }
            if (levelEight.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[7].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                Game.currentLevel = Game.CurrentLevel.Level8;
                theGame.LoadLevel(9, levels[7].NumOfEnemy);
            }
            if (levelNine.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && levels[8].Active)
            {
                Game.gameState = Game.GameMode.ToMission;
                Game.videoPLayer.Play(Game.toVideo);
                Game.currentLevel = Game.CurrentLevel.Level9;
                theGame.LoadLevel(10, levels[8].NumOfEnemy);
            }
            if (levelStation.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Game.gameState = Game.GameMode.Hangar;
            }
  
       }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(backgroundTex, new Vector2(0, 0), Color.White);
            theSpriteBatch.Draw(pathwayTex, new Vector2(0, 0), Color.White);

            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);



            Vector2 messagePosition1 = new Vector2(300, 20);
            Vector2 messagePosition2 = new Vector2(300, 60);
            Vector2 messagePosition3 = new Vector2(300, 100);
            Vector2 messagePosition4 = new Vector2(300, 140);

            theSpriteBatch.DrawString(theFont, "Level: " + lvlNo, messagePosition1, Color.White);
            theSpriteBatch.DrawString(theFont, "Asteroids: " + astNo, messagePosition2, Color.White);
            theSpriteBatch.DrawString(theFont, "Gem Chance: " + gemChanceNo, messagePosition3, Color.White);
            theSpriteBatch.DrawString(theFont, "Pirates: " + pirateNo, messagePosition4, Color.White);

            if (levelOne.Contains(mousePosition) && levels[0].Active)
            {
                lvlNo = "1";
                astNo = "2";
                gemChanceNo = "Green, 50%";
                pirateNo = "0";
                theSpriteBatch.Draw(highlightTex, levelOne, Color.Yellow);

            }

            if (levelTwo.Contains(mousePosition) && levels[1].Active)
            {
                lvlNo = "2";
                astNo = "3";
                gemChanceNo = "Green, 60%";
                pirateNo = "0";
                theSpriteBatch.Draw(highlightTex, levelTwo, Color.Yellow);
            }

            if (levelThree.Contains(mousePosition) && levels[2].Active)
            {
                lvlNo = "3";
                astNo = "5";
                gemChanceNo = "Red, 40%";
                pirateNo = "1";
                theSpriteBatch.Draw(highlightTex, levelThree, Color.Yellow);
            }

            if (levelFour.Contains(mousePosition) && levels[3].Active)
            {
                lvlNo = "4";
                astNo = "6";
                gemChanceNo = "Red, 50%";
                pirateNo = "2";
                theSpriteBatch.Draw(highlightTex, levelFour, Color.Cyan);
            }

            if (levelFive.Contains(mousePosition) && levels[4].Active)
            {
                lvlNo = "5";
                astNo = "7";
                gemChanceNo = "Orange, 30%";
                pirateNo = "2";
                theSpriteBatch.Draw(highlightTex, levelFive, Color.Cyan);
            }

            if (levelSix.Contains(mousePosition) && levels[5].Active)
            {
                lvlNo = "6";
                astNo = "8";
                gemChanceNo = "Orange 60%";
                pirateNo = "3";
                theSpriteBatch.Draw(highlightTex, levelSix, Color.Cyan);
            }

            if (levelSeven.Contains(mousePosition) && levels[6].Active)
            {
                lvlNo = "7";
                astNo = "8";
                gemChanceNo = "Purple, 30%";
                pirateNo = "3";
                theSpriteBatch.Draw(highlightTex, levelSeven, Color.Magenta);
            }

            if (levelEight.Contains(mousePosition) && levels[7].Active)
            {
                lvlNo = "8";
                astNo = "9";
                gemChanceNo = "Purple, 50%";
                pirateNo = "4";
                theSpriteBatch.Draw(highlightTex, levelEight, Color.Magenta);
            }

            if (levelNine.Contains(mousePosition) && levels[8].Active)
            {
                lvlNo = "9";
                astNo = "10";
                gemChanceNo = "Silver, 30%";
                pirateNo = "5";
                theSpriteBatch.Draw(highlightTex, levelNine, Color.Magenta);
            }

            if (levelStation.Contains(mousePosition))
            {
                lvlNo = "Station";
                astNo = "-";
                gemChanceNo = "-";
                pirateNo = "-";
                theSpriteBatch.Draw(highlightTex, levelStation, Color.Yellow);
            }

            theSpriteBatch.Draw(levelstarTex, new Vector2(0, 0), Color.White);
            theSpriteBatch.Draw(menuButtonTex, menuBox, Color.White);
        }
    }
}
