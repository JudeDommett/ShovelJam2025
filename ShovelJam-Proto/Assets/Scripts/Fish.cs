using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Fish : MonoBehaviour
{
    private bool beingCaught = false;
    private bool isCaught = false;
    private float percentCaught = 0;

    // Controls How Fish moves
    [SerializeField] private float speed = 0.01f;
    private int moveDir = 0;
    private int framesToMove = 0;
    private int framesMoved = 0;

    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;
    private Transform bobber;

    // Start is called before the first frame update
    void Start()
    {
        bobber = GameObject.Find("Bobber").transform;
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
            if (percentCaught < 0)
            {
                percentCaught = 0;
                slider.gameObject.SetActive(false);

            }

            slider.value = percentCaught;
        }
        if(percentCaught >= 1)
        {
            transform.SetParent(bobber);
            isCaught = true;
            slider.gameObject.SetActive(false);
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

        slider.transform.position = cam.WorldToScreenPoint(transform.position) + new Vector3(0,100);    

        framesMoved++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bobber")
        {
            slider.gameObject.SetActive(true);
            beingCaught = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bobber")
        {
            beingCaught = false;
        }
    }
}
