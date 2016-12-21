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

/*Bartosz Zych
 * C00205464
 * 4 weeks
 * known bugs: when the fast enemy spawns at level 2, the fast enemy from level 1 apears for a split second
 */
namespace JointGraphicsProgramingGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        TankyEnemy[] tankEnemies;
        FastEnemy fastEnemy;
        Shooter shooter;
        Map map;
        Button[] buttons;
        Boss boss;
        Texture2D win;
      

        Song backGroundMusic;
        
        bool level1Complete = false;
        bool level2Complete = false;
        bool level3Complete = false;
        
       

        int viewportHeight = 0;
        int viewportWidth = 0;

        Texture2D btnPlayTexture;
        Texture2D btnResetTexture;
        Texture2D gameOver;
        Texture2D mainMenuBackground;
        Rectangle mainMenuBackgroundRec;

        Texture2D pausedScreen;
        
        Texture2D level1Map;
        Texture2D level2Map;
        Texture2D level3Map;
        Texture2D bossMap;


        enum GameState
        {
            MainMenu,
            Playing,
            Pause,
            Reset,
        }
        GameState CurrentGameState = GameState.MainMenu;
        enum Level
        {
            Level1,
            Level2,
            Level3,
            BossRound,
        }
        Level CurrentLevel = Level.Level1;
      
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            player = new Player();
            tankEnemies = new TankyEnemy[6];
            for (int i = 0; i < tankEnemies.Length; i++ )
            {
                tankEnemies[i] = new TankyEnemy();
            }
            fastEnemy = new FastEnemy();
            shooter = new Shooter();
            map = new Map();
            boss = new Boss();

            buttons = new Button[2];
            
            viewportHeight = graphics.GraphicsDevice.Viewport.Height;
            viewportWidth = graphics.GraphicsDevice.Viewport.Width;
            mainMenuBackgroundRec = new Rectangle(0, 0, viewportWidth, viewportHeight);

            player.Initialize();
            for (int index = 0; index < tankEnemies.Length; index++)
            {
                tankEnemies[index].Initialize(viewportHeight, viewportWidth);
            }

           
            fastEnemy.Initialize();
            shooter.Initialize(viewportWidth);
            boss.Initialize();
           
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            backGroundMusic = Content.Load<Song>("Music");
            MediaPlayer.Play(backGroundMusic);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //background for levels
            level1Map = Content.Load<Texture2D>("background");
            level2Map = Content.Load<Texture2D>("level2Background");
            level3Map = Content.Load<Texture2D>("level3Background");
            bossMap = Content.Load<Texture2D>("BossMap");
            //loading the objects content
            player.LoadContent(this.Content, "name");
            fastEnemy.LoadContent(this.Content, "name");
            shooter.LoadContent(this.Content, "name");
            //using a for loop to load the content of enemy tank
            for (int index = 0; index < tankEnemies.Length; index++)
            {
                tankEnemies[index].LoadContent(this.Content, "name");
            }
            boss.LoadContent(this.Content, "name");
            win = Content.Load<Texture2D>("YouWin");
 
            //gamestate background images
            mainMenuBackground = Content.Load<Texture2D>("menu");
            pausedScreen = Content.Load<Texture2D>("PauseScreen");
            gameOver = Content.Load<Texture2D>("gameOver");

           
            //button content
            btnResetTexture = Content.Load<Texture2D>("Reset");
            btnPlayTexture = Content.Load<Texture2D>("PlayButton");
            //initialize buttons just once
            buttons[0] = new Button(btnPlayTexture);
            buttons[1] = new Button(btnResetTexture);
            IsMouseVisible = true;

           

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
             KeyboardState aCurrentKeyboardState = Keyboard.GetState();
             MouseState mouse = Mouse.GetState();
             
            // Allows the game to exit
            switch(CurrentGameState)
            {
                // updates when the game is in mainMenu state
                case GameState.MainMenu:
                    buttons[0].Update(mouse);
                    buttons[0].Position = new Vector2(600, 140);
                    if (buttons[0].MouseClicked == true)
                   {
                       CurrentGameState = GameState.Playing;
                   }
                    break;
                //updates when in playing gamestate
                case GameState.Playing:
                    if (aCurrentKeyboardState.IsKeyDown(Keys.Escape))
                    {
                        CurrentGameState = GameState.Pause;
                    }
                    CommonUpdate(gameTime);
                    switch(CurrentLevel)
                    {
                           //updates level1
                        case Level.Level1:

                            Level1Update(gameTime);
                            break;
                            //updates level 2
                        case Level.Level2:
                            Level2Update(gameTime);
                            break;
                            //updtaes level 3
                        case Level.Level3:
                            Level3Update(gameTime);
                            break;
                            //updates the boss round
                        case Level.BossRound:
                            BossRoundUpdate(gameTime);
                            break;
                    }
                   
                    break;
                //updates when in pause gamestate
                case GameState.Pause:
                    if (aCurrentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        CurrentGameState = GameState.Playing;
                    }
                    break;
                    //updates when in reset gamestate
                case GameState.Reset:
                     ResetGame();
                     CurrentLevel = Level.Level1;
                     buttons[1].Update(mouse);
                     buttons[1].Position = new Vector2(viewportWidth / 2, viewportHeight / 3);
                     if (buttons[1].MouseClicked == true)
                   {
                       CurrentGameState = GameState.MainMenu;
                   }
                    
                    break;
             }

          
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            switch (CurrentGameState)
            {
                    //draws the main menu
                case GameState.MainMenu:
                    spriteBatch.Draw(mainMenuBackground, mainMenuBackgroundRec, Color.White);
                    buttons[0].Draw(this.spriteBatch);
                    break;
                    //draws whatever happens in playing state
                case GameState.Playing:
                    // draws what occures in every level
                    
                    switch(CurrentLevel)
                    {
                            //draws level 1 images, font, etc
                        case Level.Level1:
                             map.Draw(this.spriteBatch, level1Map);
                             CommonDraw();
                            break;
                        //draws level 2 images, font, etc
                        case Level.Level2:
                             map.Draw(this.spriteBatch, level2Map);
                             CommonDraw();
                            break;
                        //draws level 3 images, font, etc
                        case Level.Level3:
                            map.Draw(this.spriteBatch, level3Map);
                            CommonDraw();
                            break;
                        //draws the boss round images, font, background, etc
                        case Level.BossRound:
                            map.Draw(this.spriteBatch, bossMap);
                            BossRoundDraw();
                           
                            break;
                    }
                     
                     break;
                    //draws everything that should be in pause menu
                case GameState.Pause:
                     spriteBatch.Draw(pausedScreen, mainMenuBackgroundRec, Color.White);
                     player.Draw(this.spriteBatch);
                     tankEnemies[0].Draw(this.spriteBatch, viewportWidth);
                     if (tankEnemies[0].Alive == false)
                     {
                         tankEnemies[1].Draw(this.spriteBatch, viewportWidth);
                     }
                     if (tankEnemies[1].Alive == false)
                     {
                          fastEnemy.Draw(this.spriteBatch, viewportWidth);
                     }
                     shooter.Draw(this.spriteBatch);
                     break;
                case GameState.Reset:
                     spriteBatch.Draw(gameOver, mainMenuBackgroundRec, Color.White);
                     buttons[1].Draw(this.spriteBatch);
                     break;
                     
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Collision()
        {
            //tank-player collision call

            for (int index = 0; index < tankEnemies.Length; index++)
            {
                if (tankEnemies[index].Alive)
                {
                    player.EnemyCollision(tankEnemies[index]);
                    player.BulletCollision(tankEnemies[index]);
                }
            }

            shooter.CollisionWithPlayer(player, shooter.Position);
            //fast-player collision call
            if (fastEnemy.Alive)
            {
                player.EnemyCollision(fastEnemy);
                player.BulletCollision(fastEnemy);
            }
            //boss-player collision calls
            if (boss.Alive)
            {
                player.EnemyCollision(boss);
                player.BulletCollision(boss);
            }
        }
        /// <summary>
        /// when the game is reseted all positions and values are back to normal
        /// </summary>
        public void ResetGame()
        {
           
            player.LoadContent(this.Content, "shuriken");
            player.Initialize();
            for (int index = 0; index < tankEnemies.Length; index++)
            {
                tankEnemies[index].Initialize(viewportHeight, viewportWidth);
                tankEnemies[index].ResetTextures();
            }
            fastEnemy.Initialize();

            shooter.Initialize(viewportWidth);
            buttons[0].Initialize();
            boss.Initialize();
            base.Initialize();
        }
        public void AllPropertiesUpdate()
        {
            if (tankEnemies[0].Health <= 0)
            {
                tankEnemies[0].Alive = false;
            }
           
           
        }
        /// <summary>
        /// sets a different values to enemies so they will be stronger
        /// </summary>
        public void Level2Reset()
        {
            for (int index = 0; index < tankEnemies.Length; index++)
            {
                tankEnemies[index].Initialize(viewportHeight, viewportWidth);
                tankEnemies[index].Damage = 50;
                tankEnemies[index].Speed = 1.4f;
            }
            player.Position = new Vector2(400, 300);
            fastEnemy.Initialize();
            fastEnemy.Health = 400;
            fastEnemy.Speed = 3.2f;
            shooter.Initialize(viewportWidth);
          

            
        }
        /// <summary>
        /// sets different values to enemies
        /// so they will be stronger
        /// in level 3
        /// </summary>
        public void Level3Reset()
        {
            for (int index = 0; index < tankEnemies.Length; index++)
            {
                tankEnemies[index].Initialize(viewportHeight, viewportWidth);
                tankEnemies[index].Health = 750;
                tankEnemies[index].Damage = 60;
                tankEnemies[index].Speed = 2f;
            }
            player.Position = new Vector2(400, 300);
            fastEnemy.Initialize();
            fastEnemy.Speed = 3.5f;
            shooter.Initialize(viewportWidth);
           
          
        }
        //resets for boss round
        public void BossRoundReset()
        {
            boss.Alive = true;
            player.Position = new Vector2(50, 400);
            boss.Position = new Vector2(viewportWidth / 2, 79);
        }
        /// <summary>
        /// method which contains common updates that are used by all levels
        /// </summary>
        public void CommonUpdate(GameTime gameTime)
        {
           
            AllPropertiesUpdate();
            Collision();
            player.Update(gameTime, viewportHeight, viewportWidth);
           
            if (player.Lives < 0)
                CurrentGameState = GameState.Reset;
        }

        /// <summary>
        /// method which contains common draws that are used by all levels
        /// </summary>
        public void CommonDraw()
        {
            player.Draw(this.spriteBatch);
            for (int i = 0; i < tankEnemies.Length;i++ )
            {
                tankEnemies[i].Draw(this.spriteBatch, viewportWidth);
            }
            fastEnemy.Draw(this.spriteBatch, viewportWidth);
            shooter.Draw(this.spriteBatch);
           
        }
        /// <summary>
        /// individual update for level 1
        /// updates the movement of each enemy 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Level1Update(GameTime gameTime)
        {
            shooter.LoadContent(this.Content, "shuriken");
            shooter.Update(gameTime, viewportWidth, viewportHeight);
            for (int i = 0; i < tankEnemies.Length;i++ )
            {
                tankEnemies[i].Update(gameTime, player.Position);
            }
            fastEnemy.Update(gameTime, player.Position);
            if (!tankEnemies[0].Alive && !tankEnemies[1].Alive && !tankEnemies[2].Alive && !tankEnemies[3].Alive && !tankEnemies[4].Alive && !tankEnemies[5].Alive && !fastEnemy.Alive)
            {
                CurrentLevel = Level.Level2;
                level1Complete = true;
            }
        }
        /// <summary>
        /// individual update for level 2
        /// </summary>
        /// <param name="gameTime"></param>
        public void Level2Update(GameTime gameTime)
        {
            if (level1Complete == true)
            {
                Level2Reset();
                for (int index = 0; index < tankEnemies.Length; index++)
                {
                    tankEnemies[index].SetTextureLevel2();
                }
                fastEnemy.SetTextureLevel2();

                level1Complete = false;
            }
            for (int i = 0; i < tankEnemies.Length; i++)
            {
                tankEnemies[i].Update(gameTime, player.Position);
            }
            fastEnemy.Update(gameTime, player.Position);
            shooter.LoadContent(this.Content, "name");
            shooter.Update(gameTime, viewportWidth, viewportHeight);
            if (!tankEnemies[0].Alive && !tankEnemies[1].Alive && !tankEnemies[2].Alive && !tankEnemies[3].Alive && !tankEnemies[4].Alive && !tankEnemies[5].Alive && !fastEnemy.Alive)
            {
                CurrentLevel = Level.Level3;
                level2Complete = true;
            }
        }
      /// <summary>
      /// individual update for level 3
      /// </summary>
      /// <param name="gameTime"></param>
        public void Level3Update(GameTime gameTime)
        {
            if (level2Complete == true)
            {
                Level3Reset();
                for (int index = 0; index < tankEnemies.Length; index++)
                {
                    tankEnemies[index].SetTextureLevel3();
                }
                fastEnemy.SetTextureLevel3();
                level2Complete = false;
            }
            for (int i = 0; i < tankEnemies.Length; i++)
            {
                tankEnemies[i].Update(gameTime, player.Position);
            }
            fastEnemy.Update(gameTime, player.Position);
            shooter.LoadContent(this.Content, "name");
            shooter.Update(gameTime, viewportWidth, viewportHeight);
            if (!tankEnemies[0].Alive && !tankEnemies[1].Alive && !tankEnemies[2].Alive && !tankEnemies[3].Alive && !tankEnemies[4].Alive && !tankEnemies[5].Alive && !fastEnemy.Alive)
            {
                CurrentLevel = Level.BossRound;
                level3Complete = true;
            }
        }
     /// <summary>
     /// updating the boos round
     /// </summary>
     /// <param name="gameTime"></param>
        public void BossRoundUpdate(GameTime gameTime)
        {
           if(level3Complete == true)
           {
               BossRoundReset();
               level3Complete = false;
           }
           boss.Update(gameTime,player.Position);
            if(boss.Health <=0)
            {
                boss.Alive = false;
            }
        }
        /// <summary>
        /// drwing the boss round 
        /// </summary>
        public void BossRoundDraw()
        {
            player.Draw(this.spriteBatch);
            boss.Draw(this.spriteBatch, viewportWidth);
            if(boss.Alive == false)
            {
                spriteBatch.Draw(win, mainMenuBackgroundRec, Color.White);
            }
        }

    }
}
