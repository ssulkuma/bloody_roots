using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text PlayerScore;
    public float  ScoreValue;
    public StartGame Game;

    // Increases the player's score with each frame when the game state is
    // set to GamePlay and displays it on the screen.
    void Update()
    {
        if (Game.GameState == "StartGame")
            PlayerScore.text = ScoreValue.ToString(" ");
        else
        {
            PlayerScore.text = ScoreValue.ToString("0");
            if (Game.GameState == "GamePlay")
                ScoreValue += 1 * Time.deltaTime * 10;
        }
    }
}
