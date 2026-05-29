using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class TimedGameEvent
    {
        public string eventName;
        public float triggerAtSeconds;
        public DialogueSpeaker speaker;
        [TextArea] public string dialogueText;
        public AudioClip dialogueAudio;
        public float dialogueDurationSeconds = 3f;
        public GameObject[] objectsToShow;
        public bool hideObjectsOnStart = true;
        public float hideAfterSeconds;
        public UnityEvent onTrigger;
        [HideInInspector] public bool hasTriggered;
    }

    // The single instance of this class
    public static GameManager Instance { get; private set; }
    public GameObject gameOverCanvas;
    public MusicController musicController;
    public DialogueCanvas dialogueCanvas;
    public DialogueAudioMgr dialogueAudioMgr;
    public TimedGameEvent[] timedEvents = new TimedGameEvent[0];

    // Timer para gestionar dialogos.
    private const float MuteMusicAtSeconds = 85f;
    private float timer;
    private bool isPlaythroughRunning;
    private bool hasMutedMusic;

    void Start()
    {
        Time.timeScale = 1;
        ResolveSceneReferences();
        ResetPlaythroughTimer();
    }

    void Update()
    {
        if (!isPlaythroughRunning)
        {
            return;
        }

        timer += Time.deltaTime;
        CheckMusicMute();
        CheckTimedEvents();
    }

    public void GameOver()
    {
        isPlaythroughRunning = false;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        musicController.TriggerGameOver();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetPlaythroughTimer()
    {
        timer = 0;
        isPlaythroughRunning = true;
        hasMutedMusic = false;

        foreach (TimedGameEvent timedEvent in timedEvents)
        {
            if (timedEvent == null)
            {
                continue;
            }

            timedEvent.hasTriggered = false;

            if (timedEvent.hideObjectsOnStart)
            {
                SetTimedEventObjectsActive(timedEvent, false);
            }
        }
    }

    private void CheckMusicMute()
    {
        if (hasMutedMusic || timer < MuteMusicAtSeconds)
        {
            return;
        }

        hasMutedMusic = true;

        if (musicController != null)
        {
            musicController.SetMuted(true);
        }
    }

    private void CheckTimedEvents()
    {
        foreach (TimedGameEvent timedEvent in timedEvents)
        {
            if (timedEvent == null)
            {
                continue;
            }

            if (timedEvent.hasTriggered || timer < timedEvent.triggerAtSeconds)
            {
                continue;
            }

            TriggerTimedEvent(timedEvent);
        }
    }

    private void TriggerTimedEvent(TimedGameEvent timedEvent)
    {
        timedEvent.hasTriggered = true;

        ResolveSceneReferences();

        if (dialogueCanvas != null && !string.IsNullOrWhiteSpace(timedEvent.dialogueText))
        {
            dialogueCanvas.ShowDialogue(
                timedEvent.speaker,
                timedEvent.dialogueText,
                timedEvent.dialogueDurationSeconds
            );
        }

        if (dialogueAudioMgr != null && timedEvent.dialogueAudio != null)
        {
            dialogueAudioMgr.PlayDialogue(timedEvent.dialogueAudio);
        }

        SetTimedEventObjectsActive(timedEvent, true);

        timedEvent.onTrigger?.Invoke();

        if (timedEvent.hideAfterSeconds > 0)
        {
            StartCoroutine(HideTimedEventObjects(timedEvent));
        }
    }

    private void ResolveSceneReferences()
    {
        if (dialogueCanvas == null)
        {
            dialogueCanvas = FindFirstObjectByType<DialogueCanvas>(FindObjectsInactive.Include);

            if (dialogueCanvas == null)
            {
                GameObject dialogueCanvasObject = GameObject.Find("DialogueCanvas");

                if (dialogueCanvasObject != null)
                {
                    dialogueCanvas = dialogueCanvasObject.AddComponent<DialogueCanvas>();
                }
            }
        }

        if (dialogueAudioMgr == null)
        {
            dialogueAudioMgr = FindFirstObjectByType<DialogueAudioMgr>(FindObjectsInactive.Include);

            if (dialogueAudioMgr == null)
            {
                GameObject dialogueAudioObject = GameObject.Find("DialogueAudioMgr");

                if (dialogueAudioObject != null)
                {
                    dialogueAudioMgr = dialogueAudioObject.AddComponent<DialogueAudioMgr>();
                }
            }
        }
    }

    private void SetTimedEventObjectsActive(TimedGameEvent timedEvent, bool isActive)
    {
        if (timedEvent.objectsToShow != null)
        {
            foreach (GameObject objectToShow in timedEvent.objectsToShow)
            {
                if (objectToShow != null)
                {
                    objectToShow.SetActive(isActive);
                }
            }
        }
    }

    private IEnumerator HideTimedEventObjects(TimedGameEvent timedEvent)
    {
        yield return new WaitForSeconds(timedEvent.hideAfterSeconds);

        if (timedEvent.objectsToShow == null)
        {
            yield break;
        }

        foreach (GameObject objectToShow in timedEvent.objectsToShow)
        {
            if (objectToShow != null)
            {
                objectToShow.SetActive(false);
            }
        }
    }
}
