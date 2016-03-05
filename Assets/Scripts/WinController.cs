using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WinController : MonoBehaviour 
{
	public GUIText winText;

	GameObject [] winObj;

	void Start()
	{
		if (winObj == null) {
			winObj = GameObject.FindGameObjectsWithTag ("win");
		}

		//InvokeRepeating("changeWallColors", 1f, 1f);
	}


	void OnCollisionEnter(Collision collisionInfo)
	{
		Debug.Log("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
		EventController.Instance.Publish (new GameOverEvent("random"));
		winText.text = "YOU WIN!";
		foreach (GameObject win in winObj) 
			Destroy (win);
	}

}