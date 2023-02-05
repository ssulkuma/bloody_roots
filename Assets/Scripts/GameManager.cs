using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public StartGame Game;
    public Score CurrentScore;
    private GameObject Enemy;
    private SpriteRenderer EnemySpriteRenderer;
    public Sprite EnemySprite;
    private GameObject ExtraLife;
    private SpriteRenderer ExtraSpriteRenderer;
    public Sprite ExtraLifeSprite;
    public Roots Player;
    public GameObject Pause;
    public GameObject GameOver;
    public GameObject MaxLifeCount;
    public GameObject RetryGameButton;
    public TMP_Text CurrentLifeCount;
    private Collider2D Collision;
    private float Speed;
    private float MinX;
    private float MaxX;
    private float MaxY;
    private float MinY;
    private int PreviousLifeCount;

    void Start()
    {
        Speed = 80.0f;
        MinX = 265.0f;
        MaxX = 486.0f;
        MinY = 0.0f;
        MaxY = 350.0f;
        Player.LifeCount = 1;
        PreviousLifeCount = Player.LifeCount;
    }

    // The game loop, called once each frame. Displays different things
    // between the game's states.
    void Update()
    {
        TogglePause();
        if (Game.GameState == "GamePlay")
            DisplayGamePlayState();
        else if (Game.GameState == "Pause")
            DisplayPauseState();
        else if (Game.GameState == "GameOver")
            DisplayGameOverState();
    }

    // The Game play. Creates new enemy obstacles and extra life icons to
    // travel through the screen vertically. Increases the speed which they
    // move every 250 score points reached. Displays score and life count.
    void DisplayGamePlayState()
    {
        if (PreviousLifeCount > Player.LifeCount && Enemy)
            Destroy(Enemy);
        if (PreviousLifeCount < Player.LifeCount && ExtraLife)
            Destroy(ExtraLife);
        if (Pause.activeInHierarchy == true)
            Pause.SetActive(false);
        if (MaxLifeCount.activeInHierarchy == false)
            MaxLifeCount.SetActive(true);
        CurrentLifeCount.text = Player.LifeCount.ToString("0");
        if (!Enemy)
            CreateEnemy();
        if (!ExtraLife && Player.LifeCount < 3)
            CreateExtraLife();
        Enemy.transform.position += new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * Speed;
        if (Enemy.transform.position.y > MaxY)
            Destroy(Enemy);
        if (ExtraLife)
        {
            ExtraLife.transform.position += new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * Speed;
            if (ExtraLife.transform.position.y > MaxY)
                Destroy(ExtraLife);
        }
        if (((int)CurrentScore.ScoreValue % 250) == 0)
            Speed += 1.0f;
        PreviousLifeCount = Player.LifeCount;
    }

    // Pauses the game, keeping all objects in place and displays PAUSE
    // text in the middle of the screen.
    void DisplayPauseState()
    {
        Pause.SetActive(true);
        while (Game.GameState == "GamePlay")
            TogglePause();
    }

    // When player uses all their lives, displays GAME OVER on the screen
    // with the score gotten.
    void DisplayGameOverState()
    {
        if (MaxLifeCount.activeInHierarchy == true)
            MaxLifeCount.SetActive(false);
        CurrentLifeCount.text = " ";
        if (GameOver.activeInHierarchy == false)
            GameOver.SetActive(true);
        if (Enemy)
            Destroy(Enemy);
        if (ExtraLife)
            Destroy(ExtraLife);
        if (RetryGameButton.activeInHierarchy == false)
            RetryGameButton.SetActive(true);
        Vector3 score = CurrentScore.gameObject.transform.position = new Vector3(800.0f, 440.0f, 291.3f);
        Speed = 80.0f;
        Player.LifeCount = 1;
    }

    // Creates an enemy with box collider and the enemy sprite. Randomizes 
    // a new starting position on the horizontal axis each time.
    void CreateEnemy()
    {
        Enemy = new GameObject("Enemy", typeof(PolygonCollider2D));
        SetEnemyCollider();
        Enemy.tag = "Enemy";
        Collision = Enemy.GetComponent<Collider2D> ();
        Collision.isTrigger = true;
        EnemySpriteRenderer = Enemy.AddComponent<SpriteRenderer> ();
        EnemySpriteRenderer.sprite = EnemySprite;
        Enemy.transform.position = new Vector3(Random.Range(MinX, MaxX), MinY, 436.0f);
    }

    // Sets the outlines for the enemy obstacle collider.
    void SetEnemyCollider()
    {
        Vector2 point0 = new Vector2(20.69f, -0.15f);
        Vector2 point1 = new Vector2(10.52f, 17.39f);
        Vector2 point2 = new Vector2(-1.70f, 15.32f);
        Vector2 point3 = new Vector2(-12.42f, 6.33f);
        Vector2 point4 = new Vector2(-15.99f, -4.54f);
        Vector2 point5 = new Vector2(-25.05f, -5.20f);
        Vector2 point6 = new Vector2(-32.15f, -12.92f);
        Vector2 point7 = new Vector2(33.86f, -10.35f);
        Vector2 point8 = new Vector2(31.77f, -5.60f);
        Vector2 point9 = new Vector2(28.24f, -5.75f);
        Vector2 point10 = new Vector2(25.66f, -0.79f);
        Enemy.GetComponent<PolygonCollider2D> ().points = new[]{point0, point1, point2,
            point3, point4, point5, point6, point7, point8, point9, point10};
    }

    // Creates an extra life with box collider and the extra life sprite. 
    // Randomizes a new starting position on the horizontal axis each time.
    void CreateExtraLife()
    {
        ExtraLife = new GameObject("ExtraLife", typeof(BoxCollider2D));
        ExtraLife.tag = "ExtraLife";
        Collision = ExtraLife.GetComponent<Collider2D> ();
        Collision.isTrigger = true;
        ExtraSpriteRenderer = ExtraLife.AddComponent<SpriteRenderer> ();
        ExtraSpriteRenderer.sprite = ExtraLifeSprite;
        ExtraLife.transform.position = new Vector3(Random.Range(MinX, MaxX), -200.0f, 436.0f);
        ExtraLife.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
    }

    // Keyup event to toggle game state between gameplay and pause.
    // Bind to space key.
    void TogglePause()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Game.GameState == "GamePlay")
            {
                Debug.Log("Inside");
                Game.GameState = "Pause";
                return ;
            }
            else if (Game.GameState == "Pause")
                Game.GameState = "GamePlay";
        }
    }
}
