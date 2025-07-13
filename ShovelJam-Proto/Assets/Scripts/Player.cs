using System;
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
    }

    private void BobberMovement()
    {
        if (Input.GetKey("down"))
        {
            backgroundManager.MoveBackground(speed * 2);
        }

        if (Input.GetKey("up"))
        {
            backgroundManager.MoveBackground(speed * -2);
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

    enum PlayerState {
        Character,
        Bobber,
        Falling 
    }

}
