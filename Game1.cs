global using static SATCollision.Globals;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;

namespace SATCollision;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    Box box1;
    Box box2;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // TargetElapsedTime = new System.TimeSpan(Time you want); If you want to change the framerate.

        // Set the window size:
        graphics.PreferredBackBufferWidth = Width = 800;
        graphics.PreferredBackBufferHeight = Height = 500;
    }

    protected override void Initialize()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

//----------------------------------------------------------
        pixel = new(GraphicsDevice, 1, 1);
        pixel.SetData(new Color[] {Color.White});
        box1 = new(new(Width / 2, Height / 2), 100, 100, Color.Yellow);
        box2 = new(new(Width / 2, 100), 50, 50, Color.Blue);
//----------------------------------------------------------

        base.Initialize();
    }

    protected override void LoadContent()
    {

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

//----------------------------------------------------------
        HandleInput();
//----------------------------------------------------------

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();  
        //vec2.Normalize();
//----------------------------------------------------------
        box1.Draw(spriteBatch, pixel);
        box2.Draw(spriteBatch, pixel);
        for (uint i = 0; i < 4; i++) {
            var vec = box1.GetEdge(i);
            var vec2 = new Vector2(vec.Y, -vec.X);
            DrawLine(spriteBatch, pixel, vec, vec2, Color.Red, 3f);
        }
        for (uint i = 0; i < 4; i++) {
            var vec = box1.GetVertix(i);
            var vec2 = new Vector2(vec.Y, -vec.X);
            DrawLine(spriteBatch, pixel, vec, vec2, Color.Red, 3f);
        }
//----------------------------------------------------------
        spriteBatch.End();

        base.Draw(gameTime);
    }

    void HandleInput()
    {
        var ks = Keyboard.GetState();

        if (ks.IsKeyDown(Keys.Up)) {
            box1.Pos.Y--;
        }
        if (ks.IsKeyDown(Keys.Down)) {
            box1.Pos.Y++;
        }
        if (ks.IsKeyDown(Keys.Left)) {
            box1.Pos.X--;
        }
        if (ks.IsKeyDown(Keys.Right)) {
            box1.Pos.X++;
        }
        if (ks.IsKeyDown(Keys.W)) {
            box1.Rotation += 0.1f;
            box1.Update();
        }
        if (ks.IsKeyDown(Keys.S)) {
            box1.Rotation -= 0.1f;
            box1.Update();
        }
    }
}
