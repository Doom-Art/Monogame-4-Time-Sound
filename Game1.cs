using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_4_Time___Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D bombTexture;
        SpriteFont _font;
        float seconds;
        float startTime;
        MouseState mouseState;
        SoundEffect explode;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            this.Window.Title = "Bomb";
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            _font = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (mouseState.LeftButton == ButtonState.Pressed)
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            if (mouseState.RightButton == ButtonState.Pressed)
                startTime = (float)gameTime.TotalGameTime.TotalSeconds - 9;
            if (seconds >= 10){
                explode.Play();
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            mouseState = Mouse.GetState();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cyan);
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, new Rectangle(50, 50, 700, 400), Color.White);
            _spriteBatch.DrawString(_font, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}