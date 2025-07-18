using System.Collections.Generic;
using UnityEngine;

public class Background : Flyweight
{
    new BackgroundSettings settings => (BackgroundSettings)base.settings;

    private int pos;
    private Animator animator;
    private BackgroundInfo backgroundInfo;
    private Fish fish = null;

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
            transform.position = new Vector3(transform.position.x, transform.position.y - 540, transform.position.z);
            backgroundInfo = backgroundManager.GetPreviousSky();
            SetBackground(backgroundInfo.backgroundAnim);
            SpawnFish();
        }
        else if(transform.position.y <= -270)
        {
            DespawnFish();
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
        int fishseed = random.Next(0, backgroundInfo.fishSettings.FishTypes.Count - 1);
        if (backgroundInfo.fishSettings ==  null)
        {
            fish = null;
        }
        else
        {
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
        }
    }
}

