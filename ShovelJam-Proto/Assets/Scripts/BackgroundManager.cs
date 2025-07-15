using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	[SerializeField] private int background_count = 3;
	private Background[] backgrounds;

	// TODO: use scriptable object to hold the sky animator and order information
	[SerializeField] private Animator[] animators;

	private SkyType[] skyOrder = { SkyType.blueSky, SkyType.blueSky, SkyType.RainSky, SkyType.RainSky};
	private int skyIndex = 1;

	[SerializeField] private BackgroundSettings backgroundSettings;
	
	// Start is called before the first frame update
	void Start()
	{
		backgrounds = new Background[background_count];
		for (int i = 0; i < background_count; i++)
		{
			backgrounds[i] = (Background)FlyweightFactory.Spawn(backgroundSettings);
			backgrounds[i].backgroundManager = this;
			backgrounds[i].transform.parent = transform;
			backgrounds[i].transform.position = new Vector3(0, 180 * (i-1), 0);
			backgrounds[i].SetBackground(GetSkyAnimatorOverride(i));
		}
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void MoveBackground(float distance)
	{
		foreach (var background in backgrounds)
		{
			background.gameObject.transform.position += Vector3.up * distance;
		}

	}

	public AnimatorOverrideController GetNextSky()
	{
		if (skyIndex < skyOrder.Length-2)
		{
			skyIndex++;
		}
		return GetSkyAnimatorOverride(skyIndex + 1);

	}

	public AnimatorOverrideController GetPreviousSky()
	{
		if(skyIndex > 1)
		{
			skyIndex--;
		}
		return GetSkyAnimatorOverride(skyIndex - 1);
	}

	//returns override controller for the sky at the given index
	private AnimatorOverrideController GetSkyAnimatorOverride(int index)
	{
        return new AnimatorOverrideController(animators[(int)skyOrder[index]].runtimeAnimatorController);
    }
}

enum SkyType {
	blueSky,
	RainSky 
}
