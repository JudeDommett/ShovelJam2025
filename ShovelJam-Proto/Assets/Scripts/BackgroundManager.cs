using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	private int background_count = 3;
	private Background[] backgrounds;

	[SerializeField] private float risingTime = 1;
	[SerializeField] private float rising_NumberOfScreens = 2;
	[SerializeField] private float fallingMoveStep = 1;

	private float risingMoveStep;
	private Transform seaBackground;

	// TODO: use scriptable object to hold the sky animator and order information
	[SerializeField] private AnimatorOverrideController[] animatorOverrides;

	private SkyType[] skyOrder = { SkyType.blueSky, SkyType.blueSky, SkyType.blueSky, SkyType.blueSky, SkyType.blueSky,  SkyType.RainSky, SkyType.RainSky, SkyType.SpaceTransition, SkyType.SpaceSky, SkyType.SpaceSky};
	private int skyIndex = 1;

	[SerializeField] private BackgroundSettings backgroundSettings;
	[SerializeField] private List<FishSettings> fishSettings;
	[SerializeField] private CloudSettings cloudSettings;
	[SerializeField] private GameManager gameManager;
	
	// Start is called before the first frame update
	void Start()
	{
		seaBackground = GameObject.FindGameObjectWithTag("SeaBackground").transform;
		risingMoveStep = (180 * rising_NumberOfScreens) / (risingTime * 60);
	}

	// Update is called once per frame
	void Update()
	{
		switch (gameManager.gameState) {
			case GameState.Rising:
				CreateBackgrounds();
				RisingMovement();
				break;
			case GameState.Falling:
				FallingMovement();
				break;
		}

	}

	public void CreateBackgrounds()
	{
		if (backgrounds == null)
		{
			backgrounds = new Background[background_count];
			for (int i = 0; i < background_count; i++)
			{
				backgrounds[i] = (Background)FlyweightFactory.Spawn(backgroundSettings);
				backgrounds[i].backgroundManager = this;
				backgrounds[i].transform.parent = transform;
				backgrounds[i].transform.position = new Vector3(0, 180 * (i-1), 0);
				backgrounds[i].SetBackground(GetSkyAnimatorOverride(0));
			}
		}
	}

	public void ClearBackground()
	{
		foreach(Background background in backgrounds)
		{
			background.DespawnCloud();
			background.DespawnFish();
			FlyweightFactory.ReturnToPool(background);
		}
		backgrounds = null;
	}

	private void RisingMovement()
	{
		seaBackground.position += Vector3.down * risingMoveStep;
		MoveBackground(-risingMoveStep);
		if(seaBackground.position.y <= (-180 * rising_NumberOfScreens))
		{
			gameManager.UpdateGameState(GameState.Bobber);
			seaBackground.position = new Vector3(0, -180 * rising_NumberOfScreens, 0);
		}
	}
	
	private void FallingMovement()
	{
		MoveBackground(fallingMoveStep);
		if(skyIndex == 1)
		{
            seaBackground.position += Vector3.up * fallingMoveStep;
			if(seaBackground.position.y >= 0)
			{
				seaBackground.position = Vector3.zero;
				gameManager.UpdateGameState(GameState.Character);
				ClearBackground();
			}

        }
    }

	public void MoveBackground(float distance)
	{
		if(backgrounds == null)
		{
			CreateBackgrounds();
		}
		Debug.Log(backgrounds);
		foreach (Background background in backgrounds)
		{
			background.gameObject.transform.position += Vector3.up * distance;
		}

	}


	public BackgroundInfo GetNextSky()
	{
		BackgroundInfo info = new BackgroundInfo();
		info.cloudSettings = null;

		if(gameManager.gameState == GameState.Rising)
		{
			info.backgroundAnim = GetSkyAnimatorOverride(0);
			info.fishSettings = null;
            return info;
		}
		if (skyIndex < skyOrder.Length-2)
		{
			skyIndex++;
		}
		info.backgroundAnim = GetSkyAnimatorOverride(skyIndex + 1);
		info.fishSettings = GetFishSettings(skyIndex + 1);
		return info;

	}

	public BackgroundInfo GetPreviousSky()
	{
		BackgroundInfo info = new BackgroundInfo();
		
		info.cloudSettings = null;
		
		if(skyIndex > 1)
		{
			skyIndex--;
		}
        info.backgroundAnim = GetSkyAnimatorOverride(skyIndex - 1);

        info.fishSettings = GetFishSettings(skyIndex - 1);
		if(gameManager.gameState == GameState.Falling)
		{
			info.fishSettings = null;
			if(seaBackground.position.y < -360)
			{
				info.cloudSettings = cloudSettings;
			}
		}
		return info;
	}

	//returns override controller for the sky at the given index
	private AnimatorOverrideController GetSkyAnimatorOverride(int index)
	{
		return animatorOverrides[(int)skyOrder[index]];
	}

	private FishSettings GetFishSettings(int index)
	{
		foreach(FishSettings settings in fishSettings)
		{
			if(settings.skyType == skyOrder[index])
			{
				return settings;
			}
		}
		return null;
	}
}

public struct BackgroundInfo
{
	public AnimatorOverrideController backgroundAnim;
	public FishSettings fishSettings;
	public CloudSettings cloudSettings;
}


public enum SkyType {
	blueSky,
	RainSky,
	SpaceTransition,
	SpaceSky
}
