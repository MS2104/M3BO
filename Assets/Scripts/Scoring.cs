using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreDisplay;
    public int score = 0;

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = score.ToString();
    }
}
