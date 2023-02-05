using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject RootsPlayer;
    public GameObject RootImage;
    public string GameState;

    void Start()
    {
        GameState = "StartGame";
    }

    // Sets player active when game state changes to Game Play.
    void Update()
    {
        if (RootImage.activeInHierarchy == false && GameState != "GameOver")
        {
            RootsPlayer.SetActive(true);
            if (GameState == "Pause")
                return ;
            GameState = "GamePlay";
        }
    }
}
