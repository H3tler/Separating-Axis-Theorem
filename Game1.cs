global using static SATCollision.Globals;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
using System;

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
        box2 = new(new(700, 400), 50, 50, Color.Blue);
        Consolas = Content.Load<SpriteFont>("Consolas");
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
        box1.Color = Color.Yellow;

        if (Box.Collide(box1, box2)) {
            box1.Color = Color.Green;
        }
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
            float ang = MathF.Atan2(vec.Y - box1.Pos.Y, vec.X - box1.Pos.X);
            var vec2 = box1.GetRotatedPoint(ang, 10) + box1.Pos;
            DrawLine(spriteBatch, pixel, vec, vec2, Color.Red, 3f);
        }
        for (uint i = 0; i < 4; i++) {
            var vec = box1.GetVertix(i);
            float ang = MathF.Atan2(vec.Y - box1.Pos.Y, vec.X - box1.Pos.X);
            var vec2 = box1.GetRotatedPoint(ang, 10) + box1.Pos;
            DrawLine(spriteBatch, pixel, vec, vec2, Color.Red, 3f);
        }

        for (uint i = 0; i < 4; i++) {
            var vec = box1.GetVertix(i);
            var vec2 = Box.ProjectOnAxis(box1.Vertices, box2.GetVertix(i));
            DrawLine(spriteBatch, pixel, vec, vec2, Color.Blue, 3f);
        }

        for (uint i = 0; i < 4; i++) {
            var vec = box1.GetVertix(i);
            spriteBatch.Draw(pixel, vec, new Rectangle{Width = 5, Height = 5}, Color.Black, 
            0f, new Vector2(5 / 2, 5/ 2), 1f, SpriteEffects.None, 0f);
        }

        for (uint i = 0; i < 4; i++) {
            var vec = box1.GetVertix(i);
            spriteBatch.DrawString(Consolas, $"Vertex {i}: ({vec.X}, {vec.Y})", new(30,30 + (i * 50)), Color.Black);
        }

        spriteBatch.DrawString(Consolas, $"MidPoint: ({box1.Pos.X}, {box1.Pos.Y})", new(30,400), Color.Black);
   
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
        if (ks.IsKeyDown(Keys.E)) {
            box1.Rotation += 0.1f;
            box1.Update();
        }
        if (ks.IsKeyDown(Keys.Q)) {
            box1.Rotation -= 0.1f;
            box1.Update();
        }
    }
}
