using System;
using System.Linq;

namespace lattice_sharp
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Lattice : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private bool _toggleFullscreen;

        public Lattice()
        {
            _graphics = new GraphicsDeviceManager(this) {HardwareModeSwitch = false};
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            _texture = CreateTexture(_graphics.GraphicsDevice, 30, 30);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                keyState.IsKeyDown(Keys.Escape))
                Exit();

            if ((keyState.IsKeyDown(Keys.LeftAlt) || keyState.IsKeyDown(Keys.RightAlt))
                && keyState.IsKeyDown(Keys.Enter))
            {
                _toggleFullscreen = true;
            }

            if (_toggleFullscreen &&
                (keyState.IsKeyUp(Keys.LeftAlt) &&
                 keyState.IsKeyUp(Keys.RightAlt)
                 || keyState.IsKeyUp(Keys.Enter)))
            {
                _graphics.ToggleFullScreen();
                _toggleFullscreen = false;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _spriteBatch.Draw(_texture, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private static Texture2D CreateTexture(GraphicsDevice device, int width, int height)
        {
            //initialize a texture
            var texture = new Texture2D(device, width, height);

            //the array holds the color for each pixel in the texture
            var data = new Color[width * height];
            var black = true;
            var row = 0;
            for (var pixel = 0; pixel < data.Length; pixel++)
            {
                var factor = pixel / height;
                if (factor > row) // new row
                {
                    row = factor;
                    black = factor % 2 == 0;
                }
                data[pixel] = black ? Color.Black : Color.White;
                black = !black;
            }

            //set the color
            texture.SetData(data);

            return texture;
        }
    }
}