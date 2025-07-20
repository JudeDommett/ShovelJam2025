using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPrompt : MonoBehaviour
{
    private GameObject text;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Text");
        text.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            text.gameObject.SetActive(true);
            gameManager.canCast = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            text.gameObject.SetActive(false);
            gameManager.canCast = false;
        }
    }
}
