using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private bool beingCaught = false;
    private float percentCaught = 0;

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
        if (beingCaught)
        {
            percentCaught += 0.005f;
            slider.value = percentCaught;
        }
        if(percentCaught >= 1)
        {
            transform.SetParent(Bobber);
        }
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
