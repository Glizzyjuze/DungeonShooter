using Raylib_cs;
using System.Numerics;

public class Zombie
{
    public Vector2 position = new(0, 0);
    public Vector2 target = new();

    public float speed = 3f;

    public Rectangle zombie;

    public void Update()
    {
        zombie = new Rectangle(position.X, position.Y, 50, 70);
        Vector2 zombieMovement = new Vector2(0, 0);
        position = new(zombie.x, zombie.y);
        target = new(Gunslinger.gunslingerRec.x, Gunslinger.gunslingerRec.y);
        Vector2 diff = target - position;
        Vector2 zombieDirection = Vector2.Normalize(diff);
        zombieMovement = zombieDirection * speed;
        position.X += zombieMovement.X;
        position.Y += zombieMovement.Y;
    }
}