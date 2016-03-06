using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WinController : MonoBehaviour 
{
	public GameObject winPanel;

	public Text currentTimeText;
	public Text winTimeText;

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
		EventController.Instance.Publish (new GameOverEvent("random"));
		//winPanel.SetActive (true);
		//winTimeText.text = "Time to complete: " + currentTimeText.text;
	}

}