using UnityEngine;

public class Roots : MonoBehaviour
{
    public int LifeCount;
    private float MoveX;
    private float MoveSpeed;
    private float LeftMostX;
    private float RightMostX;
    private Vector3 PreviousPosition;
    public GameObject RootsPlayer;
    public StartGame Game;
    public AudioSource AudioSource;
    public AudioClip ExtraLife;
    public AudioClip Obstacle;

    void Start()
    {
        MoveSpeed = 100.0f;
        LeftMostX = 265.0f;
        RightMostX = 486.0f;
    }

    // The game loop. Gets called once every frame.
    void Update()
    {
        if (Game.GameState == "GamePlay")
            MoveRoots();
        if (Game.GameState == "GameOver")
        {
            if (gameObject.activeInHierarchy == true)
                gameObject.SetActive(false);
        }
    }

    // Moves the Roots x position based on the horizontal keypresses (Left and
    // right arrowkeys + A and D keys).
    void MoveRoots()
    {
        PreviousPosition = transform.position;
        MoveX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(MoveX, 0.0f, 0.0f) * Time.deltaTime * MoveSpeed;
        if (transform.position.x < LeftMostX || transform.position.x > RightMostX)
            transform.position = PreviousPosition;
    }

    // Detects collision between objects and either substacts or adds to the
    // player's lifecount. If lifecount hits 0, changes game state to
    // Game Over.
    void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.gameObject.CompareTag("Enemy"))
        {
            LifeCount -= 1;
            AudioSource.PlayOneShot(Obstacle);
            if (LifeCount <= 0)
                Game.GameState = "GameOver";
        }
        if (Hit.gameObject.CompareTag("ExtraLife"))
        {
            AudioSource.PlayOneShot(ExtraLife);
            if (LifeCount < 3)
                LifeCount += 1;
        }
    }
}
