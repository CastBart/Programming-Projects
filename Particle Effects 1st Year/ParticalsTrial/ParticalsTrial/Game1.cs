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

namespace ParticalsTrial
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Particals[] particals;
        bool alive = false;
        static Random rnd = new Random();
        KeyboardState keyBoardState;
        KeyboardState previousKeybardStatre;

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
            particals = new Particals[10];
            for (int i = 0; i < particals.Length; i++)
            {
                particals[i] = new Particals();
            }
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            Particals.Content = Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            for (int i = 0; i < particals.Length; i++)
            {
                particals[i].LoadContent(this.Content);
            }
            
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
            int j;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            previousKeybardStatre = keyBoardState;
            keyBoardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            if(keyBoardState.IsKeyDown(Keys.Space) && previousKeybardStatre.IsKeyUp(Keys.Space))
            {
                for (int i = 0; i < particals.Length; i++)
                {
                    particals[i] = new Particals(new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10)), 0f, new Vector2(mouseState.X, mouseState.Y), j = rnd.Next(1, 5));
                    alive = true;
                }
            }
            if(alive)
            {
                for (int i = 0; i < particals.Length; i++)
                {
                   
                    particals[i].Update();
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            for (int i = 0; i < particals.Length; i++)
            {
                particals[i].Draw(this.spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
