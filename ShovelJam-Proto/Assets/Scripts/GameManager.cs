using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public bool canCast = false;
 
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
        menu.SetActive(true);

        gameState = GameState.Menu;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
            gameState = GameState.Menu;
        }
        if (Input.GetKeyDown(KeyCode.E) && canCast && gameState == GameState.Character)
        {
            UpdateGameState(GameState.Rising);
        }
        if (Input.GetKeyDown(KeyCode.Q) && gameState == GameState.Bobber)
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

    public void StartGame()
    {
        gameState = GameState.Character;
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
    Menu,
    Character,
    Bobber,
    Falling,
    Rising
}