using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject bobber;
    private GameObject character;
    private Transform caughtFish;

    [SerializeField] private float speed = 0.1f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BackgroundManager backgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        bobber = GameObject.Find("Bobber");
    }

    // Update is called once per frame
    void Update()
    {
        if(state == PlayerState.Bobber)
        if(gameManager.gameState == GameState.Bobber)
        {
            BobberMovement();
        }
        if (gameManager.gameState == GameState.Falling)
        {
            FallMovement();
        }

    }

    private void BobberMovement()
    {
        if (Input.GetKey("down"))
        {
            backgroundManager.MoveBackground(speed * 1.5f);
        }
        
        if (Input.GetKey("up"))
        {
            backgroundManager.MoveBackground(speed * -1.5f);
        }

        if (Input.GetKey("right"))
        {
            bobber.transform.position += Vector3.right * speed;
        }

        if (Input.GetKey("left"))
        {
            bobber.transform.position += Vector3.left * speed;
        }
    }

    private void FallMovement()
    {
        // play is always moving down
        backgroundManager.MoveBackground(speed * 2f);

        if (Input.GetKey("right"))
        {
            bobber.transform.position += Vector3.right * speed;
        }

        if (Input.GetKey("left"))
        {
            bobber.transform.position += Vector3.left * speed;
        }
    }


    public void AttachFishToBobber(Transform fish)
    {
        caughtFish = fish;
        fish.SetParent(bobber.transform);
    }

    public void LoseFish()
    {
        if(gameManager.gameState == GameState.Falling)
        {
            caughtFish.gameObject.SetActive(false);
            //TODO: sent fish back into the background object pool
        }
    }

}
