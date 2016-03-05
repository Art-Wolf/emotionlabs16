using UnityEngine;
using System.Collections;

public class GoodPickup : MonoBehaviour 
{	
	private GameObject[] players;
	void Start()
	{
		if (players == null) {
			players = GameObject.FindGameObjectsWithTag ("Player");
		}

		//InvokeRepeating("changeWallColors", 1f, 1f);
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		foreach (GameObject player in players) {
			Rigidbody rb = player.GetComponent<Rigidbody> ();
			rb.mass = 0.1f;
		}
		Destroy (this);
	}
}