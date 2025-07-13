using UnityEngine;

public class Background : MonoBehaviour
{
    private int pos;
    private Animator animator;

    // TODO: decouple background to it's manager
    [SerializeField ] private BackgroundManager backgroundManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= 270)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 540, transform.position.z);
            backgroundManager.GetPreviousSky(animator);
        }
        else if(transform.position.y <= -270)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 540, transform.position.z);
            backgroundManager.GetNextSky(animator);
        }
    }
}
