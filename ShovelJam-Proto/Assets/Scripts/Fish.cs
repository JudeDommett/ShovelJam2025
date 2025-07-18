using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : Flyweight
{
    new FishSettings settings => (FishSettings)base.settings;

    private bool beingCaught = false;
    private bool isCaught = false;
    private float percentCaught = 0;

    // Controls How Fish moves
    [SerializeField] private float speed = 0.01f;
    private int moveDir = 0;
    private int framesToMove = 0;
    private int framesMoved = 0;

    [SerializeField] private Slider slider;

    private GameManager gameManager;
    private Camera cam;
    private Animator animator;

    // Start is called before the first frame update
    void OnEnable()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
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
            percentCaught += 0.002f;
            slider.value = percentCaught;
        }
        else
        {
            percentCaught -= 0.004f;
            if (percentCaught < 0)
            {
                percentCaught = 0;
                slider.gameObject.SetActive(false);

            }

            slider.value = percentCaught;
        }
        if(percentCaught >= 1)
        {
            // update gamestate
            gameManager.UpdateGameState(GameState.Falling);

            gameManager.CatchFish(this.transform);
            // TODO: have the fish become "detached" from the BGManager to avoid it reusing the object 

            isCaught = true;
            slider.gameObject.SetActive(false);

        }
    }

    private void DoMovement()
    {
        System.Random random = new System.Random();

        //if completed previous movement generate new direction and move time
        if(framesMoved == framesToMove)
        {
            moveDir = random.Next(2);
            framesToMove = random.Next(45, 90);
            framesMoved = 0;
        }

        // override move dir if gone too far
        if(transform.position.x > 100)
        {
            moveDir = 1;
        }
        if(transform.position.x < -100)
        {
            moveDir = 0;
        }

        if(moveDir%2 == 0)
        {
            transform.position += Vector3.right * speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.position += Vector3.left * speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // update slider positon
        //slider.transform.position = cam.WorldToScreenPoint(transform.position) + new Vector3(0,100);
        
        framesMoved++;
    }

    public void SetFishAnim(AnimatorOverrideController newBackground)
    {
        animator.runtimeAnimatorController = newBackground;
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

[CreateAssetMenu(menuName = "FishAndAnim")]
public class FishAndAnim : ScriptableObject
{
    public List<FishType> fishType;
    public List<AnimatorOverrideController> fishAnim;
}

public enum FishType {
    Gold,
    Sardine,
    Soap,
    Cat,
    Clown,
    Peeper,
    BoxJellyFish,
    Glorp
}
 
