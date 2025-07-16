using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject bobber;
    private GameObject character;
    private Transform caughtFish;

    [SerializeField] private float bobberSpeed = 1f;
    [SerializeField] private float characterSpeed = 0.5f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BackgroundManager backgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        bobber = GameObject.Find("Bobber");
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameManager.gameState);
        if(gameManager.gameState == GameState.Character)
        {
            CharacterMovement();
        }

        if(gameManager.gameState == GameState.Bobber)
        {
            BobberMovement();
        }

        if (gameManager.gameState == GameState.Falling)
        {
            FallMovement();
        }

    }

    private void CharacterMovement()
    {
        if (Input.GetKey("down"))
        {
            character.transform.position += Vector3.down * characterSpeed;
        }

        if (Input.GetKey("up"))
        {
            character.transform.position += Vector3.up * characterSpeed;
        }

        if (Input.GetKey("right"))
        {
            character.transform.position += Vector3.right * characterSpeed;
        }

        if (Input.GetKey("left"))
        {
            character.transform.position += Vector3.left * characterSpeed;
        }
    }

    private void BobberMovement()
    {
        if (Input.GetKey("down"))
        {
            backgroundManager.MoveBackground(bobberSpeed * 1.5f);
        }
        
        if (Input.GetKey("up"))
        {
            backgroundManager.MoveBackground(bobberSpeed * -1.5f);
        }

        if (Input.GetKey("right"))
        {
            bobber.transform.position += Vector3.right * bobberSpeed;
        }

        if (Input.GetKey("left"))
        {
            bobber.transform.position += Vector3.left * bobberSpeed;
        }
    }

    private void FallMovement()
    {
        // play is always moving down
        backgroundManager.MoveBackground(bobberSpeed * 2f);

        if (Input.GetKey("right"))
        {
            bobber.transform.position += Vector3.right * bobberSpeed;
        }

        if (Input.GetKey("left"))
        {
            bobber.transform.position += Vector3.left * bobberSpeed;
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
