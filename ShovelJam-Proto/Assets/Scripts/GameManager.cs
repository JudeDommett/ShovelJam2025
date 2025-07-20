using Unity.Profiling;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BackgroundManager backgroundManager;

    [SerializeField] private Camera cam;

    private GameObject menu;

    public GameState gameState = GameState.Character;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        menu = GameObject.FindGameObjectWithTag("Menu");

        gameState = GameState.Character;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateGameState(GameState.Rising);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateGameState(GameState.Falling);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;
    }

    public void CatchFish(Transform fish)
    {
        player.AttachFishToBobber(fish);
    }

    public void LoseFish()
    {
        player.LoseFish();
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}

public enum GameState
{
    Character,
    Bobber,
    Falling,
    Rising
}