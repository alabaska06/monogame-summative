using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_summative
{
    public class Game1 : Game
    {

        Texture2D bowlgraf, sonicW, stairset, sonicwave, speech;

        SpriteFont text;

        MouseState mouseState, prevMouseState;

        enum Screen
        {
            Intro,
            Animation,
            End
        }
        Screen screen;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bowlgraf = Content.Load<Texture2D>("bowlgraf");
            sonicW = Content.Load<Texture2D>("sonicW");
            stairset = Content.Load<Texture2D>("stairset");
            sonicwave = Content.Load<Texture2D>("sonicwave");
            speech = Content.Load<Texture2D>("speech");
            text = Content.Load<SpriteFont>("File");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    screen = Screen.Animation;
            }
            else if (screen == Screen.Animation)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    screen = Screen.End;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(bowlgraf, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.Draw(sonicwave, new Rectangle(375, 100, 200, 300), Color.White);
                _spriteBatch.Draw(speech, new Rectangle (540, 80, 150, 100), Color.White);
                _spriteBatch.DrawString(text, ("Hi Friends!"), new Vector2(570, 120), Color.Black);
             

            }
            else if (screen == Screen.Animation)
            {
                _spriteBatch.Draw(stairset, new Rectangle(0, 0, 800, 500), Color.White);
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(sonicW, new Vector2(120, 0), Color.White);
            }
            _spriteBatch.End();

                // TODO: Add your drawing code here

                base.Draw(gameTime);
        }
    }
}