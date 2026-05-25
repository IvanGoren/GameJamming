using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // The single instance of this class
    public static GameManager Instance { get; private set; }
    public GameObject gameOverCanvas;
    public MusicController musicController;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Optional: Keep this object alive across scene changes
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {

    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        musicController.TriggerGameOver();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
