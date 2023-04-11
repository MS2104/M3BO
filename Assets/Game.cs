using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerDisplay;

    public float timeLeft = 5f;
    private bool isCountingDown = true;

    public GameObject Player;
    public GameObject Gun;
    public GameObject GameData;
    public GameObject FinalScore;

    public AudioSource ambience;

    void Awake()
    {
        // Assign necessary objects if they are not assigned
        if (Player == null)
            Player = GameObject.FindWithTag("Player"); // Replace with appropriate tag or GameObject.Find() call
        if (Gun == null)
            Gun = GameObject.FindWithTag("Gun"); // Replace with appropriate tag or GameObject.Find() call
        if (GameData == null)
            GameData = GameObject.FindWithTag("GameData"); // Replace with appropriate tag or GameObject.Find() call
        if (FinalScore == null)
            FinalScore = GameObject.FindWithTag("FinalScore"); // Replace with appropriate tag or GameObject.Find() call
    }

    void Update()
    {
        if (isCountingDown) // Added check for boolean flag
        {
            if (timeLeft > 0)
            {
                int timeLeftInteger = Mathf.RoundToInt(timeLeft);
                timerDisplay.text = timeLeftInteger.ToString();

                timeLeft -= Time.deltaTime;
            }
            else
            {
                isCountingDown = false;
            }
        }

        if (!isCountingDown)
        {
            GameData.SetActive(false);
            Gun.SetActive(false);
            Player.GetComponent<PlayerController>().enabled = false;
            ambience.Stop();

            FinalScore.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Place any additional code that should run when the timer runs out here
        }
    }
}
