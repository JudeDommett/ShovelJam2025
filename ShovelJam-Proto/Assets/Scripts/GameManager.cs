using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BackgroundManager backgroundManager;

    public GameState gameState = GameState.Character;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Character;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

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
    Falling
}