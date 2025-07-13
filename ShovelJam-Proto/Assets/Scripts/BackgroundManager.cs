using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	[SerializeField] private Background[] backgrounds;

	// TODO: use scriptable object to hold the sky animator and order information
	[SerializeField] private Animator[] animators;
	private SkyType[] skyOrder = { SkyType.blueSky, SkyType.blueSky, SkyType.RainSky, SkyType.RainSky};
	
	private int skyIndex = 1;
	
	// Start is called before the first frame update
	void Start()
	{
		
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

	public void GetNextSky(Animator animator)
	{
		if (skyIndex < skyOrder.Length-2)
		{
			skyIndex++;
			Debug.Log(skyIndex);
		}
		animator.runtimeAnimatorController = new AnimatorOverrideController(animators[(int)skyOrder[skyIndex+1]].runtimeAnimatorController);

	}

	public void GetPreviousSky(Animator animator)
	{
		if(skyIndex > 1)
		{
			skyIndex--;
			Debug.Log(skyIndex);
		}
		animator.runtimeAnimatorController = new AnimatorOverrideController(animators[(int)skyOrder[skyIndex-1]].runtimeAnimatorController);
	}
}

enum SkyType {
	blueSky,
	RainSky 
}
