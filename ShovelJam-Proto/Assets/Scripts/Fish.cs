using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private bool beingCaught = false;
    private bool isCaught = false;
    private float percentCaught = 0;

    // Controls How Fish moves
    private float speed = 0.01f;
    private int moveDir = 0;
    private int framesToMove = 0;
    private int framesMoved = 0;

    [SerializeField] private Slider slider;
    private Transform Bobber;

    // Start is called before the first frame update
    void Start()
    {
        Bobber = GameObject.Find("Bobber").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCaught)
        {
            DoMovement();
        }

        if (beingCaught)
        {
            percentCaught += 0.001f;
            slider.value = percentCaught;
        }
        else
        {
            percentCaught -= 0.002f;
            slider.value = percentCaught;
        }
        if(percentCaught >= 1)
        {
            transform.SetParent(Bobber);
            isCaught = true;
        }
    }

    private void DoMovement()
    {
        System.Random random = new System.Random();
        if(framesMoved == framesToMove)
        {
            moveDir = random.Next(2);
            framesToMove = random.Next(30, 90);
            framesMoved = 0;
        }

        if(moveDir%2 == 0)
        {
            transform.position += Vector3.right * speed;
        }
        else
        {
            transform.position += Vector3.left * speed;
        }

        framesMoved++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Bobber.gameObject)
        {
            beingCaught = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == Bobber.gameObject)
        {
            beingCaught = false;
        }
    }
}
