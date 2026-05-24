using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    void Start()
    {
        Time.timeScale=1;
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale=0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
