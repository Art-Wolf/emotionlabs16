using UnityEngine;
using System.Collections;

public class DelayedSceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(ChangeLevel());
	}

	IEnumerator ChangeLevel() {
		yield return new WaitForSeconds (19);
		Application.LoadLevel(2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
