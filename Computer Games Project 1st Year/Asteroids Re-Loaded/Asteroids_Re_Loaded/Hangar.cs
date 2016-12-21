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
    class Hangar
    {
        #region Properties Constants and Variables

        private Texture2D backgroundTex;
        private Texture2D backTex;
        private Texture2D opheliaTex;
        private Texture2D othelloTex;
        private Texture2D oberonTex;
        private Texture2D upgrade1Tex;
        private Texture2D upgrade2Tex;
        private Texture2D engineTex;
        private Texture2D holdTex;
        private Texture2D laserPowTex;
        private Texture2D laserShiTex;
        
       
        private Texture2D thrustTex;
        private Texture2D fuelTex;
        private Texture2D upgradeShipTex;
        private Texture2D sellTex;

        private enum Ships
        {
            Ophelia,
            Othello,
            Oberon
        }

        private Ships theCurrentShip;

      
        private bool engineClicked = false;
        private bool fuelTankClicked = false;
       
        private bool laserPowerClicked = false;
      
        private bool holdClicked = false;
        private bool sideThrusterClicked = false;
        private bool partSelected = false;
        private bool laserShieldClicked = false;


        Rectangle shipPos = new Rectangle(441, 0, 358, 358);
        Rectangle enginePos = new Rectangle(151, 12, 129, 67);
        Rectangle holdPos = new Rectangle(151, 89, 129, 67);

        Rectangle sideThrustPos = new Rectangle(12, 167, 129, 67);
        Rectangle laserChaPos = new Rectangle(12, 89, 129, 67);
        Rectangle laserPowPos = new Rectangle(12, 89, 129, 67);

        Rectangle laserShiPos = new Rectangle(151, 167, 129, 67);
        Rectangle fuelTankPos = new Rectangle(12, 12, 129, 67);
        Rectangle armorPos = new Rectangle(12, 12, 129, 67);

        Rectangle backPos = new Rectangle(290, 370, 129, 67);
        Rectangle sellPos = new Rectangle(12, 370, 191, 100);

        Rectangle upgradeShipPos = new Rectangle(492, 332, 258, 134);
        Rectangle upgrade1 = new Rectangle(12, 12, 193, 100);
        Rectangle upgrade2 = new Rectangle(250, 12, 193, 100);

        Vector2 texturePosition = new Vector2(0, 0);
        Player player;

         MouseState prevMouseState;
         MouseState mouseState;
        #endregion

         public Hangar(Player newPLayer)
         {
             player = newPLayer;
         }

        public void LoadContent(ContentManager theContentManager)
        {
            opheliaTex = theContentManager.Load<Texture2D>("Ophelia2");
            othelloTex = theContentManager.Load<Texture2D>("Othello2");
            oberonTex = theContentManager.Load<Texture2D>("Oberon");
            backgroundTex = theContentManager.Load<Texture2D>("hangarback");
            upgrade1Tex = theContentManager.Load<Texture2D>("Upgrade1");
            upgrade2Tex = theContentManager.Load<Texture2D>("Upgrade2");
            engineTex = theContentManager.Load<Texture2D>("Engine");
            holdTex = theContentManager.Load<Texture2D>("Hold");
            laserPowTex = theContentManager.Load<Texture2D>("LaserPower");
            thrustTex = theContentManager.Load<Texture2D>("SideThrust");
            fuelTex = theContentManager.Load<Texture2D>("FuelTank");
            
            laserShiTex = theContentManager.Load<Texture2D>("LaserShield");
            
            backTex = theContentManager.Load<Texture2D>("Back");
            upgradeShipTex = theContentManager.Load<Texture2D>("UpgradeShip2");
            sellTex = theContentManager.Load<Texture2D>("Sell");
        }

        public void Update(GameTime gameTime)
        {
            //Get the mouse's movements

            Game.previousKeyBoardState = Game.aCurrentKeyboardState;
            Game.aCurrentKeyboardState = Keyboard.GetState();

            prevMouseState = mouseState;
             mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);


            if (theCurrentShip == Ships.Ophelia)
            {
                 if (upgradeShipPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released )
                  {
                      theCurrentShip = Ships.Othello;
                      Player.currentShip = Player.ShipState.Othello;
                  }
            }

            if (theCurrentShip == Ships.Othello)
            {
                if (upgradeShipPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && Game.aCurrentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    theCurrentShip = Ships.Oberon;
                    Player.currentShip = Player.ShipState.Oberon;
                }
            }


            //Click ship part
            if (enginePos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                engineClicked = true;
                partSelected = true;
            }
           
            else if (fuelTankPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                fuelTankClicked = true;
                partSelected = true;
            }
           
            else if (laserPowPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                laserPowerClicked = true;
                partSelected = true;
            }
            else if (holdPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                holdClicked = true;
                partSelected = true;
            }
            else if (laserShiPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                laserShieldClicked = true;
                partSelected = true;
            }

          
            else if (sideThrustPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                sideThrusterClicked = true;
                partSelected = true;
            }
            else if (sellPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                int addCredits = player.currentHold * 1000;
                player.credits += addCredits;
                player.currentHold -= player.currentHold;
            }
            else if (backPos.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                Game.gameState = Game.GameMode.OuterMap;
            }
            if (partSelected == true)
            {
                #region Ophelia Upgrades

                #region Upgrade 1
                if (theCurrentShip == Ships.Ophelia)
                {
                    int cost = 200;
                    if (upgrade1.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && Game.aCurrentKeyboardState.IsKeyDown(Keys.Space) && player.credits >= cost)
                    {
                        if (engineClicked == true)
                        {
                            player.maxSpeed = 6;
                            player.speedingUp = 0.03f;
                            engineClicked = false;
                            partSelected = false;
                            player.credits -= 200;
                        }
                        else if (laserShieldClicked == true)
                        {
                            //apply upgrade 1 to laser shield 
                            //AND return to map
                            player.decFuel = 0.25f;
                            laserShieldClicked = false;
                            partSelected = false;
                            player.credits -= 400;
                        }
                       
                        else if (fuelTankClicked == true)
                        {
                            player.decFuel = 0.2f;
                            fuelTankClicked = false;
                            partSelected = false;
                            player.credits -= 200;
                        }
                      
                        else if (laserPowerClicked == true)
                        {
                            //apply upgrade 1 to laser power 
                            //AND return to map
                            for (int i = 0; i < player.bulletArray.Length; i++)
                            {
                                player.bulletArray[i].speed = 3;
                            }

                            laserPowerClicked = false;
                            partSelected = false;
                            player.credits -= 200;
                        }
                      
                        else if (holdClicked == true)
                        {
                            player.holdCapacity = 12;
                            holdClicked = false;
                            partSelected = false;
                            player.credits -= 200;
                        }
                        else if (sideThrusterClicked == true)
                        {
                            //apply upgrade 1 to side thruster 
                            //AND return to map
                            player.addTurning = 0.06f;
                            sideThrusterClicked = false;
                            partSelected = false;
                            player.credits -= 200;
                        }
                    }
                #endregion


                    #region Upgrade 2
                    else if (upgrade2.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && Game.aCurrentKeyboardState.IsKeyDown(Keys.Space) && player.credits >= cost + 200)
                    {
                        if (engineClicked == true)
                        {
                            //apply upgrade 2 to engine 
                            //AND return to map
                            player.maxSpeed = 7;
                            player.speedingUp = 0.04f;
                            engineClicked = false;
                            partSelected = false;
                            player.credits -= 400;
                        }
                        else if (laserShieldClicked == true)
                        {
                            //apply upgrade 1 to laser shield 
                            //AND return to map
                            player.decFuel = 0.23f;
                            laserShieldClicked = false;
                            partSelected = false;
                            player.credits -= 400;
                        }
                        else if (fuelTankClicked == true)
                        {
                            //apply upgrade 2 to fuel tank 
                            //AND return to map
                            player.decFuel = 0.15f;
                            fuelTankClicked = false;
                            partSelected = false;
                            player.credits -= 400;
                        }
                       
                        else if (laserPowerClicked == true)
                        {
                            //apply upgrade 2 to laser power 
                            //AND return to map
                            for (int i = 0; i < player.bulletArray.Length; i++)
                            {
                                player.bulletArray[i].speed = 4;
                            }
                            laserPowerClicked = false;
                            partSelected = false;
                            player.credits -= 400;
                        }
                     
                        else if (holdClicked == true)
                        {
                            //apply upgrade 2 to hold
                            // AND return to map
                            player.holdCapacity = 14;
                            holdClicked = false;
                            partSelected = false;
                            player.credits -= 400;
                        }
                        else if (sideThrusterClicked == true)
                        {
                            //apply upgrade 2 to side thruster
                            // AND return to map
                            player.addTurning = 0.07f;
                            sideThrusterClicked = false;
                            partSelected = false;
                            player.credits -= 400;
                        }
                    }
                    #endregion
                }
                #endregion

                #region Ophelia Upgrades
                #region Updrage 1
                if (theCurrentShip == Ships.Othello)
                {
                    int cost = 600;
                    if (upgrade1.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && Game.aCurrentKeyboardState.IsKeyDown(Keys.Space))
                    {
                        if (engineClicked == true)
                        {
                            player.maxSpeed = 8;
                            player.speedingUp = 0.05f;
                            engineClicked = false;
                            partSelected = false;
                            player.credits -= 600;
                        }
                       
                        else if (fuelTankClicked == true)
                        {
                            player.decFuel = 0.1f;
                            fuelTankClicked = false;
                            partSelected = false;
                            player.credits -= 600;
                        }
                       
                        else if (laserPowerClicked == true)
                        {
                            //apply upgrade 1 to laser power 
                            //AND return to map
                            for (int i = 0; i < player.bulletArray.Length; i++)
                            {
                                player.bulletArray[i].speed = 5;
                            }

                            laserPowerClicked = false;
                            partSelected = false;
                            player.credits -= 600;
                        }
                        else if (laserShieldClicked == true)
                        {
                            //apply upgrade 1 to laser shield 
                            //AND return to map
                            player.decFuel = 0.2f;
                            laserShieldClicked = false;
                            partSelected = false;
                            player.credits -= 400;
                        }
                       
                        else if (holdClicked == true)
                        {
                            player.holdCapacity = 16;
                            holdClicked = false;
                            partSelected = false;
                            player.credits -= 600;
                        }
                        else if (sideThrusterClicked == true)
                        {
                            //apply upgrade 1 to side thruster 
                            //AND return to map
                            player.addTurning = 0.9f;
                            sideThrusterClicked = false;
                            partSelected = false;
                            player.credits -= 600;
                        }
                    }

                #endregion

                    #region Upgrade 2
                    else if (upgrade2.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && Game.aCurrentKeyboardState.IsKeyDown(Keys.Space) && player.credits >= cost + 200)
                    {
                        if (engineClicked == true)
                        {
                            //apply upgrade 2 to engine 
                            //AND return to map
                            player.maxSpeed = 8;
                            player.speedingUp = 0.075f;
                            engineClicked = false;
                            partSelected = false;
                            player.credits -= 800;
                        }
                        else if (laserShieldClicked == true)
                        {
                            //apply upgrade 1 to laser shield 
                            //AND return to map
                            player.decFuel = 0.17f;
                            player.credits -= 400;
                            laserShieldClicked = false;
                            partSelected = false;
                        }
                        else if (fuelTankClicked == true)
                        {
                            //apply upgrade 2 to fuel tank 
                            //AND return to map
                            player.decFuel = 0.05f;
                            fuelTankClicked = false;
                            partSelected = false;
                            player.credits -= 800;
                        }
                       
                        else if (laserPowerClicked == true)
                        {
                            for (int i = 0; i < player.bulletArray.Length; i++)
                            {
                                player.bulletArray[i].speed = 7;
                            }
                            laserPowerClicked = false;
                            partSelected = false;
                            player.credits -= 800;
                        }
                      
                        else if (holdClicked == true)
                        {
                            //apply upgrade 2 to hold
                            // AND return to map
                            player.holdCapacity = 18;
                            holdClicked = false;
                            partSelected = false;
                            player.credits -= 800;
                        }
                        else if (sideThrusterClicked == true)
                        {
                            //apply upgrade 2 to side thruster
                            // AND return to map
                            player.addTurning = 0.1f;
                            sideThrusterClicked = false;
                            partSelected = false;
                            player.credits -= 800;
                        }
                    }
                }
                    #endregion
                #endregion

                #region Oberon Upgrades
                #region Upgrade 1
                if (theCurrentShip == Ships.Oberon)
                {
                    int cost = 1000;
                    if (upgrade1.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && Game.aCurrentKeyboardState.IsKeyDown(Keys.Space) && player.credits >= cost)
                    {
                        if (engineClicked == true)
                        {
                            player.maxSpeed = 9;
                            player.speedingUp = 0.075f;
                            engineClicked = false;
                            partSelected = false;
                            player.credits -= 1000;
                        }
                        else if (laserShieldClicked == true)
                        {
                            //apply upgrade 1 to laser shield 
                            //AND return to map
                            player.decFuel = 0.15f;
                            player.credits -= 400;
                            laserShieldClicked = false;
                            partSelected = false;

                        }
                        
                        else if (fuelTankClicked == true)
                        {
                            player.decFuel = 0.4f;
                            fuelTankClicked = false;
                            partSelected = false;
                            player.credits -= 1000;
                        }
                        
                        else if (laserPowerClicked == true)
                        {
                            //apply upgrade 1 to laser power 
                            //AND return to map
                            for (int i = 0; i < player.bulletArray.Length; i++)
                            {
                                player.bulletArray[i].speed = 6;
                            }

                            laserPowerClicked = false;
                            partSelected = false;
                            player.credits -= 1000;
                        }
                       
                        else if (holdClicked == true)
                        {
                            player.holdCapacity = 20;
                            holdClicked = false;
                            partSelected = false;
                            player.credits -= 1000;
                        }
                        else if (sideThrusterClicked == true)
                        {
                            //apply upgrade 1 to side thruster 
                            //AND return to map
                            player.addTurning = 0.11f;
                            sideThrusterClicked = false;
                            partSelected = false;
                            player.credits -= 1000;
                        }
                    }
                #endregion

                    #region Upgrade 2
                    else if (upgrade2.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && Game.aCurrentKeyboardState.IsKeyDown(Keys.Space) && player.credits >= cost + 200)
                    {
                        if (engineClicked == true)
                        {
                            //apply upgrade 2 to engine 
                            //AND return to map
                            player.maxSpeed = 9;
                            player.speedingUp = 0.15f;
                            engineClicked = false;
                            partSelected = false;
                            player.credits -= 1200;
                        }
                        else if (laserShieldClicked == true)
                        {
                            //apply upgrade 1 to laser shield 
                            //AND return to map
                            player.decFuel = 0.1f;
                            player.credits -= 400;
                            laserShieldClicked = false;
                            partSelected = false;
                        }
                      
                        else if (fuelTankClicked == true)
                        {
                            //apply upgrade 2 to fuel tank 
                            //AND return to map
                            player.decFuel = 0.03f;
                            fuelTankClicked = false;
                            partSelected = false;
                            player.credits -= 1200;
                        }
                       
                        else if (laserPowerClicked == true)
                        {
                            //apply upgrade 2 to laser power 
                            //AND return to map
                            for (int i = 0; i < player.bulletArray.Length; i++)
                            {
                                player.bulletArray[i].speed = 7;
                            }
                            laserPowerClicked = false;
                            partSelected = false;
                            player.credits -= 1200;
                        }
                        
                        else if (holdClicked == true)
                        {
                            //apply upgrade 2 to hold
                            // AND return to map
                            player.holdCapacity = 30;
                            holdClicked = false;
                            partSelected = false;
                            player.credits -= 1200;
                        }
                        else if (sideThrusterClicked == true)
                        {
                            //apply upgrade 2 to side thruster
                            // AND return to map
                            player.addTurning = 0.12f;
                            sideThrusterClicked = false;
                            partSelected = false;
                            player.credits -= 1200;
                        }
                    }
                }
                    #endregion
                #endregion

            }
               
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(backgroundTex, texturePosition, Color.White);
            theSpriteBatch.Draw(sellTex, sellPos, Color.White);
            theSpriteBatch.Draw(backTex, backPos, Color.White);
            if (theCurrentShip == Ships.Ophelia)
            {
                theSpriteBatch.Draw(opheliaTex, shipPos, Color.White);
            }
            else if (theCurrentShip == Ships.Othello)
            {
                theSpriteBatch.Draw(othelloTex, shipPos, Color.White);
            }
            else
            {
                theSpriteBatch.Draw(oberonTex, shipPos, Color.White);
            }

            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            if (partSelected == false)
            {
                theSpriteBatch.Draw(engineTex, enginePos, Color.White);
                theSpriteBatch.Draw(fuelTex, fuelTankPos, Color.White);
               
                theSpriteBatch.Draw(laserPowTex, laserPowPos, Color.White);
                theSpriteBatch.Draw(laserShiTex, laserShiPos, Color.White);
                theSpriteBatch.Draw(thrustTex, sideThrustPos, Color.White);
                theSpriteBatch.Draw(holdTex, holdPos, Color.White);
                theSpriteBatch.Draw(upgradeShipTex, upgradeShipPos, Color.White);
            }
            else
            {
                theSpriteBatch.Draw(upgrade1Tex, upgrade1, Color.White);
                theSpriteBatch.Draw(upgrade2Tex, upgrade2, Color.White);
            }
        }
    }
}