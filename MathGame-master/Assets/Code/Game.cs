using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance
    {
        get; private set;
    }

    private Player player;

    private List<Enemy> enemies;
    public List<Enemy> Enemies { get { return enemies; } }

    private List<Projectile> projectiles;

    private float screenShakeDuration = 100f;
    private Coroutine screenShakeRoutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = new Player();

        enemies = new List<Enemy>();
        for (int i = 0; i < 1; ++i)
        {
            enemies.Add(new Enemy(new DevMath.Vector2(Random.Range(.0f, Screen.width), Random.Range(.0f, Screen.height))));
        }

        projectiles = new List<Projectile>();
    }

    private void OnGUI()
    {
        player?.Render();

        enemies.ForEach(e => e.Render());

        projectiles.ForEach(p => p.Render());

        if (player == null)
        {
            //Use Sin to animate the colour of the text (GUI.color) between alpha 0.5 and 1.0
            Color newColor = GUI.color;
            newColor.a = DevMath.DevMath.Lerp(0.5f, 1f, Mathf.Sin(Time.time));
            GUI.color = newColor;

            GUI.Label(new Rect(Screen.width * .5f - 50.0f, Screen.height * .5f - 10.0f, 100.0f, 100.0f), "YOU LOSE!");
        }
    }

    public void CreateProjectile(DevMath.Vector2 position, DevMath.Vector2 direction, float startVelocity, float acceleration)
    {
        projectiles.Add(new Projectile(position, direction, startVelocity, acceleration));
    }

    private void ScreenShake()
    {
        //Implement screen shake with Sin + Matrices
        //Zoals je ziet is ook dit me niet gelukt ;_;. Ik snap de theorie van het schudden van een matrix maar heb geen idee waar ik de matrix
        //vervolgens op moet toepassen. Ik heb zowel de GUI als de projectionview van de camera geprobeerd maar dit haalde niks uit.

        if (screenShakeRoutine != null) StopCoroutine(screenShakeRoutine);
        screenShakeRoutine = StartCoroutine(IEScreenShake());
    }

    private IEnumerator IEScreenShake()
    {
        float _timeKey = 0;
        while (_timeKey < screenShakeDuration)
        {
            _timeKey += Time.deltaTime;
            DevMath.Vector3 newTranslation = new DevMath.Vector3(Random.Range(-100, 100), Random.Range(-500, 500), 0);

            Debug.Log(newTranslation.x + " | " + newTranslation.y);

            Camera.main.projectionMatrix = Extensions.ToUnity(DevMath.Matrix4x4.Translate(newTranslation));
            Debug.Log("SCREENSHAKE");

            //DevMath.Matrix4x4 newMatrix = DevMath.Matrix4x4.Identity * new DevMath.Vector4(1,2,1,1);
            //GUI.matrix = Extensions.ToUnity(DevMath.Matrix4x4.Identity);

            yield return null;
        }

        screenShakeRoutine = null;
        yield return null;
    }

    private void Update()
    {
        if (player == null) return;

        player.Update();

        enemies.ForEach(e => e.Update(player));

        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update();
            if (projectiles[i].ShouldDie)
            {
                projectiles.RemoveAt(i);
            }
        }

        foreach (Enemy e in enemies)
        {
            if (e.Circle.CollidesWith(player.Circle))
            {
                ScreenShake();
                player = null;
            }
        }

        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            for (int j = enemies.Count - 1; j >= 0; --j)
            {
                if (projectiles[i].Circle.CollidesWith(enemies[j].Circle))
                {
                    ScreenShake();
                    enemies.RemoveAt(j);
                    projectiles.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
