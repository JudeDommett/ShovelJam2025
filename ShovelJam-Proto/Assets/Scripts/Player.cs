using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject bobber;
    private GameObject character;
    private Fish caughtFish;

    [SerializeField] private float bobberSpeed = 1f;
    [SerializeField] private float characterSpeed = 0.5f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BackgroundManager backgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        bobber = GameObject.Find("Bobber");
        bobber.SetActive(false);
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameManager.gameState)
        {
            case GameState.Character:
                bobber.SetActive(false);
                DespawnFish();
                CharacterMovement();
                break;

            case GameState.Bobber:
                bobber.GetComponent<BoxCollider2D>().enabled = true;
                BobberMovement();
                break;

            case GameState.Falling:
                MoveBobberToPosition(new Vector3(0, 60,  0));

                FallMovement();
                bobber.GetComponent<BoxCollider2D>().enabled = false;
                break;

            case GameState.Rising:

                MoveBobberToPosition(new Vector3(0, -30,  0));
                if (!bobber.activeSelf)
                {
                    bobber.SetActive(true);
                    bobber.GetComponent<BoxCollider2D>().enabled = false;
                }
                break;

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

    void MoveBobberToPosition(Vector3 position)
    {
        if(bobber.transform.position.y != position.y)
        {
            bobber.transform.position += Vector3.up * (position.y - bobber.transform.position.y) * Time.deltaTime * 2;
        }
    }

    public void AttachFishToBobber(Transform fish)
    {
        caughtFish = fish.GetComponent<Fish>();
        fish.GetComponentInParent<Background>().fish = null;
        fish.SetParent(bobber.transform);
    }

    public void LoseFish()
    {
        if(gameManager.gameState == GameState.Falling)
        {
            DespawnFish();
        }
    }

    void DespawnFish()
    {
        if(caughtFish != null)
        {
            FlyweightFactory.ReturnToPool(caughtFish);
            caughtFish = null;
        }
    }

}
