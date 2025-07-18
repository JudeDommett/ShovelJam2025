using Unity.Profiling;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BackgroundManager backgroundManager;

    [SerializeField] private Camera cam;

    public GameState gameState = GameState.Character;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        gameState = GameState.Character;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
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
}

public enum GameState
{
    Character,
    Bobber,
    Falling,
    Rising
}