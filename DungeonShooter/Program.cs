using System.Numerics;
using Raylib_cs;

Raylib.InitWindow(1920, 1080, "DungeonShooter");
Raylib.SetTargetFPS(60);
Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);

string currentScene = "startScreen";
int points = 0;

Random generator = new Random();

float screenWidth = Raylib.GetScreenWidth();
float screenHeight = Raylib.GetScreenHeight();

//Crosshair
Rectangle crosshairRec = new Rectangle(screenWidth/2 - 25, screenHeight/2 - 25, 50, 50);
Vector2 crosshairPosition = new(crosshairRec.x, crosshairRec.y);
Texture2D crosshairSprite;
crosshairSprite = Raylib.LoadTexture(@"crosshair/Crosshair.png");

Raylib.HideCursor();

List<Bullet> bullets = new();

int zombieY = generator.Next(Raylib.GetScreenHeight());
List<Zombie> zombies = new List<Zombie>();
zombies.Add(new Zombie());
zombies.Add(new Zombie());
zombies.Add(new Zombie());
zombies[1].position.X = Raylib.GetScreenWidth();
zombies[2].position.X = -50;
zombies[1].position.Y = zombieY;
zombies[2].position.Y = zombieY;

Camera2D camera = new Camera2D();
camera.target = new Vector2(Gunslinger.position.X, Gunslinger.position.Y);
camera.offset = new Vector2(screenWidth/2 - 25, screenHeight/2 - 35);
camera.rotation = 0;
camera.zoom = 1;

while (!Raylib.WindowShouldClose())
{
    //Crosshair
    Vector2 mousePosition = Raylib.GetMousePosition();
    crosshairPosition = mousePosition + camera.target - camera.offset;
    crosshairRec.x = crosshairPosition.X - crosshairRec.width / 2;
    crosshairRec.y = crosshairPosition.Y - crosshairRec.height / 2;

    Gunslinger.Update();

    //Camera target gunslinger
    camera.target = new Vector2(Gunslinger.position.X, Gunslinger.position.Y);

    //Bullets
    if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
    {
        Bullet b = new Bullet();
        b.position = Gunslinger.position;
        b.target = crosshairPosition;

        bullets.Add(b);
    }

    foreach (Bullet bullet in bullets)
    {
        bullet.Update();
    }

    //Graphics
    Raylib.BeginDrawing();

    if (currentScene == "lose")
    {
        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawText("You lose.", 150, Raylib.GetScreenHeight() / 2 - 50, 100, Color.YELLOW);
    }

    if (points == 3)
    {
        currentScene = "win";
    }

    if (currentScene == "win")
    {
        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawText("You win! Press ESC to exit the game.", 50, Raylib.GetScreenHeight() / 2 - 30, 60, Color.YELLOW);
    }

    if (currentScene == "startScreen")
    {
        Raylib.ClearBackground(Color.BLACK);

        Raylib.DrawText("Welcome to DungeonShooter. Press ENTER on your keyboard to start", 50, Raylib.GetScreenHeight() / 2 - 25, 50, Color.YELLOW);

        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
        }
    }

    if (currentScene == "game")
    {
        Raylib.ClearBackground(Color.GRAY);

        Raylib.BeginMode2D(camera);

        Raylib.DrawTexture(crosshairSprite, (int)crosshairPosition.X, (int)crosshairPosition.Y,Color.WHITE);
        Raylib.DrawRectangleRec(Gunslinger.gunslingerRec, Color.BROWN);

        foreach (Bullet bullet in bullets)
        {
            bullet.Draw();
        }

        foreach (Zombie z in zombies)
        {
            Raylib.DrawRectangleRec(z.zombie, Color.GREEN);
            z.Update();
            zombieY = new();

            if (Raylib.CheckCollisionRecs(Gunslinger.gunslingerRec, z.zombie))
            {
                currentScene = "lose";
            }
        }

        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            Bullet b = bullets[i];

            for (int j = zombies.Count - 1; j >= 0; j--)
            {
                Zombie z = zombies[j];

                if (Raylib.CheckCollisionCircleRec(b.position, 5, z.zombie))
                {
                    bullets.RemoveAt(i);
                    zombies.RemoveAt(j);
                    points += 1;
                    break;
                }
            }
        }

        Raylib.EndMode2D();
    }

    Raylib.EndDrawing();
}