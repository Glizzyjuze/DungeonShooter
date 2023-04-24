using Raylib_cs;
using System.Numerics;

public class Bullet
{
    public Vector2 target = new();
    public Vector2 position = new();
    
    public float bulletVelocity = 15f;

    public void Update()
    {
        // logik som flyttar närmare target
        Vector2 bulletMovement = new Vector2(0, 0);
        Vector2 direction = target - position;
        direction = Vector2.Normalize(direction);
        bulletMovement = direction * bulletVelocity;
        position.X += bulletMovement.X;
        position.Y += bulletMovement.Y;
        target.X += bulletMovement.X;
        target.Y += bulletMovement.Y;
    }
    public void Draw()
    {
        Raylib.DrawCircleV(position, 10, Color.YELLOW);
    }
}