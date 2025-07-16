using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	[SerializeField] private int background_count = 3;
	private Background[] backgrounds;

	[SerializeField] private float risingTime = 1;
	[SerializeField] private float rising_NumberOfScreens = 2;
	private float risingMoveStep;
	private Transform seaBackground;

	// TODO: use scriptable object to hold the sky animator and order information
	[SerializeField] private AnimatorOverrideController[] animatorOverrides;

	private SkyType[] skyOrder = { SkyType.blueSky, SkyType.blueSky, SkyType.RainSky, SkyType.RainSky};
	private int skyIndex = 1;

	[SerializeField] private BackgroundSettings backgroundSettings;
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
		}
	}
	
	private void FallingMovement()
	{
		MoveBackground(risingMoveStep);
		if(skyIndex == 1)
		{
            seaBackground.position += Vector3.up * risingMoveStep;
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
		foreach (var background in backgrounds)
		{
			background.gameObject.transform.position += Vector3.up * distance;
		}

	}


	public AnimatorOverrideController GetNextSky()
	{
		if(gameManager.gameState == GameState.Rising)
		{
			return GetSkyAnimatorOverride(0);
		}
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
		return animatorOverrides[(int)skyOrder[index]];
	}
}

public enum SkyType {
	blueSky,
	RainSky 
}
