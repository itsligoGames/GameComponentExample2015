using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Engine.Engines;

namespace ComponentExample
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MovingCircle : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //SpriteBatch spriteBatch;
        Texture2D movingCircle;
        Vector2 movingCirclePosition;
        Vector2 Target;
        
        public MovingCircle(Game game)
            : base(game)
        {
            game.Components.Add(this);
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            //spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            movingCircle = Game.Content.Load<Texture2D>("MovingCircle");
            movingCirclePosition = new Vector2(Utilities.Utility.NextRandom(600), Utilities.Utility.NextRandom(400));
            Target = new Vector2(Utilities.Utility.NextRandom(600), Utilities.Utility.NextRandom(400));
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if(InputEngine.CurrentMouseState.LeftButton == ButtonState.Pressed )
            {
                Rectangle bounds = new Rectangle(
                                (int)movingCirclePosition.X,
                                (int)movingCirclePosition.Y,
                                movingCircle.Width,
                                movingCircle.Height);
                if (bounds.Contains(InputEngine.MousePosition))
                {
                    this.Enabled = false;
                    this.Visible = false;
                }
            }
            movingCirclePosition = Vector2.Lerp(movingCirclePosition, Target, 0.1f);
            if (Vector2.Distance(movingCirclePosition, Target) < 0.2)
            {
                movingCirclePosition = Target;
                Target = new Vector2(Utilities.Utility.NextRandom(600), Utilities.Utility.NextRandom(400));
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Game.GraphicsDevice.Clear(Color.Black);
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            spriteBatch.Begin();
            spriteBatch.Draw(movingCircle, movingCirclePosition, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
