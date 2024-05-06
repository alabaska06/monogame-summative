using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_summative
{
    public class Game1 : Game
    {

        Texture2D bowlgraf, sonicW, stairset, sonicwave, speech, sonicflat, sonicollie;

        Rectangle sonicflatRect, sonicollieRect;

        Vector2 sonicollieSpeed, sonicflatSpeed;

        SpriteFont text;

        SoundEffect edm, ollie, skateroll;
        SoundEffectInstance edmInstance, ollieInstance, skaterollInstence;

        bool introPlayed;

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

            _graphics.PreferredBackBufferWidth = 800; 
            _graphics.PreferredBackBufferHeight = 500; 
            _graphics.ApplyChanges();

            base.Initialize();

            sonicflatRect = new Rectangle(680, 15, 150, 150);
            sonicollieRect = new Rectangle(600, 10, 150, 150);

            
            sonicollieSpeed = new Vector2 (-2, 1);
            sonicflatSpeed = new Vector2 (-2, 0);

            introPlayed = false;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bowlgraf = Content.Load<Texture2D>("bowlgraf");
            sonicW = Content.Load<Texture2D>("sonicW");
            stairset = Content.Load<Texture2D>("stairset");
            sonicwave = Content.Load<Texture2D>("sonicwave");
            speech = Content.Load<Texture2D>("speech");
            sonicflat = Content.Load<Texture2D>("sonicflat");
            sonicollie = Content.Load<Texture2D>("sonicollie");
            text = Content.Load<SpriteFont>("File");
            edm = Content.Load<SoundEffect>("edm");
            edmInstance = edm.CreateInstance();
            ollie = Content.Load<SoundEffect>("ollie");
            ollieInstance = ollie.CreateInstance();
            skateroll = Content.Load<SoundEffect>("skateroll");
            skaterollInstence = skateroll.CreateInstance(); 

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
                if (!introPlayed)
                {
                    edmInstance.Play();
                    introPlayed = true;
                }
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    screen = Screen.Animation;
                    edmInstance.Stop();
                }
                else if (edmInstance.State == SoundState.Stopped && introPlayed)
                {
                    screen = Screen.Animation;
                }

            }
            else if (screen == Screen.Animation)
            {
                Rectangle temp = sonicflatRect;
                temp.X += (int)sonicflatSpeed.X;
                temp.Y -= (int)sonicflatSpeed.Y;
                sonicflatRect = temp;

                Rectangle temp2 = sonicollieRect;
                temp2.X += (int)sonicollieSpeed.X;
                temp2.Y -= (int)sonicollieSpeed.Y;
                sonicollieRect = temp2;

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
                _spriteBatch.Draw(speech, new Rectangle(540, 80, 150, 100), Color.White);
                _spriteBatch.DrawString(text, ("Hi Friends! \nLeft click to continue!"), new Vector2(560, 110), Color.Black);


            }
            else if (screen == Screen.Animation)
            {
                 _spriteBatch.Draw(stairset, new Rectangle(0, 0, 800, 500), Color.White);
                 _spriteBatch.Draw(sonicflat, sonicflatRect, Color.White);
                 skaterollInstence.Play();

                 if (sonicflatRect.Location.X <= 600)
                 {
                     skaterollInstence.Stop();
                     _spriteBatch.Draw(sonicollie, sonicollieRect, Color.White);
                     sonicflatRect = new Rectangle(0, 0, 0, 0);
                 }
                 if (sonicollieRect.Location.X <= 500)
                 {
                     sonicollieSpeed = new Vector2(-3, -2);
                 }
                 if (sonicollieRect.Location.X <= 400)
                 {
                     ollieInstance.Play();
                 }
                 if (sonicollieRect.Location.X <= 10)
                 {
                     ollieInstance.Stop();
                     sonicflatRect = new Rectangle(10, 300, 150, 150);
                     sonicollieSpeed = new Vector2(0, 0);
                     sonicollieRect = new Rectangle(0, 0, 0, 0);
                     _spriteBatch.DrawString(text, ("Left click to go to the end screen."), new Vector2(450, 80), Color.Black);
                 }
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(sonicW, new Vector2(120, 0), Color.White);
                _spriteBatch.DrawString(text, ("Press esc to exit the program."), new Vector2(450, 80), Color.Black);
            }
            _spriteBatch.End();

                // TODO: Add your drawing code here

                base.Draw(gameTime);
        }
    }
}