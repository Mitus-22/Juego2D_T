using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool invulnerable = false;
    private static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

    void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

    void OnSceneChanged(Scene previousScene, Scene newScene)
        {
            Time.timeScale = 1;
            playerControl1.enemykilled = 0;
            playerControl1.doorHp = 3;
        }
    }


