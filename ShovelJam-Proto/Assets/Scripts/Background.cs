using System.Collections.Generic;
using UnityEngine;

public class Background : Flyweight
{
    new BackgroundSettings settings => (BackgroundSettings)base.settings;

    private int pos;
    
    public Fish fish = null;
    private Animator animator;
    private BackgroundInfo backgroundInfo;
    private Cloud cloud = null;

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
            DespawnFish();
            DespawnCloud();

            transform.position = new Vector3(transform.position.x, transform.position.y - 540, transform.position.z);
            backgroundInfo = backgroundManager.GetPreviousSky();
            SetBackground(backgroundInfo.backgroundAnim);

            SpawnFish();
            SpawnCloud();
        }
        else if(transform.position.y <= -270)
        {
            DespawnFish();
            DespawnCloud();

            transform.position = new Vector3(transform.position.x, transform.position.y + 540, transform.position.z);
            backgroundInfo = backgroundManager.GetNextSky();
            SetBackground(backgroundInfo.backgroundAnim);
            SpawnFish();
        }
    }

    
    public void SetBackground(AnimatorOverrideController newBackground)
    {
        animator.runtimeAnimatorController = newBackground;
    }

    public void SpawnFish()
    {
        System.Random random = new System.Random();

        if (backgroundInfo.fishSettings ==  null)
        {
            fish = null;
        }
        else
        {
            int fishseed = random.Next(0, backgroundInfo.fishSettings.FishTypes.Count - 1);
            
            fish = (Fish)FlyweightFactory.Spawn(backgroundInfo.fishSettings);
            fish.transform.SetParent(transform);
            fish.transform.position = transform.position + new Vector3(0, 80);
            fish.SetFishAnim(backgroundInfo.fishSettings.FishTypes[fishseed].fishAnim[fishseed]);
        }
    }

    public void DespawnFish()
    {
        if (fish != null)
        {
            FlyweightFactory.ReturnToPool(fish);
            fish.transform.rotation = Quaternion.Euler(0,0,0);
            fish = null;
        }
    }

    public void SpawnCloud()
    {
        System.Random random = new System.Random();
        int cloudPos = random.Next(-1, 1);

        if (backgroundInfo.cloudSettings != null)
        {
            cloud = (Cloud)FlyweightFactory.Spawn(backgroundInfo.cloudSettings);
            cloud.transform.SetParent(transform);
            cloud.transform.position = transform.position + new Vector3(90*cloudPos, -40);
            return;
        }

        cloud = null;
    }

    public void DespawnCloud()
    {
        if(cloud != null)
        {
            FlyweightFactory.ReturnToPool(cloud);
            cloud = null;
        }
    }
}

