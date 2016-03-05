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

	void Start()
	{
		if (wallTops == null) {
			wallTops = GameObject.FindGameObjectsWithTag ("top");
		}
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