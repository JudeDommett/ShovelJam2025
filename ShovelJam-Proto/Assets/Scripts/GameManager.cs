using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BackgroundManager backgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePlayerState(PlayerState newState)
    {
        player.SetPlayerState(newState);
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
