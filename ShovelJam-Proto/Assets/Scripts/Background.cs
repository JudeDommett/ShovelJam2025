using Unity.VisualScripting;
using UnityEngine;

public class Background : Flyweight
{
    new BackgroundSettings settings => (BackgroundSettings)base.settings;

    private int pos;
    private Animator animator;

    // TODO: decouple background to it's manager
    [SerializeField] public BackgroundManager backgroundManager;


    // Start is called before the first frame update
    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= 270)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 540, transform.position.z);
            SetBackground(backgroundManager.GetPreviousSky());
        }
        else if(transform.position.y <= -270)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 540, transform.position.z);
            SetBackground(backgroundManager.GetNextSky());
        }
    }

    
    public void SetBackground(AnimatorOverrideController newBackground)
    {
        animator.runtimeAnimatorController = newBackground;
    }
}

[CreateAssetMenu(menuName = "Flyweight/Background Settings")]
public class BackgroundSettings : FlyweightSettings
{
    public override Flyweight Create()
    {
        var go = Instantiate(prefab);

        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.GetOrAddComponent<Background>();
        flyweight.settings = this;

        return flyweight;
    }
}

