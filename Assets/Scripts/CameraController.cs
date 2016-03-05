using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 

{
	public GameObject player;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = new Vector3(-2.2f, 5.46f, -10f);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
