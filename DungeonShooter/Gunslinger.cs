using Raylib_cs;
using System.Numerics;

static public class Gunslinger
{
    static public Rectangle gunslingerRec = new Rectangle(position.X, position.Y, 50, 70);
    static public Vector2 position = new(Raylib.GetScreenWidth()/2 - 25, Raylib.GetScreenHeight()/2 - 35);
    static public int hp = 100;
    static public float movementSpeed = 7f;

    public static void Update()
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            position.X += movementSpeed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            position.X -= movementSpeed;
        }
        gunslingerRec.x = position.X;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            position.Y += movementSpeed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            position.Y -= movementSpeed;
        }
        gunslingerRec.y = position.Y;
    }
}
