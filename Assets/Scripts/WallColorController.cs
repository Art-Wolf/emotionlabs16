using UnityEngine;
using System.Collections;

public class WallColorController : MonoBehaviour 
{	
	private GameObject[] wallTops;
	private Component[] topRender;

	public Material sadMaterial;
	public Material angryMaterial;
	public Material happyMaterial;
	public Material surpriseMaterial;

	private float startingMetalic = 0.6f;
	private float endingMetalic = 0.2f;
	private float currentMetalic = 0.58f;

	private bool decreasing = true;

	void Start()
	{
		if (wallTops == null) {
			wallTops = GameObject.FindGameObjectsWithTag ("top");
		}
	}

	void FixedUpdate() 
	{
		if (decreasing) {
			if (currentMetalic > endingMetalic)
				currentMetalic -= 0.01f;
			else {
				decreasing = false;
				currentMetalic += 0.01f;
			}
		} else {
			if (currentMetalic < startingMetalic)
				currentMetalic += 0.01f;
			else {
				decreasing = true;
				currentMetalic -= 0.01f;
			}
		}

		sadMaterial.SetVector("_Color", new Vector4(currentMetalic, 0, 0, 0));
	}

	void Awake()
	{
		EventController.Instance.Subscribe<GoWestEvent> (GoWest);
		EventController.Instance.Subscribe<GoEastEvent> (GoEast);
		EventController.Instance.Subscribe<GoNorthEvent> (GoNorth);
		EventController.Instance.Subscribe<GoSouthEvent> (GoSouth);
	}

	void GoWest(GoWestEvent eventTest) {
		foreach (GameObject top in wallTops) {
			topRender = top.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in topRender) 
			{
				rend.material.Lerp(rend.material, surpriseMaterial, 0.5f);
			}
		}
	}

	void GoEast(GoEastEvent eventTest) {
		foreach (GameObject top in wallTops) {
			topRender = top.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in topRender) 
			{
				rend.material.Lerp(rend.material, angryMaterial, 0.5f);
			}
		}
	}

	void GoNorth(GoNorthEvent eventTest) {
		foreach (GameObject top in wallTops) {
			topRender = top.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in topRender) 
			{
				rend.material.Lerp(rend.material, happyMaterial, 0.5f);
			}
		}
	}

	void GoSouth(GoSouthEvent eventTest) {

		foreach (GameObject top in wallTops) {
			topRender = top.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in topRender) 
			{
				rend.material.Lerp(rend.material, sadMaterial, 0.5f);
			}
		}
	}


}