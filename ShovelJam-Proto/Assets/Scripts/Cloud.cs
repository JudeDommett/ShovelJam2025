using UnityEngine;

public class Cloud : Flyweight
{
    new CloudSettings settings => (CloudSettings)base.settings;

    private GameManager gameManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if(collision.gameObject.tag == "Bobber")
        {
            gameManager.LoseFish();
        }
    }
}

