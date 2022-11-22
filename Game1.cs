using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Monogame_4_Time___Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D bombTexture;
        SpriteFont _font;
        float seconds;
        float countdown;
        float startTime;
        MouseState mouseState;
        SoundEffect explode;
        bool explosion;
        Texture2D boom;
        float timer;
        bool timerSet;

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
            explosion = false;
            bool timerSet = false;
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            _font = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            boom = Content.Load<Texture2D>("boom");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            countdown = 15 - seconds;
            if (mouseState.LeftButton == ButtonState.Pressed){
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                explosion = false;
            }
            if (mouseState.RightButton == ButtonState.Pressed)
                startTime = (float)gameTime.TotalGameTime.TotalSeconds - 14;
            if (seconds >= 15){
                explode.Play();
                explosion = true;
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            if (explosion && !timerSet)
            {
                timer = (float)gameTime.TotalGameTime.TotalSeconds + (float)explode.Duration.TotalSeconds;
                timerSet = true;
            }
            if (timer <= (float)gameTime.TotalGameTime.TotalSeconds && explosion)
                Exit();
            if (explosion)
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;

            mouseState = Mouse.GetState();
            // TODO: Add your update logic here.

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Cyan);
            _spriteBatch.Begin();
            if (!explosion){
                _spriteBatch.Draw(bombTexture, new Rectangle(50, 50, 700, 400), Color.White);
                _spriteBatch.DrawString(_font, countdown.ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            else
                _spriteBatch.Draw(boom, new Rectangle(-90,-65, 1000,600), Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}