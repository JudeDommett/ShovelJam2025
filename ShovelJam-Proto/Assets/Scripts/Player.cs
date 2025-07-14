using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject bobber;
    private PlayerState state = PlayerState.Bobber;

    [SerializeField] private float speed = 0.1f;
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
        {
            BobberMovement();
        }
        if (state == PlayerState.Falling)
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

    public void SetPlayerState(PlayerState newState)
    {
        state = newState;
    }

    public void AttachFishToBobber(Transform fish)
    {
        fish.SetParent(bobber.transform);
    }


}

public enum PlayerState {
    Character,
    Bobber,
    Falling 
}
