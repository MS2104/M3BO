using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MainGame");
    }
}
