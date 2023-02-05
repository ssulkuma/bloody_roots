using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator Animation;
    public StartGame Game;
    public GameObject GameOver;
    public GameObject Retry;
    public Score Score;

    // Triggers the Start Game animation on button click.
    public void OnButtonClick()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (Game.GameState == "StartGame")
            {
                Animation = GetComponentInParent<Animator> ();
                Animation.SetBool("StartGame", false);
            }
            if (Game.GameState == "GameOver")
            {
                GameOver.SetActive(false);
                Retry.SetActive(false);
                Score.ScoreValue = 0;
                Score.gameObject.transform.position = new Vector3(1470.80f, 1145.90f, 291.30f);
                Game.GameState = "GamePlay";
            }
        }
    }
}
