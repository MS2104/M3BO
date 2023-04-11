using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    private Scoring scoringScript;
    public int finalScore;
    [SerializeField] private TextMeshProUGUI finalScoreDisplay;

    [SerializeField] AudioSource winSong;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            scoringScript = playerObject.GetComponent<Scoring>();
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    void Update()
    {
        if (scoringScript != null)
        {
            int score = scoringScript.score; // Get the score value from the Scoring script
            if (finalScore < score) // Check if finalScore is less than score
            {
                // Increment finalScore by 1 until it's equal to score
                for (int i = finalScore; i <score; i++)
                {
                    finalScore++;
                }

                winSong.Play();
            }
            finalScoreDisplay.text = finalScore.ToString();
        }
    }
}
