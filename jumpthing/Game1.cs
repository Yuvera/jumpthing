using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

// player movement 
// coin class
// level switching
// ui
// sounds
// lives/checkpoints

namespace jumpthing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D backgroundTxr, playerSheetTxr, platformSheetTxr, whiteBox;

        Point screenSize = new Point(800, 450);
        int levelNumber = 0;
        PlayerSprite playerSprite;
        CoinSprite coinSprite;

        List<List<PlatformSprite>> levels = new List<List<PlatformSprite>>();
        List<Vector2> coins = new List<Vector2>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTxr = Content.Load<Texture2D>("JumpThing_background");
            playerSheetTxr = Content.Load<Texture2D>("JumpThing_spriteSheet1");
            platformSheetTxr = Content.Load<Texture2D>("JumpThing_spriteSheet2");

            whiteBox = new Texture2D(GraphicsDevice, 1, 1);
            whiteBox.SetData(new[] { Color.White } );

            playerSprite = new PlayerSprite(playerSheetTxr, whiteBox, new Vector2(100,50));
            coinSprite = new CoinSprite(playerSheetTxr, whiteBox, new Vector2(200, 200));

            BuildLevels();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            playerSprite.Update(gameTime, levels[levelNumber]);
            if (playerSprite.spritePos.Y > screenSize.Y + 50) playerSprite.ResetPlayer(new Vector2(100, 50));
            if (playerSprite.checkCollision(coinSprite))
            {
                levelNumber++;
                if (levelNumber >= levels.Count) levelNumber = 0;
                coinSprite.spritePos = coins[levelNumber];
                playerSprite.ResetPlayer(new Vector2(100, 50));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTxr, new Rectangle(0, 0, screenSize.X, screenSize.Y), Color.White);

            playerSprite.Draw(_spriteBatch, gameTime);

            coinSprite.Draw(_spriteBatch, gameTime);
            
            foreach(PlatformSprite platform in levels[levelNumber])
            {
                platform.Draw(_spriteBatch, gameTime);
            }

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        void BuildLevels()
        {
            levels.Add(new List<PlatformSprite>());
            levels[0].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 300)));
            levels[0].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 300)));
            coins.Add(new Vector2(200, 200));

            levels.Add(new List<PlatformSprite>());
            levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 200)));
            levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 150)));
            levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(350, 100)));
            coins.Add(new Vector2(350, 60));

            levels.Add(new List<PlatformSprite>());
            levels[2].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(120, 100)));
            levels[2].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(120, 140)));
            levels[2].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(40, 100)));
            coins.Add(new Vector2(35,330));
        }

    }
}
