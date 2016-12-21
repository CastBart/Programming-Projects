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



/*
 * Game made by: Bartosz Zych, Cathal Ryan and Sam Patterson
 * 
 * Bartosz Zych Time Spent On Game Class ---6 Hours--- 
 * 
 * Toatal spent hours by each member
 * Bartosz Zych  ---62 Hours---
 * 
 * Cathal Ryan   ---???Hours---
 * 
 * Sam Patterson ---???Hours---
 * 
 *   ********************
 *   *    Percentage    *
 *   *   Bartosz Zych   *
 *   *    ---???---     * 
 *   *   Cathal Ryan    *
 *   *    ---???---     *
 *   *   Sam Patterson  *
 *   *    ---???---     *
 *   ********************
 * 
 */
namespace Asteroids_Re_Loaded
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {

        #region Veriables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

        public static Video toVideo, fromVideo;
        public static VideoPlayer videoPLayer;
        public static Texture2D videoTexture;
        Rectangle videoRect;

        SpriteFont font;
        Texture2D backgroundTexture,option, credits;

        public static int viewportWidth;
        public static int viewportHeight;
        int numOfAsteroids;
        int freeAsteroid;
        
        const int MaxAsteroids = 20;
        const int MaxAsteroidsOnScreen = 6;
        const int MaxLevels = 9;

        public static KeyboardState previousKeyBoardState;
        public static KeyboardState aCurrentKeyboardState;

        public enum GameMode { Loading ,Hangar ,LicenceScreen, SplashScreen, MainMenu, ToMission, FromMission, OuterMap, Playing, Credits, Options, GameOver };
        public static GameMode gameState = GameMode.Loading;
        public enum CurrentLevel{None, Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9};
        public static CurrentLevel currentLevel = CurrentLevel.None;

        //objects
        Gem[] gem;
        Player player;
        Enemy[] enemy;
        Asteroids[] asteroids;
        Level[] levels;
        MainMenu theMainMenu;
        OuterMap theOuterMap;
        Loading loading;
        Hangar hangar;
        Song music;
        public static SoundEffect click;
        float elapsed;
        float deadElapsed;
        Texture2D gameOver;
        

        



        #endregion 

        #region Constructor
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        #endregion

        #region Intialize, Load, Unload, Update, Draw
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            IsMouseVisible = true;
            viewportWidth = graphics.GraphicsDevice.Viewport.Width;
            viewportHeight = graphics.GraphicsDevice.Viewport.Height;
            videoPLayer = new VideoPlayer();
            videoRect = new Rectangle(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y,
                                     GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

          
           
            #region Level Declaration

            levels = new Level[MaxLevels];

            levels[0] = new Level(100, 0, true);
            levels[1] = new Level(60, 0, true);
            levels[2] = new Level(40, 1, true);
            levels[3] = new Level(50, 2, true);
            levels[4] = new Level(30, 2, true);
            levels[5] = new Level(60, 3, true);
            levels[6] = new Level(30, 3, true);
            levels[7] = new Level(60, 4, true);
            levels[8] = new Level(30, 5, true);

            #endregion
            player = new Player();
            theMainMenu = new MainMenu();
            theOuterMap = new OuterMap(levels);
            loading = new Loading();
            player.Initialize();
            hangar = new Hangar(player);
           // graphics.IsFullScreen = true;
           // graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            player.LoadContent(this.Content, "name");
            toVideo = Content.Load<Video>("GoingToMission");
            fromVideo = Content.Load<Video>("ComingFromMission");
            option = Content.Load<Texture2D>("Options");
            credits = Content.Load<Texture2D>("Credits");
            Gem.Content = this.Content;
            theMainMenu.LoadContent(this.Content);
            theOuterMap.LoadContent(this.Content);
            loading.LoadContent(this.Content);
            hangar.LoadContent(this.Content);
            music = Content.Load<Song>("backGroundMusic");
            click = Content.Load<SoundEffect>("Click");
            MediaPlayer.Play(music);
            backgroundTexture = Content.Load<Texture2D>("Background");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameOver = Content.Load<Texture2D>("gameover");

            //loading the image of the player
           
           
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
           
            switch (gameState)
            {
                case GameMode.Loading:
                    loading.Update(gameTime);
                    break;
                case GameMode.Hangar:
                    hangar.Update(gameTime);
                    break;
                case GameMode.LicenceScreen:
                    
                    elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if(elapsed>=3)
                    {
                        gameState = GameMode.MainMenu;
                    }
                    break;
                case GameMode.SplashScreen:
                    break;
                case GameMode.MainMenu:
                    theMainMenu.Update(gameTime, this);
                    break;
                case GameMode.ToMission:
                   // videoPLayer.Play(toVideo);
                    if (videoPLayer.State == MediaState.Stopped)
                    {
                        gameState = GameMode.Playing;
                    }
                    break;
                case GameMode.FromMission:
                    //videoPLayer.Play(fromVideo);
                    if (videoPLayer.State == MediaState.Stopped)
                    {
                        gameState = GameMode.OuterMap;
                    }
                    break;
                case GameMode.OuterMap:
                    theOuterMap.Update(gameTime, this);
                    break;
                case GameMode.Playing:
                    if (aCurrentKeyboardState.IsKeyDown(Keys.Back) && previousKeyBoardState.IsKeyUp(Keys.Back))
                    {
                        videoPLayer.Play(fromVideo);
                        gameState = GameMode.FromMission;
                    }
                    #region Levels
                    switch (currentLevel)
	                {
                       case CurrentLevel.None:
                         break;
                     case CurrentLevel.Level1:

                         PlayingUpdate(gameTime, levels[0].GemChance, Gem.TypeOfGem.Green);
                         break;
                     case CurrentLevel.Level2:
                         PlayingUpdate(gameTime, levels[1].GemChance, Gem.TypeOfGem.Green);
                         break;
                     case CurrentLevel.Level3:
                         PlayingUpdate(gameTime, levels[2].GemChance, Gem.TypeOfGem.Red);
                         break;
                     case CurrentLevel.Level4:
                         PlayingUpdate(gameTime, levels[3].GemChance, Gem.TypeOfGem.Red);
                         break;
                     case CurrentLevel.Level5:
                         PlayingUpdate(gameTime, levels[4].GemChance, Gem.TypeOfGem.Orange);
                         break;
                     case CurrentLevel.Level6:
                         PlayingUpdate(gameTime, levels[5].GemChance, Gem.TypeOfGem.Orange);
                         break;
                     case CurrentLevel.Level7:
                         PlayingUpdate(gameTime, levels[6].GemChance, Gem.TypeOfGem.Purple);
                         break;
                     case CurrentLevel.Level8:
                         PlayingUpdate(gameTime, levels[7].GemChance, Gem.TypeOfGem.Purple);
                         break;
                     case CurrentLevel.Level9:
                         PlayingUpdate(gameTime, levels[8].GemChance, Gem.TypeOfGem.Silver);
                         break;
                     default:
                         break;
	                }
                    #endregion
                    break;
                case GameMode.Credits:
                     Game.previousKeyBoardState = Game.aCurrentKeyboardState;
                     Game.aCurrentKeyboardState = Keyboard.GetState();
                    deadElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (aCurrentKeyboardState.IsKeyDown(Keys.Escape) && previousKeyBoardState.IsKeyUp(Keys.Escape))
                    {
                        
                        gameState = GameMode.MainMenu;
                    }
                    break;
                case GameMode.Options:
                     Game.previousKeyBoardState = Game.aCurrentKeyboardState;
                     Game.aCurrentKeyboardState = Keyboard.GetState();
                    if (aCurrentKeyboardState.IsKeyDown(Keys.Escape) && previousKeyBoardState.IsKeyUp(Keys.Escape))
                    {
                        
                        gameState = GameMode.MainMenu;
                    }
                    break;
                case GameMode.GameOver:
                    deadElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if(deadElapsed >=3)
                    {
                        gameState = GameMode.MainMenu;
                    }
                    
                    
                    break;
                default:
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
            GraphicsDevice.Clear(Color.Red);
           // videoTexture = videoPLayer.GetTexture();
            //begining of draw
            spriteBatch.Begin();

            switch (gameState)
            {
                case GameMode.Loading:
                    loading.Draw(this.spriteBatch);
                    break;
                case GameMode.Hangar:
                    hangar.Draw(this.spriteBatch);
                    spriteBatch.DrawString(Loading.theFont, "You Currently have " + player.credits + " Space Credits", new Vector2(20, 300), Color.White);
                    spriteBatch.DrawString(Loading.theFont, "You Currently have " + player.currentHold + " Gems", new Vector2(20, 325), Color.White);
                    break;
                case GameMode.LicenceScreen:
                    spriteBatch.Draw(credits, new Vector2(), Color.White);
                    break;
                case GameMode.SplashScreen:
                    break;
                case GameMode.MainMenu:
                    theMainMenu.Draw(this.spriteBatch);
                    break;
                case GameMode.ToMission:
                    videoTexture = videoPLayer.GetTexture();
                    spriteBatch.Draw(videoTexture, videoRect, Color.White);
                    break;
                case GameMode.FromMission:
                    videoTexture = videoPLayer.GetTexture();
                    spriteBatch.Draw(videoTexture, videoRect, Color.White);
                    break;
                case GameMode.OuterMap:
                    theOuterMap.Draw(this.spriteBatch);
                    break;
                case GameMode.Playing:
                    PlayingDraw(gameTime, player);
                    break;
                case GameMode.Credits:
                    spriteBatch.Draw(credits, new Vector2(), Color.White);
                    break;
                case GameMode.Options:
                    spriteBatch.Draw(option, new Vector2(), Color.White);
                    break;
                case GameMode.GameOver:
                    spriteBatch.Draw(gameOver, new Vector2(), Color.White);
                    break;
                default:
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Updates everything while in the playing mode
        /// </summary>
        /// <param name="gameTime"></param>
        private void PlayingUpdate(GameTime gameTime, int chanceForGem, Gem.TypeOfGem type)
        {
            player.Update(gameTime, viewportWidth, viewportHeight);

            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].Update(gameTime, viewportWidth, viewportHeight, player, enemy[i]);
            }
          
            for (int i = 0; i < numOfAsteroids; i++)
            {
                asteroids[i].Update(viewportWidth, viewportHeight);
            }
            for (int i = 0; i < numOfAsteroids; i++)
            {
                gem[i].Update();
            }
            Collisions(chanceForGem, type);
        }

        /// <summary>
        /// commutes collision for
        /// ** asteroid-player **
        /// ** bullet-asteroid **
        /// **  bullet-player  **
        /// **  bullet-enemy   **
        /// **  enemy-player   **
        /// </summary>
        /// <param name="chanceForGem"></param>
        /// <param name="type"></param>
        private void Collisions(int chanceForGem, Gem.TypeOfGem type)
        {
            //player-enemy
            for (int i = 0; i < enemy.Length; i++)
            {
                
                player.CollisionEnemy(enemy[i], ref deadElapsed);
            }
           
            //players bullets-enemyship
            for (int i = 0; i < player.bulletArray.Length; i++)
            {
                for (int j = 0; j < enemy.Length; j++)
			    {
                    player.bulletArray[i].CollisionWithShips(enemy[j]);
			    }
            }
            //enemy bullets- player
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].bulletArray[i].CollisionWithShips(player, ref deadElapsed);
            }
           
            for (int i = 0; i < numOfAsteroids; i++)
            {
                if(player.Alive)
                {
                    player.AsteroidCollision(asteroids[i], player.Texture, ref deadElapsed);
                }
                for (int j = 0; j < player.bulletArray.Length; j++)
                {
                   
                    for (int k = 0; k < asteroids.Length; k++)
                    {
                        if (!asteroids[k].Alive)
                        {
                            freeAsteroid = k;
                            break;
                        }
                    }
                    
                    if(player.bulletArray[j].Alive)
                    {
                        player.bulletArray[j].BulletAsteroidCollision(asteroids[i], asteroids[freeAsteroid], gem[i], chanceForGem, type, ref numOfAsteroids, this.Content);
                       
                    }
                }
            }
            for (int i = 0; i < levels.Length; i++)
            {
                for (int j = 0; j < asteroids.Length; j++)
                {
                    if (!asteroids[i].Alive && i < levels.Length)
	                {
                        levels[i].Active = true;
	                }
                }
            }

            for (int i = 0; i < gem.Length; i++)
            {
                gem[i].PlayerCollision(player);
            }
           
        }
                    

        /// <summary>
        /// Draws everything while in the playing mode
        /// </summary>
        private void PlayingDraw(GameTime gameTime, Player player)
        {
            spriteBatch.Draw(backgroundTexture, new Vector2(), Color.White);
            player.Draw(this.spriteBatch, gameTime );
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].Draw(this.spriteBatch, player);   
            }
            
            for (int i = 0; i < numOfAsteroids; i++)
            {
                asteroids[i].Draw(this.spriteBatch, 100, 94);
            }
            for (int i = 0; i < asteroids.Length; i++)
            {
                gem[i].Draw(this.spriteBatch);
            }
        }

        /// <summary>
        /// Creates a specific ammount of
        /// **Asteroids**
        /// **Gems**
        /// in the a level depending which level it is
        /// </summary>
        /// <param name="numOfAsteroids"></param>
        public void LoadLevel(int newNumOfAsteroids, int nexEnemy)
        {
            player.Initialize();
            player.LoadContent(this.Content, "hi");
            enemy = new Enemy[nexEnemy];
            for (int i = 0; i < nexEnemy; i++)
            {
                   enemy[i] = new Enemy();
                   enemy[i].Initialize();
                   enemy[i].LoadContent(this.Content);
               
            }
           // enemy.Initialize();
            numOfAsteroids = newNumOfAsteroids;
            int asteroidsInLevel = numOfAsteroids * 2 * 3;

            gem = new Gem[asteroidsInLevel];
            for (int i = 0; i < asteroidsInLevel; i++)
            {
                gem[i] = new Gem();
                gem[i].LoadContent(this.Content, "GreenGem");
             
            }

            asteroids = new Asteroids[asteroidsInLevel];
            for (int i = 0; i < asteroidsInLevel; i++)
            {
                asteroids[i] = new Asteroids();
            }

            //make passed value of asteroid in the level spawn 
            for (int i = 0; i < numOfAsteroids; i++)
            {
                asteroids[i].MakeAsteroid(Asteroids.AsteroidType.Big, new Vector2(), this.Content);
            }
            //gems
           

           
        }

       
        #endregion

    }
}
